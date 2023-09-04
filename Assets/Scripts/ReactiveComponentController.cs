using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting;

/// <summary>
/// Script used by either a Floor or Wall that will 
/// react to the presence of the Player or other Game Objects.
/// </summary>
public class ReactiveComponentController : MonoBehaviour
{
    /// <summary>
    /// Identify the purpose of this surface.
    /// </summary>
    enum WallFloorType { Wall, Floor };

    /// <summary>
    /// Designer will choose a type for this surface.
    /// </summary>
    [SerializeField] WallFloorType type;

    /// <summary>
    /// A child game object of the surface which is used to position the spotLight. 
    /// This is being referenced as a field as Hocking promotes the idea of passing 
    /// known Game Objects rather than using slower methods such as GameObject.Find, GetComponent<>
    /// </summary>
    [SerializeField] GameObject lightMount;
    /// <summary>
    /// A reference to the Light component which is a component of the lightMount.
    /// This is being referenced as a field as.. see above comment.
    /// While the LightMount is passed to this variable, using the Light datatype 
    /// effectively references that component of the GameObject.
    /// the game object.
    /// </summary>
    [SerializeField] Light spotLight;

    /// <summary>
    /// The designer will choose the maximum intensity applied to the spotlight.
    /// </summary>
    [Range(0, 1f)]
    [SerializeField] float maxSpotlightIntensity = 1.0f;

    /// <summary>
    /// Class variable that stores the current ray initialized in DoSphere.
    /// currentyRay is used by OnDrawGizmos.
    /// </summary>
    private Ray currentRay;
    internal float InverseFactorForSpotlight;

    /// <summary>
    /// Other scripts need to know if this script has a spotlight component.
    /// </summary>
    public bool HasSpotlight => spotLight != null;


    /// <summary>
    /// When the Player collides with the surface, where the surface is a wall,
    /// then the intensity of the spotlight is set to value of the maxIntensity 
    /// class variable.
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.LogFormat("Collision Entered by {0} into {1}",
                collision.gameObject.name,
                gameObject.name
                );

            if (type == WallFloorType.Wall && spotLight != null)
            {
                spotLight.intensity = maxSpotlightIntensity;
            }

        }
    }

    /// <summary>
    /// DoSphere is called indirectly by a Player event when the player is in contact 
    /// with the game object connected with the instance of this script.
    /// </summary>
    /// <param name="target">The player</param>
    public void TrackTarget(GameObject target)
    {

        // Reference to the world position of the child game object that hosts the spotlight.
        Vector3 p1 = lightMount.transform.position;

        // Used to store the distance between the spotlight and the target.
        float distanceToObstacle = 0;

        // The distance as a Vector3 between the spotlight and the target.
        Vector3 direction = target.transform.position - p1;

        // Stores the information about the hit.
        RaycastHit hitInfo;

        // Create a ray between the spotlight and the target
        Ray ray = new Ray(p1, direction);
        
        // Determine the distance if there is a hit. This will be used in a later task.
        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity))
        {
            distanceToObstacle = hitInfo.distance;

            float spotLightIntensity = 1f;
            if (InverseFactorForSpotlight > 0)
            {
                spotLightIntensity =  InverseFactorForSpotlight/ distanceToObstacle;
                spotLight.intensity = spotLightIntensity;
            }

            // Initialize the local variable ray is copied to the class variable currentRay
            // as it is being used by OnDrawGizmos.
            currentRay = ray;
            Debug.LogFormat($"Distance to obstacle:{distanceToObstacle}");
        }
    }

    /// <summary>
    /// The player is no longer touching the surface that this script is attached to.
    /// </summary>
    /// <param name="collision">The object that collided with the surface.</param>
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (type == WallFloorType.Wall && spotLight != null)
            {
                spotLight.intensity = 0;
            }
        }

    }

    /// <summary>
    /// Used for visual debugging within the scene window.
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(currentRay.origin, currentRay.direction);
        
    }
}

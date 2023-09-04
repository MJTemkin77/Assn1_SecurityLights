using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting;

public class ReactiveComponentController : MonoBehaviour
{
    enum WallFloorType { Wall, Floor };
    [SerializeField] WallFloorType type;

    private Light spotLight;
    private GameObject lightMount;
        
    
    // Start is called before the first frame update
    void Start()
    {
        if (type == WallFloorType.Wall)
        {
            spotLight = gameObject.GetComponentInChildren<Light>();
            if (spotLight != null )
            {
                lightMount = transform.Find("LightMount").gameObject;
            }
        }

    }

    public bool HasSpotlight => spotLight != null;  

    // Update is called once per frame
    void Update()
    {

    }

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
                spotLight.intensity = 1;
            }
          
        }
    }

    Ray currentRay = new Ray();
    public void DoSphere(GameObject target)
    {
        Vector3 p1 = lightMount.transform.position;
        
        float distanceToObstacle = 0;
        //Debug.LogFormat("Spotlight at: {0}, target at: {1}", p1, target.transform.position);

        Vector3 direction = target.transform.position - p1;
        // Cast a sphere wrapping character controller 10 meters forward
        // to see if it is about to hit anything.
        RaycastHit hitInfo;

        Ray ray = new Ray(p1, direction);
        currentRay = ray;
        //if (Physics.Raycast(p1, direction, out hitInfo, Mathf.Infinity))
        //{
        //    Debug.Log("P to P works");
        //}

        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity))
        {
            distanceToObstacle = hitInfo.distance;
            Debug.LogFormat($"Distance to obstacle:{distanceToObstacle}");
        }
    }
    

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

    private void OnDrawGizmos()
    {

        if (!(currentRay.origin == Vector3.zero && currentRay.direction == Vector3.zero))
            Gizmos.DrawLine(currentRay.origin, currentRay.direction);
    }
}

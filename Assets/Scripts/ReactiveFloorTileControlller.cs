using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Actions that apply to the child Wall or Floor game objects
/// </summary>
public enum TileActions { None, SlideUp, SlideDown, SwingOpen, DropsAway }

/// <summary>
/// Script attached to the parent object that contains Reactive Floor and/or Wall Game Objects.
/// The purpose of this script is to provide access to its child game objects and to make
/// it easier for the designer to configure the actions of the child game objects.
/// </summary>
public class ReactiveFloorTileController : MonoBehaviour
{
    

    /// <summary>
    /// The Designer selects the TileAction for the Wall.
    /// </summary>
    [SerializeField] TileActions WallAction;

    /// <summary>
    /// The Designer selects the TileAction for the Floor.
    /// </summary>
    [SerializeField] TileActions FloorAction;

    /// <summary>
    /// References to the child game object for the floor.
    /// </summary>
    [SerializeField] GameObject floor;

    /// <summary>
    /// References to the child game object for the wall.
    /// </summary>
    [SerializeField] GameObject wall;

    [SerializeField] float InverseFactorForSpotlight = 1f;

    /// <summary>
    /// The designer will choose the maximum intensity applied to the spotlight.
    /// </summary>
    [Range(0, 20)]
    [SerializeField] float maxSpotlightIntensity = 1.0f;


    /// <summary>
    /// Reference to the script component for the wall.
    /// </summary>
    private ReactiveComponentController wallController;


    
    /// <summary>
    /// Initialize any controllers, etc.
    /// Access the child script components and pass along values that they need.
    /// </summary>
    private void Awake()
    {
        
        if (wall != null)
        {
            wallController = wall.GetComponent<ReactiveComponentController>();
            wallController.InverseFactorForSpotlight = InverseFactorForSpotlight;
            wallController.maxSpotlightIntensity = maxSpotlightIntensity;
            wallController.WallAction = WallAction;
            
        }
    }

    /// <summary>
    /// Called by the Player (or other GameObject) to call the TrackTarget routine.
    /// </summary>
    /// <param name="target">The Player</param>
    public bool TrackTarget(GameObject target)
    {
        if (wall != null && wallController.HasSpotlight)
        {
            wallController.TrackTarget(target);
            return true;
        }
        return false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveFloorTileController : MonoBehaviour
{
    enum TileActions { None, SlideUp, SlideDown, SwingOpen, DropsAway }
    [SerializeField] TileActions WallAction;
    [SerializeField] TileActions FloorAction;

    [SerializeField] GameObject floor;
    [SerializeField] GameObject wall;

    private ReactiveComponentController wallController;

    private void Start()
    {
        if (wall != null)
            wallController = wall.GetComponent<ReactiveComponentController>();
    }
    public void DoSphere(GameObject target)
    {
        if (wall != null && wallController.HasSpotlight)
        {
            wallController.DoSphere(target);
        }
    }
}

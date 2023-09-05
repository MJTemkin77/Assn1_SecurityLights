using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveWallMovement : MonoBehaviour
{

    /// <summary>
    /// The Designer selects the TileAction for the Wall.
    /// </summary>
    public TileActions WallAction = TileActions.None;

    Rigidbody body;
    Bounds bounds;
    private void Start()
    {
        body = GetComponent<Rigidbody>();
        bounds = GetComponent<MeshRenderer>().bounds;
    }
    public void DoAction(Vector3 otherVec)
    {
        Debug.LogFormat("Wall will {0}", WallAction);

        switch (WallAction)
        {
            case TileActions.SlideUp:
            case TileActions.SlideDown:
                Vector3 direction = WallAction == TileActions.SlideUp? Vector3.up: Vector3.down;
                if (body != null)
                {
                    body.useGravity = false;
                    transform.Translate(direction * otherVec.y, Space.World);

                }
                break;
            case TileActions.SwingOpen:

                transform.localRotation = Quaternion.Euler(90, 0, 10);
                break;
            default: break;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveFloorTileControlller : MonoBehaviour
{
    enum TileActions { None, SlideUp, SlideDown, SwingOpen, DropsAway }
    [SerializeField] TileActions WallAction;
    [SerializeField] TileActions FloorAction;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

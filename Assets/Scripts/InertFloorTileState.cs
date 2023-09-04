using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Used to customize the floor tile for color and other properties.
/// </summary>
public class InertFloorTileState : MonoBehaviour
{
    /// <summary>
    /// A material to be applied to the floor tile.
    /// </summary>
    [SerializeField] Material floorMaterial;

    /// <summary>
    /// Initialize all component references and properties.
    /// </summary>
    void Start()
    {
        GetComponent<MeshRenderer>().material = floorMaterial;
    }

    
}

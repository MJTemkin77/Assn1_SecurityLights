using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InertFloorTileState : MonoBehaviour
{
    [SerializeField] Material floorMaterial;

    /// <summary>
    /// All initialization 
    /// </summary>
    void Start()
    {
        GetComponent<MeshRenderer>().material = floorMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

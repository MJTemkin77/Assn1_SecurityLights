using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// NOTE: This script is NOT currently in use by the scene. It was used for doing 
/// a simple Raycast between two game objects and to demonstrate using a LineRenderer component.
/// </summary>
public class RaycastShoot : MonoBehaviour
{
    [SerializeField] private GameObject target;
    private LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 origin = transform.position;
            Vector3 destination = target.transform.position;
            lineRenderer.SetPositions(new Vector3[] {origin, destination});
            lineRenderer.startWidth = .1f;
            Vector3 direction = destination - origin;
            if (Physics.Raycast(origin, direction))
            {
                Debug.Log("Got item!");
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveComponentController : MonoBehaviour
{
    enum WallFloorType { Wall, Floor };
    [SerializeField] WallFloorType type;

    Rigidbody body;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
       /* if (type == WallFloorType.Wall)
        {
            if (body != null)
                body.constraints = RigidbodyConstraints.FreezeAll;
        }*/

    }

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
        }
    }
}

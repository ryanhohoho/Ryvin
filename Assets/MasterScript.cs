using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterScript : MonoBehaviour
{
    public Vector3 gravityDirection = new Vector3(0, -1, 0);
    public float gravityMagnitude; // default gravity

    void Start()
    {
        // set initial gravity direction and magnitude
        Vector3 gravityDirection = new Vector3(0, -1, 0).normalized;
        Physics.gravity = gravityDirection * gravityMagnitude;
    }

    // Update is called once per frame
    void Update()
    {
        //PHYSICS CHANGING CODE
        // update gravity direction and magnitude per tick
        Vector3 gravityDirection = new Vector3(0, -1, 0).normalized;
        Physics.gravity = gravityDirection * gravityMagnitude;
    }
}


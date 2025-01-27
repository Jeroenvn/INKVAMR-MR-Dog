using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{

    private void Ray(){
        RaycastHit hit;
        float distance = 20f;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, distance))
        { 
            Debug.Log("Throw Event");
        }
        else
        { 
            Debug.Log("Reset Ball"); 
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ButtonClicker : MonoBehaviour
{

    public GameObject button;

    public GameObject TouchCollider;

    private bool inCollision = false;

    private Vector3 initial_position;

    // RigidBody from the colliding object
    private Rigidbody colliding_rb;

    private Vector3 button_diff;

    // Start is called before the first frame update
    void Start()
    {
        initial_position = transform.position;

        button_diff = transform.position - button.transform.position;
    }

    void OnCollisionEnter (Collision collision)
    {

        if (collision.gameObject.name == TouchCollider.name)
        {
            inCollision = true;
            colliding_rb = collision.rigidbody;
        }

    }

    void OnCollisionStay (Collision collision)
    {
        if (collision.gameObject.name == TouchCollider.name)
        {
            inCollision = true;
            colliding_rb = collision.rigidbody;
        }

    }
    
    void OnCollisionExit (Collision collision)
    {
        if (collision.gameObject.name == TouchCollider.name)
        {
            inCollision = false;
            colliding_rb = null;
        }
    }


    // Update is called once per frame
     void Update()
    {
    
        button.transform.position = transform.position - button_diff;

        Vector3 position = transform.position;

        if (inCollision) {

            float diff = initial_position.y - position.y;

            if ((diff < 1f) && (diff > 0))
            {
                transform.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 100f, 0);
            }
            else
            {
                transform.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
               
                if (diff >= 1f) {
                    transform.position = new Vector3(initial_position.x, diff+1f , initial_position.z);
                }

            }

        }
        
        if (!inCollision)
        {

            // Get back to initial position
            if (position.y < initial_position.y)
            {
                transform.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 1f, 0);
            }
            // If in initial position, stop moving
            else
            {
                transform.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                transform.position = initial_position;
            }
        }

    
    }
}


using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ButtonClicker : MonoBehaviour
{

    public GameObject button;
    private bool inCollision = false;

    private Vector3 initial_position;

    // RigidBody from the button
    private Rigidbody button_rb;

    // RigidBody from the colliding object
    private Rigidbody colliding_rb;

    // Start is called before the first frame update
    void Start()
    {

        button_rb = button.GetComponent<Rigidbody>();

        if (button_rb != null)
        {
            button_rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        }

        initial_position = button_rb.position;
    }

    void OnCollisionEnter (Collision collision)
    {
        Debug.Log("Collision detected");
        inCollision = true;
        colliding_rb = collision.rigidbody;
    }

    void OnCollisionStay (Collision collision)
    {
        Debug.Log("Collision detected");
        inCollision = true;
        colliding_rb = collision.rigidbody;
    }
    
    void OnCollisionExit (Collision collision)
    {
        Debug.Log("Collision ended");
        inCollision = false;
        colliding_rb = null;
    }


    // Update is called once per frame
     void Update()
    {
    
        if (button_rb != null)
        {
            Vector3 position = button_rb.position;


            //Debug.Log("Initial position: " + initial_position);
            //Debug.Log("Current position: " + position);
            //Debug.Log(initial_position.y - position.y);

            if (inCollision) {
                Debug.Log(true);
            }

            if (inCollision) {

                Debug.Log(initial_position.y - position.y >= 0.05f);

                if (initial_position.y - position.y < 0.05f && initial_position.y - position.y > 0)
                {
                    button_rb.velocity = new Vector3(0, colliding_rb.velocity.y, 0);
                }
                else if (initial_position.y - position.y < 0)
                {
                    button_rb.MovePosition(initial_position);
                    button_rb.velocity = Vector3.zero;
                }
                else if (initial_position.y - position.y >= 0.05f)
                {
                    button_rb.velocity = new Vector3(0, 0, 0);
                    Debug.Log("In collision but too far down");

                }

            }
            
            if (!inCollision)
            {
                // Get back to initial position
                if (position.y < initial_position.y)
                {
                    button_rb.velocity = new Vector3(0, 0.05f, 0);
                }
                // If in initial position, stop moving
                else
                {
                    button_rb.velocity = Vector3.zero;
                    button_rb.MovePosition(initial_position);
                }
            }

        }
    }
   
}


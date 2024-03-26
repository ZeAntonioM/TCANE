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
    public Vector3 initial_position;
    private Rigidbody button_rb;
    private Rigidbody colliding_rb;

    // Start is called before the first frame update
    void Start()
    {
        button_rb = GetComponent<Rigidbody>();
        if (button_rb != null)
        {
            button_rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        }
       

    }

    void OnCollisionEnter (Collision collision)
    {
        inCollision = true;
        colliding_rb = collision.rigidbody;
    }

    void OnCollisionStay (Collision collision)
    {
        colliding_rb = collision.rigidbody;
    }
    
    void OnCollisionExit (Collision collision)
    {
        inCollision = false;
        colliding_rb = null;
    }


    // Update is called once per frame
    void Update()
    {
    
        if (button_rb != null)
        {
            Vector3 position = button_rb.position;


            Debug.Log("Initial position: " + initial_position);
            Debug.Log("Current position: " + position);
            Debug.Log(initial_position.y - position.y);
            Debug.Log(4f);

            // If in collision and not too far down, move down
            if (inCollision && (initial_position.y - position.y < 0.5f))
            {
                button_rb.velocity = new Vector3(0, colliding_rb.velocity.y, 0);
            }
            
            else if (!inCollision)
            {
                // Get back to initial position
                if (position.y < initial_position.y)
                {
                    button_rb.velocity = new Vector3(0, 0.5f, 0);
                }
                // If in initial position, stop moving
                else
                {
                    button_rb.velocity = Vector3.zero;
                    button_rb.MovePosition(initial_position);
                }
            }

            position.x = initial_position.x;
            position.z = initial_position.z;

            button_rb.MovePosition(position);
        }
    }
   


}


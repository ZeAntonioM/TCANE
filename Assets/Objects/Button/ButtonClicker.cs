using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ButtonClicker : MonoBehaviour
{

    public GameObject TouchCollider;

    private bool inCollision = false;

    private Vector3 initial_position;
    private Vector3 bottom_position;
    private GameObject Parent;


    // Start is called before the first frame update
    void Start()
    {

        Debug.Log(transform.position);

        Parent = transform.parent.gameObject;
        initial_position = Parent.transform.position;

        bottom_position = new Vector3(initial_position.x, initial_position.y - 5f, initial_position.z); 

        

    }

    void OnCollisionEnter (Collision collision)
    {

        if (collision.gameObject.name == TouchCollider.name)
        {
            inCollision = true;
        }

    }

    void OnCollisionStay (Collision collision)
    {
        if (collision.gameObject.name == TouchCollider.name)
        {
            inCollision = true;
        }

    }
    
    void OnCollisionExit (Collision collision)
    {

        Debug.Log("Fora");
        if (collision.gameObject.name == TouchCollider.name)
        {
            inCollision = false;
        }
    }


    // Update is called once per frame
     void Update()
    {
        Vector3 position = Parent.transform.position;

        Debug.Log(transform.position);

        if (inCollision) {


            if ((position.y > bottom_position.y) && (initial_position.y > position.y))
            {
                transform.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, -1f, 0);
            }
            else
            {
                transform.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
               
                if (position.y < bottom_position.y) {
                    transform.position = new Vector3(initial_position.x, bottom_position.y, initial_position.z);
                }
            }
        }
        
        if (!inCollision)
        {

            // Get back to initial position
            if (position.y < initial_position.y)
            {
                transform.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 1, 0);
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


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
    private bool clicked = false;


    // Start is called before the first frame update
    void Start()
    {
        Parent = transform.parent.gameObject;
        initial_position = Parent.transform.position;

        bottom_position = new Vector3(initial_position.x, initial_position.y - 0.25f, initial_position.z); 

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
        if (collision.gameObject.name == TouchCollider.name)
        {
            inCollision = false;
        }
    }

    public bool IsClicked()
    {
        return clicked;
    }

    public void SetClicked(bool value)
    {
        clicked = value;
    }


    // Update is called once per frame
     void Update()
    {
        Vector3 position = Parent.transform.position;

        if (inCollision) {

            if ((position.y > bottom_position.y) && (initial_position.y >= position.y))
            {
                Parent.transform.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, -0.5f, 0);
            }
            else
            {
                Parent.transform.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
               
                if (position.y < bottom_position.y) {
                    Parent.transform.position = new Vector3(initial_position.x, bottom_position.y, initial_position.z);
                }

                if (initial_position.y < position.y) {
                    Parent.transform.position = initial_position;
                }
            }
        }
        
        if (!inCollision)
        {

            // Get back to initial position
            if (position.y < initial_position.y)
            {
                Parent.transform.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 2f, 0);
            }
            // If in initial position, stop moving
            else
            {
                Parent.transform.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                Parent.transform.position = initial_position;
            }
        }

        if (Parent.transform.position.y <= bottom_position[1]) {
            clicked = true;
            
        }
    
    }
}


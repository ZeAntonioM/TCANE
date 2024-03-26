using UnityEngine;
using System.Collections;

public class DoorObject : MonoBehaviour
{
    // “other” refers to the collider that is touching this collider
    void OnCollisionEnter (Collision other)
    {
        Debug.Log ("A collider has made contact with the DoorObject Collider");
    }

    void OnCollisionStay (Collision other)
    {
        Debug.Log ("A collider is in contact with the DoorObject Collider");
    }
    
    void OnCollisionExit (Collision other)
    {
        Debug.Log ("A collider has ceased contact with the DoorObject Collider");
    }
}
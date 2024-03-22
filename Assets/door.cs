using UnityEngine;
using System.Collections;

public class DoorObject : MonoBehaviour
{
    // “other” refers to the collider that is touching this collider
    void OnColliderEnter (Collider other)
    {
        Debug.Log ("A collider has made contact with the DoorObject Collider");
    }

    void OnColliderStay (Collider other)
    {
        Debug.Log ("A collider is in contact with the DoorObject Collider");
    }
    
    void OnColliderExit (Collider other)
    {
        Debug.Log ("A collider has ceased contact with the DoorObject Collider");
    }
}
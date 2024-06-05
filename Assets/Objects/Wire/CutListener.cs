using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutListener : MonoBehaviour
{

    public GameObject HapticCollider;
    public GameObject HapticActor;

    private bool isCut = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == HapticCollider)
        {
            if (HapticActor.GetComponent<HapticPlugin>().bIsGrabbing) {
                isCut = true;
                Debug.Log("Cut!");
            }
        }
    }

    public bool IsCut()
    {
        return isCut;
    }

}

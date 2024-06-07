using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutListener : MonoBehaviour
{

    public GameObject HapticCollider;
    public GameObject HapticActor;

    private string challengeType;

    private bool isCut = false;

    // Start is called before the first frame update
    void Start()
    {
        //Checks if the challenge is a VWireChallenge or HWireChallenge

        if (transform.parent.transform.parent.GetComponent<VWireChallenge>() != null)
        {
            challengeType = "VWireChallenge";
        }
        else if (transform.parent.transform.parent.GetComponent<HWireChallenge>() != null)
        {
            challengeType = "HWireChallenge";
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == HapticCollider)
        {
            if (!isCut) {
                if (HapticActor.GetComponent<HapticPlugin>().bIsGrabbing) {
                    isCut = true;
                    Debug.Log("Cut!");
                    
                    if (challengeType == "VWireChallenge")
                    {
                        transform.parent.transform.parent.GetComponent<VWireChallenge>().CutWire(gameObject);
                    }
                    else
                    {
                        transform.parent.transform.parent.GetComponent<HWireChallenge>().CutWire(gameObject);
                    }

                }
            }
        }
    }

    public bool IsCut()
    {
        return isCut;
    }

}

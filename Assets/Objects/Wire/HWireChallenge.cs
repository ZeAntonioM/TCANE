using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HWireChallenge : MonoBehaviour
{

    public GameObject[] wires;
    private GameObject wireToCut;
    public static bool exploded = false;
    public static bool solved = false;


    // Start is called before the first frame update
    void Start()
    {

        int redCount = 0;
        int blueCount = 0;
        int yellowCount = 0;
        int darkCount = 0;
        int whiteCount = 0;
        int greenCount = 0;

        string[] colors = new string[4];
        
        for (int i = 0; i < 4; i++) {

            string color = wires[i].transform.Find("./WireRender").GetComponent<Renderer>().material.name.Split(' ')[0];

            colors[i] = color;

            if (color == "Red") redCount++;
            else if (color == "Blue") blueCount++;
            else if (color == "White") whiteCount++;
            else if (color == "Yellow") yellowCount++;
            else if (color == "Green") greenCount++;
            else if (color == "Dark") darkCount++;

        }

        //Check rules to decide which wire to cut
        if (redCount == 1) {
            if (colors[3] == "Red") wireToCut = wires[3];
            else wireToCut = wires[0];
        }
        else if (blueCount > 1) {

            bool blueCheck = false;

            for (int i = 0; i < 4; i++) {
                if(blueCheck) {
                    wireToCut = wires[i];
                    break;
                }
                blueCheck = true;
            }
        }
        else if (yellowCount == 1) {
            if ((colors[1] == "Green") || (colors[1] == "Blue")) wireToCut = wires[1];
            else wireToCut = wires[2];
        }
        else if (darkCount == 0) wireToCut = wires[2];
        else if (colors[4] == "White") {
            if (colors[1] == "Red") wireToCut = wires[0];
            else wireToCut = wires[4];
        }
        else if (greenCount == 2) {
            for (int i = 0; i < 4; i++) {
                if (colors[i] == "Green") {
                    wireToCut = wires[i];
                    break;
                }
            }
        }
        else if (redCount == 4 || blueCount == 4 || yellowCount == 4 || darkCount == 4 || whiteCount == 4 || greenCount == 4) wireToCut = wires[2];
        else if (colors[0] == colors[1]) wireToCut = wires[1];
        else if (colors[3] == "Red") wireToCut = wires[2];
        else wireToCut = wires[0];

    }

    // Function to be called by CutListener, to tell the script that the wire was cut
    public void CutWire(GameObject wire) {
        if ( !exploded && !solved ) {
            if (wire == wireToCut) {
                solved = true;
            }
            else {
                exploded = true;
            }
        }
    }


    // Update is called once per frame
     void Update()
    {

    }
}


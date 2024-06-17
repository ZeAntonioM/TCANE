using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VWireChallenge : MonoBehaviour
{

    public GameObject[] wires;

    private List<bool> cut_wires = new List<bool>();

    private List<bool> hasBeenCut = new List<bool>(); 
    private int cutCount = 0;

    public bool exploded = false;
    public bool solved = false;

    // Start is called before the first frame update
    void Start()
    {

        int redCount = 0;
        int blueCount = 0;
        int whiteCount = 0;
        int yellowCount = 0;

        for (int i = 0; i < wires.Length ; i++) {

            GameObject wire = wires[i];

            // Initialize hasBeenCut list
            hasBeenCut.Add(false);

            //Check Wire color
            string color = wire.transform.Find("WireRender").GetComponent<Renderer>().material.name.Split(' ')[0];

            // Count the number of wires of each color
            if (color == "Red") redCount++;
            else if (color == "Blue") blueCount++;
            else if (color == "White") whiteCount++;
            else if (color == "Yellow") yellowCount++;

        }

        // Calculates for each wire if it is supposed to be cut or not
        for (int i = 0; i < wires.Length; i++) {

            GameObject wire = wires[i];

            //Check Wire color
            string color = wire.transform.Find("WireRender").GetComponent<Renderer>().material.name.Split(' ')[0];

            // Go through all rules
            if (color == "Red") cut_wires.Add( wires.Length % 2 == 1 ? true : false );
            else if (color == "Blue") cut_wires.Add( i == 2 ? true : false );
            else if (color == "Green") cut_wires.Add( redCount > 2 ? false : true );
            else if (color == "Yellow") cut_wires.Add( wires.Length % 3 == 0 ? true : false );
            else if (color == "White") cut_wires.Add( blueCount >= whiteCount ? true : false );
            else if (color == "Dark") cut_wires.Add( yellowCount == 0 ? true : false );
            
        }

        string log = "Cut wires: ";
        foreach (bool b in cut_wires) {
            log += b + " ";
        }
        Debug.Log(log);

        
    }

    // Function to be called by CutListener, to tell the script that the wire was cut
    public void CutWire(GameObject wire) {
        if ( !exploded && !solved ) {
            
            for(int i = 0; i < wires.Length; i++) {
                if (wire == wires[i]) {
                    
                    hasBeenCut[i] = true;

                    if (!cut_wires[i]) {
                        exploded = true;
                        transform.parent.GetComponent<BombGame>().Explode();
                    }

                    else {
                        cutCount++;
                        if (cutCount == wires.Length) {
                            solved = true;
                            Debug.Log("Solved!");
                        }
                    }

                    break;
                }
            }

            
        }
    }

    // Update is called once per frame
     void Update()
    {

    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombGame : MonoBehaviour
{

    public static bool exploded = false;
    public static bool solved = false;

    private List<Component> challenges = new List<Component>();

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        
        foreach (Transform child in transform)
        {
            
            ButtonChallenge buttonChallenge = child.GetComponent<ButtonChallenge>();
            HWireChallenge hwireChallenge = child.GetComponent<HWireChallenge>();
            VWireChallenge vwireChallenge = child.GetComponent<VWireChallenge>();

            if (buttonChallenge != null)
            {
                challenges.Add(buttonChallenge);
            }
            else if (hwireChallenge != null)
            {
                challenges.Add(hwireChallenge);
            }
            else if (vwireChallenge != null)
            {
                challenges.Add(vwireChallenge);
            }
            
        }
    }

    public void Explode()
    {
        exploded = true;
    }

    public bool isSolved()
    {
        return solved;
    }

    // Update is called once per frame
    void Update()
    {
        int solvedCount = 0;
        foreach (Component challenge in challenges)
        {
            if (challenge is ButtonChallenge challenge1)
            {
                if (challenge1.solved)
                {
                    solvedCount++;
                }
            }
            else if (challenge is HWireChallenge challenge2)
            {
                if (challenge2.solved)
                {
                    solvedCount++;
                }
            }
            else if (challenge is VWireChallenge challenge3)
            {
                if (challenge3.solved)
                {
                    solvedCount++;
                }
            }
        }

        Debug.Log("Solved: " + solvedCount + " out of " + challenges.Count);

        if (solvedCount == challenges.Count)
        {
            solved = true;
        }


        if (exploded) {
            Time.timeScale = 0;
        }


    }
}

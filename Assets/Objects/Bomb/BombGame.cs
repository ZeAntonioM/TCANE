using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombGame : MonoBehaviour
{

    public static bool exploded = false;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    public void Explode()
    {
        exploded = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (exploded) {
            Time.timeScale = 0;
        }


    }
}

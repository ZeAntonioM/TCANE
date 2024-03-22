using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClicker : MonoBehaviour
{

    public GameObject button;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<GameObject>();
        
        Collision collision = button.GetComponent<Collision>();
    

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                if (hit.collider.gameObject == button) {
                    Click();
                }
            }
        }

    }

   public void Click() {
       Debug.Log("Button Clicked");
   }


}


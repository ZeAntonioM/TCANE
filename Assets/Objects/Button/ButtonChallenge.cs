using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;

public class ButtonChallenge : MonoBehaviour
{

    public GameObject[] Buttons;
    private GameObject[] colliders;
    public int[] correct_order;
    private int n_clicked_buttons;
    private List<GameObject> clicked_buttons = new List<GameObject>();
    private string color_top_left; 
    private string color_top_right;
    private string color_bottom_left;
    private string color_bottom_right;
    public bool exploded = false;
    public bool solved = false;

    // Start is called before the first frame update
    void Start()
    {

        n_clicked_buttons = 0;

        // Obter as cores dos botões
        color_top_left = Buttons[0].GetComponent<Renderer>().material.name.Split(' ')[0];
        color_top_right = Buttons[1].GetComponent<Renderer>().material.name.Split(' ')[0];
        color_bottom_left = Buttons[2].GetComponent<Renderer>().material.name.Split(' ')[0];
        color_bottom_right = Buttons[3].GetComponent<Renderer>().material.name.Split(' ')[0];

        // Decidir a ordem em que os botões devem ser pressionados
        if (color_top_left == "Red" || color_top_right == "Red" || color_bottom_left == "Red" || color_bottom_right == "Red") {

            correct_order = new int[] {2, 1, 3, 4};

        }
        
        else if (color_top_left == "Dark") {
            
            string[] order = {"Dark", "Green", "White", "Red", "Yellow", "Blue"};
            Sort_Buttons(order);

        }

        else if (color_bottom_right == "Yellow") {

            correct_order = new int[] {3, 4, 2, 1};
        
        }

        else if (color_top_right == "White") {

            string[] order = {"Red", "Dark", "White", "Blue", "Green", "Yellow"};
            Sort_Buttons(order);

        }

        else if (color_bottom_left == "Dark") {

            string[] order = {"Green", "Yellow", "Dark", "Red", "Blue", "White"};
            Sort_Buttons(order);

        }

        else {

            correct_order = new int[] {1, 2, 3, 4};

        }

        Debug.Log("Correct order: " + correct_order[0] + " " + correct_order[1] + " " + correct_order[2] + " " + correct_order[3]);


        // Encontrar os colliders dos botões
        colliders = new GameObject[4];
        for (int i = 0; i < 4; i++) {

            if (Buttons[i] == null) {
                Debug.Log("Button " + i + " is null");
            }
            else {
                colliders[i] = Buttons[i].transform.Find("Collider").gameObject;
            }

        }



    }

    void Sort_Buttons(string[] colors) {

        int[] sorted_buttons = new int[4];
        int counter = 0;

        for (int i = 0; i < 6; i++) {

            if (counter == 4) {
                break;
            }

            if (color_top_left == colors[i]) {
                sorted_buttons[counter] = 1;
                counter++;
            }

            if (color_top_right == colors[i]) {
                sorted_buttons[counter] = 2;
                counter++;
            }

            if (color_bottom_left == colors[i]) {
                sorted_buttons[counter] = 3;
                counter++;
            }

            if (color_bottom_right == colors[i]) {
                sorted_buttons[counter] = 4;
                counter++;
            }

        }

        correct_order = sorted_buttons;

    }

    void set_greenLight(GameObject button) {

        GameObject light = button.GetNamedChild("Light");
        light.GetComponent<Renderer>().material = Resources.Load("Color Materials/GreenLight", typeof(Material)) as Material;

    }


    // Update is called once per frame
    void Update()
    {
        
        // Go though all the buttons and check if they were clicked. 
        // If a button has been clicked before, ignore it.
        for (int i = 0; i < 4; i++) {

            GameObject button = Buttons[i];

            if (clicked_buttons.Contains(button)) {
                continue;
            }

            if (colliders[i].GetComponent<ButtonClicker>().IsClicked()) {

                Debug.Log("Button " + (i+1) + " clicked");

                if (i+1 == correct_order[n_clicked_buttons]) {
                    clicked_buttons.Add(button);
                    n_clicked_buttons++;
                    set_greenLight(button);
                    if (n_clicked_buttons == 4) {
                        solved = true;
                    }
                }
                else {
                    exploded = true;
                    transform.parent.GetComponent<BombGame>().Explode();
                }

                break;

            }

        }

    }

}

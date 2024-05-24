using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;

public class ButtonChallenge : MonoBehaviour
{

    public GameObject[] Buttons;
    public static bool exploded = false;
    private GameObject[] colliders;
    public static int[] correct_order;
    public static int clicked_buttons;
    private string color_top_left; 
    private string color_top_right;
    private string color_bottom_left;
    private string color_bottom_right;

    // Start is called before the first frame update
    void Start()
    {

        clicked_buttons = 0;

        // Obter as cores dos botões
        color_top_left = Buttons[0].GetNamedChild("button").GetComponent<Renderer>().material.name.Split(' ')[0];
        color_top_right = Buttons[1].GetNamedChild("button").GetComponent<Renderer>().material.name.Split(' ')[0];
        color_bottom_left = Buttons[2].GetNamedChild("button").GetComponent<Renderer>().material.name.Split(' ')[0];
        color_bottom_right = Buttons[3].GetNamedChild("button").GetComponent<Renderer>().material.name.Split(' ')[0];

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


        // Encontrar os colliders dos botões
        colliders = new GameObject[4];
        for (int i = 0; i < 4; i++) {

            if (Buttons[i] == null) {
                Debug.Log("Button " + i + " is null");
            }
            else {
                colliders[i] = Buttons[i].transform.Find("Collider").gameObject;
                Debug.Log("Collider " + i + " is " + colliders[i].name);
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
        light.GetComponent<Renderer>().material = Resources.Load("Materials/GreenLight", typeof(Material)) as Material;

    }

    // Update is called once per frame
    void Update()
    {
        
        // Go though all the buttons and check if they were clicked
        for (int i = 0; i < 4; i++) {

            GameObject button = Buttons[i];

            if (colliders[i].GetComponent<ButtonClicker>().isClicked()) {

                if (i+1 == correct_order[clicked_buttons]) {
                    clicked_buttons++;
                    set_greenLight(button);
                }
                else {
                    exploded = true;
                    Debug.Log("Wrong button clicked");
                }

                break;

            }

        }

    }

}

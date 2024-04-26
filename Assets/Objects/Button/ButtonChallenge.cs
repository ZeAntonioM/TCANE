using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class ButtonChallenge : MonoBehaviour
{

    public GameObject Top_Left_Button;

    public GameObject Top_Right_Button;

    public GameObject Bottom_Left_Button;

    public GameObject Bottom_Right_Button;

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
        color_top_left = Top_Left_Button.GetNamedChild("button").GetComponent<Renderer>().material.name.Split(' ')[0];
        color_top_right = Top_Right_Button.GetNamedChild("button").GetComponent<Renderer>().material.name.Split(' ')[0];
        color_bottom_left = Bottom_Left_Button.GetNamedChild("button").GetComponent<Renderer>().material.name.Split(' ')[0];
        color_bottom_right = Bottom_Right_Button.GetNamedChild("button").GetComponent<Renderer>().material.name.Split(' ')[0];

        // Decidir a ordem em que os botões devem ser pressionados
        // um botão vermelho
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

        Debug.Log(correct_order[0]+ " " + correct_order[1]+ " " + correct_order[2]+ " " + correct_order[3]);

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

}

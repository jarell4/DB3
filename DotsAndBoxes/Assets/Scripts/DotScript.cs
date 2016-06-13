﻿using UnityEngine;
using System;
using System.Collections;

public class DotScript : MonoBehaviour {

    #region Vars

    public Color defaultCol = new Color(1, 1, 1);

    #endregion

    #region Unity Methods
    // Use this for initialization
    void Start()
    {
		this.gameObject.GetComponent<MeshRenderer>().enabled = true;
        this.gameObject.GetComponent<MeshRenderer>().material.color = defaultCol;
    }

    // Called when the user has pressed the mouse button while over the collider
    void OnMouseDown()
    {
        //if this is first dot to be clicked on by user
        if (MainGameScript.First_Dot)
        {
			//play audio
			AudioScript.PlayAudio("dot");

            //save first dot in main script
            MainGameScript.Dot_One = this.gameObject;

            //color first dot the current player's color, so user knows it was clicked
            this.gameObject.GetComponent<MeshRenderer>().material.color = 
                MainGameScript._Players[Convert.ToInt32(MainGameScript.Player_2_Turn)];

            //sets FirstDot bool to false, so next dot will execute the contents of the else statement
            MainGameScript.First_Dot = false;
        }

        //if this is the second dot to be clicked on by user
        else
        {
            //save second dot in main script
            MainGameScript.Dot_Two = this.gameObject;

            //save the names of two last dots from main script
            string firstDotName = MainGameScript.Dot_One.name;
            string secondDotName = MainGameScript.Dot_Two.name;

			//save integers of firstDotNames for line coordinates
			int [] firstDot = new int[]{(int)Char.GetNumericValue(firstDotName[1]), 
				(int)Char.GetNumericValue(firstDotName[2]), 
				(int)Char.GetNumericValue(firstDotName[3])};

			//save integers of secondDotNames for line coordinates
			int [] secondDot = new int[]{(int)Char.GetNumericValue(secondDotName[1]), 
				(int)Char.GetNumericValue(secondDotName[2]), 
				(int)Char.GetNumericValue(secondDotName[3])};

            //checks if the dots have the same Y and Z values, which means they will draw a X line
            if (firstDot[1] == secondDot[1] && firstDot[2] == secondDot[2])
            {
                //Determines which X line is drawn based on dot's X position relative to one another
				int difference = firstDot[0] - secondDot[0];
                if (difference == -1)
                {
					if(MainGameScript.UpdateLine('X', firstDot[0], firstDot[1], firstDot[2]))
						Debug.Log("Draw line: " + 'X' + firstDotName[1] + firstDotName[2] + firstDotName[3]);
                }
                else if (difference == 1)
                {
                    if(MainGameScript.UpdateLine('X', secondDot[0], secondDot[1], secondDot[2]))
						Debug.Log("Draw line: " + 'X' + secondDotName[1] + secondDotName[2] + secondDotName[3]);
                }
            }
            //checks if the dots have the same X and Z values, which means they will draw a Y line
            else if (firstDot[0] == secondDot[0] && firstDot[2] == secondDot[2])
            {
                //Determines which Y line is drawn based on dot's Y position relative to one another
                int difference = firstDot[1] - secondDot[1];
                if (difference == -1)
                {
                    if(MainGameScript.UpdateLine('Y', firstDot[0], firstDot[1], firstDot[2]))
						Debug.Log("Draw line: " + 'Y' + firstDotName[1] + firstDotName[2] + firstDotName[3]);
                }
                else if (difference == 1)
                {
					if(MainGameScript.UpdateLine('Y', secondDot[0], secondDot[1], secondDot[2]))
						Debug.Log("Draw line: " + 'Y' + secondDotName[1] + secondDotName[2] + secondDotName[3]);
                }
            }
            //checks if the dots have the same X and Y values, which means they will draw a Z line
            else if (firstDot[0] == secondDot[0] && firstDot[1] == secondDot[1])
            {
                //Determines which Z line is drawn based on dot's Z position relative to one another
                int difference = firstDot[2] - secondDot[2];
                if (difference == -1)
                {
                    if(MainGameScript.UpdateLine('Z', firstDot[0], firstDot[1], firstDot[2]))
						Debug.Log("Draw line: " + 'Z' + firstDotName[1] + firstDotName[2] + firstDotName[3]);
                }
                else if (difference == 1)
                {
					if(MainGameScript.UpdateLine('Z', secondDot[0], secondDot[1], secondDot[2]))
						Debug.Log("Draw line: " + 'Z' + secondDotName[1] + secondDotName[2] + secondDotName[3]);
                }
            }
            //regardless of whether a valid line could be drawn, dots are reset, and player must select new first dot
            MainGameScript.Dot_One.gameObject.GetComponent<MeshRenderer>().material.color = defaultCol;
            MainGameScript.First_Dot = true;
        }
    }
    #endregion

    #region Private Methods
    #endregion

    #region Public Methods
    #endregion
}

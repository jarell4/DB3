using UnityEngine;
using System;
using System.Collections;

public class BoxScript : MonoBehaviour {

    #region Vars
    #endregion

    #region Unity Methods
    // Use this for initialization
    void Start()
    {
		this.GetComponentsInChildren<MeshRenderer>()[0].enabled = false;
    }
    #endregion

    #region Private Methods
    #endregion

    #region Public Methods
    public static void Draw(int i, int j, int k)
    {
		//play audio
		AudioScript.PlayAudio("box");

		MainGameScript.Grid_Boxes[i, j, k].GetComponentsInChildren<MeshRenderer>()[0].material.color = 
            MainGameScript._Players[Convert.ToInt32(MainGameScript.Player_2_Turn)];

		MainGameScript.Grid_Boxes[i, j, k].GetComponentsInChildren<MeshRenderer>()[0].enabled = true;
    }
    #endregion
}

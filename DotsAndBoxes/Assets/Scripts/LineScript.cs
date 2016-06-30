using UnityEngine;
using System.Collections;

public class LineScript : MonoBehaviour {

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
    
	public static void Draw(char objType, int i, int j, int k)
    {
		//play audio
		AudioScript.PlayAudio("line");

        GameObject drawnLine = null;

        if (objType == 'X')
            drawnLine = MainGameScript.Grid_LinesX[i, j, k];
        else if (objType == 'Y')
            drawnLine = MainGameScript.Grid_LinesY[i, j, k];
        else if (objType == 'Z')
            drawnLine = MainGameScript.Grid_LinesZ[i, j, k];

		drawnLine.GetComponentsInChildren<MeshRenderer>()[0].enabled = true;

        if (objType == 'X')
        {
            if (j == 0)
            {
                if (k == 0)
                {
                    if (!MainGameScript.UpdateBox(i, j, k))
                        MainGameScript.SwitchPlayer();
                }
                else if (k == MainGameScript.GRID_SIZE)
                {
                    if (!MainGameScript.UpdateBox(i, j, k - 1))
                        MainGameScript.SwitchPlayer();
                }
                else
                {
                    if (!MainGameScript.UpdateBox(i, j, k - 1) & 
                    !MainGameScript.UpdateBox(i, j, k))
                        MainGameScript.SwitchPlayer();
                }
            }

            else if (j == MainGameScript.GRID_SIZE)
            {
                if (k == 0)
                {
                    if (!MainGameScript.UpdateBox(i, j - 1, k))
                        MainGameScript.SwitchPlayer();
                }
                else if (k == MainGameScript.GRID_SIZE)
                {
                    if (!MainGameScript.UpdateBox(i, j - 1, k - 1))
                        MainGameScript.SwitchPlayer();
                }
                else
                {
                    if (!MainGameScript.UpdateBox(i, j - 1, k - 1) & 
                    !MainGameScript.UpdateBox(i, j - 1, k))
                        MainGameScript.SwitchPlayer();
                }
            }
            else
            {
                if (k == 0)
                {
                    if (!MainGameScript.UpdateBox(i, j - 1, k) &
                    !MainGameScript.UpdateBox(i, j, k))
                        MainGameScript.SwitchPlayer();
                }
                else if (k == MainGameScript.GRID_SIZE)
                {
                    if (!MainGameScript.UpdateBox(i, j - 1, k - 1) &
                    !MainGameScript.UpdateBox(i, j, k - 1))
                        MainGameScript.SwitchPlayer();
                }
                else
                {
                    if (!MainGameScript.UpdateBox(i, j - 1, k - 1) &
                    !MainGameScript.UpdateBox(i, j - 1, k) &
                    !MainGameScript.UpdateBox(i, j, k - 1) &
                    !MainGameScript.UpdateBox(i, j, k))
                        MainGameScript.SwitchPlayer();
                }
            }
        }

        else if (objType == 'Y')
        {
            if (i == 0)
            {
                if (k == 0)
                {
                    if(!MainGameScript.UpdateBox(i, j, k))
                        MainGameScript.SwitchPlayer();
                }
                else if (k == MainGameScript.GRID_SIZE)
                {
                    if(!MainGameScript.UpdateBox(i, j, k - 1))
                        MainGameScript.SwitchPlayer();
                }
                else
                {
                    if(!MainGameScript.UpdateBox(i, j, k - 1) &
                    !MainGameScript.UpdateBox(i, j, k))
                        MainGameScript.SwitchPlayer();
                }
            }

            else if (i == MainGameScript.GRID_SIZE)
            {
                if (k == 0)
                {
                    if(!MainGameScript.UpdateBox(i - 1, j, k))
                        MainGameScript.SwitchPlayer();
                }
                else if (k == MainGameScript.GRID_SIZE)
                {
                    if(!MainGameScript.UpdateBox(i - 1, j, k - 1))
                        MainGameScript.SwitchPlayer();
                }
                else
                {
                    if(!MainGameScript.UpdateBox(i - 1, j, k - 1) &
                    !MainGameScript.UpdateBox(i - 1, j, k))
                        MainGameScript.SwitchPlayer();
                }
            }
            else
            {
                if (k == 0)
                {
                    if(!MainGameScript.UpdateBox(i - 1, j, k) &
                    !MainGameScript.UpdateBox(i, j, k))
                        MainGameScript.SwitchPlayer();
                }
                else if (k == MainGameScript.GRID_SIZE)
                {
                    if(!MainGameScript.UpdateBox(i - 1, j, k - 1) &
                    !MainGameScript.UpdateBox(i, j, k - 1))
                        MainGameScript.SwitchPlayer();
                }
                else
                {
                    if(!MainGameScript.UpdateBox(i - 1, j, k - 1) &
                    !MainGameScript.UpdateBox(i - 1, j, k) &
                    !MainGameScript.UpdateBox(i, j, k - 1) &
                    !MainGameScript.UpdateBox(i, j, k))
                        MainGameScript.SwitchPlayer();
                }
            }
        }

        else if (objType == 'Z')
        {
            if (i == 0)
            {
                if (j == 0)
                {
                    if(!MainGameScript.UpdateBox(i, j, k))
                        MainGameScript.SwitchPlayer();
                }
                else if (j == MainGameScript.GRID_SIZE)
                {
                    if(!MainGameScript.UpdateBox(i, j - 1, k))
                        MainGameScript.SwitchPlayer();
                }
                else
                {
                    if(!MainGameScript.UpdateBox(i, j - 1, k) &
                    !MainGameScript.UpdateBox(i, j, k))
                        MainGameScript.SwitchPlayer();
                }
            }

            else if (i == MainGameScript.GRID_SIZE)
            {
                if (j == 0)
                {
                    if(!MainGameScript.UpdateBox(i - 1, j, k))
                        MainGameScript.SwitchPlayer();
                }
                else if (j == MainGameScript.GRID_SIZE)
                {
                    if(!MainGameScript.UpdateBox(i - 1, j - 1, k))
                        MainGameScript.SwitchPlayer();
                }
                else
                {
                    if(!MainGameScript.UpdateBox(i - 1, j - 1, k) &
                    !MainGameScript.UpdateBox(i - 1, j, k))
                        MainGameScript.SwitchPlayer();
                }
            }
            else
            {
                if (j == 0)
                {
                    if(!MainGameScript.UpdateBox(i - 1, j, k) &
                    !MainGameScript.UpdateBox(i, j, k))
                        MainGameScript.SwitchPlayer();
                }
                else if (j == MainGameScript.GRID_SIZE)
                {
                    if(!MainGameScript.UpdateBox(i - 1, j - 1, k) &
                    !MainGameScript.UpdateBox(i, j - 1, k))
                    MainGameScript.SwitchPlayer();
                }
                else
                {
                    if(!MainGameScript.UpdateBox(i - 1, j - 1, k) &
                    !MainGameScript.UpdateBox(i - 1, j, k) &
                    !MainGameScript.UpdateBox(i, j - 1, k) &
                    !MainGameScript.UpdateBox(i, j, k))
                    MainGameScript.SwitchPlayer();
                }
            }
        }
	}
    #endregion
}

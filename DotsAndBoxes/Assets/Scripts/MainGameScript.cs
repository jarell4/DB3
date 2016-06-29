using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class MainGameScript : MonoBehaviour
{

    #region Vars
	//UIVars
	public GameObject[] endGameUI;
    public GameObject[] confirmUI;

    //OrganizeVars
    public GameObject cubeParent;

    //GridSizeVars
    public int initGridSize = 3;  //Temporary var, for testing without start menu DELETE
    public static int GRID_SIZE;

    //PlayerVars
	public static bool Player_2_Turn = false;
    public static Color[] _Players = new Color[2];

	//BGVars
	public static Color[] BG_Colors = new Color[2];
	public static int BG_Index = 0;
	public static Camera Main_Cam;

    //ScoreVars
    private static int[] _Score = { 0, 0 };

    //GameCountingVars
    private static int unclaimedBoxes;
    public static int[,,] Box_Use_Array;

    //DotVars
    public GameObject dotPrefab;
    public float dotSize = 5;
    public float dotInit = -1.25f;
    public static GameObject[, ,] Grid_Dots;
    public static bool First_Dot = true;
    public static GameObject Dot_One;
    public static GameObject Dot_Two;
    public static float DOT_BASE_SCALE;

    //LineVars
    public GameObject linePrefab;
    public float lineSizeLong = 2;
    public float lineSizeShort = 0.2f;
    public float lineInit = 0;
    public float lineInitOffset = -1.25f;
    public static GameObject[, ,] Grid_LinesX;
    public static GameObject[, ,] Grid_LinesY;
    public static GameObject[, ,] Grid_LinesZ;
    public static string currLine;

    //BoxVars
    public GameObject boxPrefab;
    public float boxSize = 5;
    public float boxInitX = 0;
    public float boxInitY = 0;
    public float boxInitZ = 0;
    public static GameObject[,,] Grid_Boxes;
    #endregion

    #region Unity Methods
	// First function that happens
	void Awake()
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			Application.targetFrameRate = 48;
		}
	}
    // Use this for initialization
    void Start()
    {
		GRID_SIZE = ButtonScript.Selected_Size;
        Box_Use_Array = new int[GRID_SIZE, GRID_SIZE, GRID_SIZE];
		unclaimedBoxes = GRID_SIZE * GRID_SIZE * GRID_SIZE;
        DOT_BASE_SCALE = 1 / (float)GRID_SIZE;
        
        InitColors();
        InitGridArrays();
        InitGameObjs();

		Main_Cam = GameObject.Find("Main Camera").GetComponent<Camera>();
		Main_Cam.backgroundColor = BG_Colors[BG_Index];

        //for (int x = 0; x < Grid_LinesY.GetLength(0); x += 1)
        //{
        //    for (int y = 0; y < Grid_LinesY.GetLength(1); y += 1)
        //    {
        //        for (int z = 0; z < Grid_LinesY.GetLength(2); z += 1)
        //        {
        //            Debug.Log(Grid_LinesY[x, y, z].name);
        //        }
        //    }
        //}
    }
    #endregion

    #region Private Methods
    private void InitColors()
    {
		_Players[0] = MainColorScript.HsvToRgb(MainColorScript.P1_Hue, 0.9f, 0.7f);
		_Players[1] = MainColorScript.HsvToRgb(MainColorScript.P2_Hue, 0.9f, 0.7f);

		BG_Colors[0] = MainColorScript.HsvToRgb(MainColorScript.P1_Hue, 0.1f, 0.7f);
		BG_Colors[1] = MainColorScript.HsvToRgb(MainColorScript.P2_Hue, 0.1f, 0.7f);
    }
    //Initialize grid arrays, to be sized according to grid size
    private void InitGridArrays()
    {
        Grid_Dots = new GameObject[GRID_SIZE + 1, GRID_SIZE + 1, GRID_SIZE + 1];
        Grid_LinesX = new GameObject[GRID_SIZE, GRID_SIZE + 1, GRID_SIZE + 1];
        Grid_LinesY = new GameObject[GRID_SIZE + 1, GRID_SIZE, GRID_SIZE + 1];
        Grid_LinesZ = new GameObject[GRID_SIZE + 1, GRID_SIZE + 1, GRID_SIZE];
        Grid_Boxes = new GameObject[GRID_SIZE, GRID_SIZE, GRID_SIZE];
    }
    //Initialize game object prefabs by calling their respective methods
    private void InitGameObjs()
    {
        InitDots();
        InitLinesX();
        InitLinesY();
        InitLinesZ();
        InitBoxes();
    }

    //Initialize dot prefabs
    private void InitDots()
    {
        //sets transform variables
        float size = dotSize / (float)GRID_SIZE;
        float scale = 1 / (float)GRID_SIZE;

        //calls ForLoopCreate with specific inputs
         ForLoopCreate
            (Grid_Dots, dotPrefab,
            dotInit, dotInit, dotInit, 
            size, size, size,
            scale, scale, scale,
            Grid_Dots.Length, 'D');
    }

    //Initialize line prefabs with primary axis X
    private void InitLinesX()
    {
        //sets transform variables
        float posStart = lineInit - (1f - 1.9f / (float)GRID_SIZE);
        float posOffset = lineInitOffset * 2f / (float)GRID_SIZE;
        float delta = lineSizeLong * 2.5f / (float)GRID_SIZE;
        float scaleLong = lineSizeLong * 2f / (float)GRID_SIZE;
        float scaleShort = lineSizeShort * 0.5f / (float)GRID_SIZE;

        //calls ForLoopCreate with specific inputs
        ForLoopCreate
            (Grid_LinesX, linePrefab,
            posStart, posStart + posOffset, posStart + posOffset,
            delta, delta, delta,
            scaleLong, scaleShort, scaleShort,
            Grid_LinesX.Length, 'X');
    }

    //Initialize line prefabs with primary axis Y
    private void InitLinesY()
    {
        //sets transform variables
        float posStart = lineInit - (1f - 1.9f / (float)GRID_SIZE);
        float posOffset = lineInitOffset * 2f / (float)GRID_SIZE;
        float delta = lineSizeLong * 2.5f / (float)GRID_SIZE;
        float scaleLong = lineSizeLong * 2f / (float)GRID_SIZE;
        float scaleShort = lineSizeShort * 0.5f / (float)GRID_SIZE;

        //calls ForLoopCreate with specific inputs
        ForLoopCreate
            (Grid_LinesY, linePrefab,
            posStart + posOffset, posStart, posStart + posOffset,
            delta, delta, delta,
            scaleShort, scaleLong, scaleShort,
            Grid_LinesY.Length, 'Y');
    }

    //Initialize line prefabs with primary axis Z
    private void InitLinesZ()
    {
        //sets transform variables
        float posStart = lineInit - (1f - 1.9f / (float)GRID_SIZE);
        float posOffset = lineInitOffset * 2f / (float)GRID_SIZE;
        float delta = lineSizeLong * 2.5f / (float)GRID_SIZE;
        float scaleLong = lineSizeLong * 2f / (float)GRID_SIZE;
        float scaleShort = lineSizeShort * 0.5f / (float)GRID_SIZE;

        //calls ForLoopCreate with specific inputs
        ForLoopCreate
            (Grid_LinesZ, linePrefab,
            posStart + posOffset, posStart + posOffset, posStart,
            delta, delta, delta,
            scaleShort, scaleShort, scaleLong,
            Grid_LinesZ.Length, 'Z');
    }

    //Initialize box prefabs
    private void InitBoxes()
    {
        //sets transform variables
        float size = boxSize / (float)GRID_SIZE;
        float locOffset = 1f - 2f / (float)GRID_SIZE;
        float scale = 2f / (float)GRID_SIZE;

        //calls ForLoopCreate with specific inputs
        ForLoopCreate
            (Grid_Boxes, boxPrefab,
            boxInitX - locOffset, boxInitY - locOffset, boxInitZ - locOffset, 
            size, size, size, 
            scale, scale, scale,
            Grid_Boxes.Length, 'B');
    }

    //Method used by initDots, initBoxes, and initLines for instancing and placement
    private void ForLoopCreate
        (GameObject[, ,] objArray, GameObject objPrefab,
        float xLoc, float yLoc, float zLoc, 
        float xDelta, float yDelta, float zDelta, 
        float xScale, float yScale, float zScale,
        int total, char objType)
    {
        float initXLoc = xLoc;
        float initYLoc = yLoc;
        float initZLoc = zLoc;
        
        int i = 0;
        int j = 0;
        int k = 0;

        for (int count = 0; count < total; count++ )
        {

            if (j >= objArray.GetUpperBound(1) && i > objArray.GetUpperBound(0))
            {
                //sets initial position of objects for next Z value
                xLoc = initXLoc;
                i = 0;
                yLoc = initYLoc;
                j = 0;
                zLoc += zDelta;
                k++;
            }

            if (i > objArray.GetUpperBound(0))
            {
                //sets initial position of objects for next Y value
                xLoc = initXLoc;
                i = 0;
                yLoc += yDelta;
                j++;
            }
            objArray[i, j, k] = Instantiate(objPrefab);

            //temp var for initializing game object
            GameObject currObj = objArray[i, j, k];

            currObj.transform.localScale = new Vector3(xScale, yScale, zScale);
            currObj.transform.position = new Vector3(xLoc, yLoc, zLoc);
            currObj.transform.parent = cubeParent.transform;
            currObj.name = objType + i.ToString() + j.ToString() + k.ToString();

            ////sets initial position of objects for next X value
            i++;
            xLoc += xDelta;
        }
    }

    //Called when a box is drawn, increments the score of the current player
    private static void UpdateScore()
    {
        _Score[Convert.ToInt32(MainGameScript.Player_2_Turn)]++;
    }

    //Called when a box is drawn, decrements unclaimedBoxes count, and checks for endgame
    private static void UpdateCount()
    {
        unclaimedBoxes--;
        if (unclaimedBoxes <= 0)
        {
            EndGame();
        }
    }

    //Called if there are no more unclaimed boxes
    private static void EndGame()
    {
        if (_Score[0] > _Score[1])
            Debug.Log("Game Over: Winner = P1");
        else if (_Score[0] < _Score[1])
            Debug.Log("Game Over: Winner = P2");
        else
            Debug.Log("Game Over: Tie");

		HideDots();
		HideLines();
		DisplayEndUI();
    }

	//Called by EndGame()
	private static void DisplayEndUI()
	{
		foreach (GameObject go in GameObject.Find("mainGameHandler").GetComponent<MainGameScript>().endGameUI)
			go.GetComponent<Image>().enabled = true;
	}

	//Called by EndGame() to hide dot objects on completion
	private static void HideDots()
	{
		foreach(GameObject go in Grid_Dots)
			go.GetComponent<MeshRenderer>().enabled = false;
	}

	//Called by EndGame() to hide line objects on completion
	private static void HideLines()
	{
		foreach(GameObject go in Grid_LinesX)
			go.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;

		foreach(GameObject go in Grid_LinesY)
			go.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;

		foreach(GameObject go in Grid_LinesZ)
			go.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
	}
    #endregion

    #region Public Methods
    public static void DisplayConfirmUI()
    {
        foreach (GameObject go in GameObject.Find("mainGameHandler").GetComponent<MainGameScript>().confirmUI)
            go.GetComponent<Image>().enabled = true;
    }

    public static void ConfirmLine()
    {

		Debug.Log("Attempting to confirm line: " + currLine.ToString());

        char axis = Convert.ToChar(currLine[0]);

		//Left these in just to show debug lines, can delete.
        int oldX = Convert.ToInt32(currLine[1]);
		Debug.Log("Passing in: " + Convert.ToInt32(currLine[1]).ToString() + " to update line.");
        int oldY = Convert.ToInt32(currLine[2]);
		Debug.Log("Passing in: " + Convert.ToInt32(currLine[2]).ToString() + " to update line.");
        int oldZ = Convert.ToInt32(currLine[3]);
		Debug.Log("Passing in: " + Convert.ToInt32(currLine[3]).ToString() + " to update line.");

		//Funtional line values, function to get numeric value of ASCII characters, then force int
		int x = (int)Char.GetNumericValue(currLine[1]);
		int y = (int)Char.GetNumericValue(currLine[2]);
		int z = (int)Char.GetNumericValue(currLine[3]);

        if (UpdateLine(axis, x, y, z))
            Debug.Log("Draw line: " + currLine);
    }

	public static bool UpdateLine(char objType, int i, int j, int k)
    {
		GameObject drawnLine = null;

		if (objType == 'X')
			drawnLine = MainGameScript.Grid_LinesX[i, j, k];
		else if (objType == 'Y')
			drawnLine = MainGameScript.Grid_LinesY[i, j, k];
		else if (objType == 'Z')
			drawnLine = MainGameScript.Grid_LinesZ[i, j, k];
		
		if(drawnLine.GetComponentsInChildren<MeshRenderer>()[0].enabled == false)
		{
			LineScript.Draw(objType, i, j, k);
			return true;
		}
		else
			return false;
    }

    //Called by LineScript to increment count of lines drawn around box. If 12, draws new box and returns true
    public static bool UpdateBox(int i, int j, int k)
    {
        Box_Use_Array[i, j, k]++;

        if (Box_Use_Array[i, j, k] == 12)
        {
            Debug.Log("Draw box: " + 'B' + i.ToString() + j.ToString() + k.ToString());
            BoxScript.Draw(i, j, k);
            UpdateScore();
            UpdateCount();
            return true;
        }
        else
            return false;
    }

    //Called by LineScript to switch current player should no boxes be claimed
    public static void SwitchPlayer()
    {
        Player_2_Turn = !Player_2_Turn;

		if (BG_Index == 0)
			BG_Index = 1;
		else if(BG_Index ==1)
			BG_Index = 0;
		
		Main_Cam.backgroundColor = BG_Colors[BG_Index];
    }

    //Called by DotScipt to reset the dots
    public static void ResetDots()
    {
        foreach (GameObject currentDot in Grid_Dots)
        {
            currentDot.transform.localScale = new Vector3((DOT_BASE_SCALE), (DOT_BASE_SCALE), (DOT_BASE_SCALE));
            currentDot.gameObject.GetComponent<MeshRenderer>().material.color = DotScript.defaultCol;
            currentDot.gameObject.GetComponent<SphereCollider>().enabled = true;
        }
        First_Dot = true;
    }
    #endregion
}

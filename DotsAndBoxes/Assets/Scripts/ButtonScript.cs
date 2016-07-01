using UnityEngine;
using UnityEngine.SceneManagement;//Additional using directory, "SceneManagement" for level loading
using UnityEngine.UI; //Additional using directory, "UI" for buttons
using System;
using System.Collections;

public class ButtonScript : MonoBehaviour
{

    #region Vars
	//CanvasVars
	public static Canvas Start_Canvas;
	public static Canvas Loading_Canvas;
    //LevelVars
	public static int playLvl = 2;
	public static int tutorialLvl = 1;
    public static int mainMenuLvl = 0;
	public static int creditsLvl = 3;

	//SizeVar
	public static int Selected_Size = 1;

    //SliderVars
	public static Slider Grid_Slider;
	public static Slider P1_Slider;
	public static Slider P2_Slider;
    #endregion

	#region Unity Methods
	void Start()
	{
		Start_Canvas = GameObject.Find("StartCanvas").GetComponent<Canvas>();
		Loading_Canvas = GameObject.Find("LoadingCanvas").GetComponent<Canvas>();

		if(Start_Canvas != null)
			Start_Canvas.enabled = true;

		if(Loading_Canvas != null)
			Loading_Canvas.enabled = false;
		
		Grid_Slider = GameObject.Find("Grid_Slider").GetComponent<Slider>();
		if (Grid_Slider != null)
		{
			Grid_Slider.value = Selected_Size;
			this.onGridSliderMoved();
		}

		P1_Slider = GameObject.Find("P1_Slider").GetComponent<Slider>();
		P2_Slider = GameObject.Find("P2_Slider").GetComponent<Slider>();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape)) 
		{
			GameObject cubeGroup = GameObject.Find("CubeParentEmpty");
			if(cubeGroup != null)
			{
				if(cubeGroup.transform.childCount > 0)
				{
					foreach(Transform T in cubeGroup.GetComponentInChildren<Transform>())
						Destroy(T.gameObject);
				}
			}
			SceneManager.LoadScene(mainMenuLvl);
            MainGameScript.Player_2_Turn = false;
            MainGameScript.BG_Index = 0;
        }
	}

	#endregion

    #region Click Methods
    //Could be called by an exit application button
	public void onExitClick()
    {
		Application.Quit();
	}

    //Called by confirm button in game
    public void onConfirmClick()
    {
        MainGameScript.ConfirmLine();
        MainGameScript.ResetDots();
        foreach (GameObject go in GameObject.Find("mainGameHandler").GetComponent<MainGameScript>().confirmUI)
            go.GetComponent<Image>().enabled = false;
		
    }

    //Called by cancel button in game
    public void onCancelClick()
    {
        MainGameScript.ResetDots();
        foreach (GameObject go in GameObject.Find("mainGameHandler").GetComponent<MainGameScript>().confirmUI)
            go.GetComponent<Image>().enabled = false;
    }

    //Called by return button in various menus
    public void onMainMenuClick()
	{
		SceneManager.LoadScene(mainMenuLvl);
        MainGameScript.Player_2_Turn = false;
        MainGameScript.BG_Index = 0;
    }

	//Called by question mark button in start menu
	public void onHelpClick()
	{
		SceneManager.LoadScene(tutorialLvl);
	}

	//Called by asterisk button in start menu
	public void onCreditsClick()
	{
		SceneManager.LoadScene(creditsLvl);
	}

	//Called by play button in intro menu
	public void onPlayClick()
    {
		Start_Canvas.enabled = false;
		Loading_Canvas.enabled = true;

		Selected_Size = (int)Grid_Slider.value + 1;

		MainColorScript.P1_Hue = (int)P1_Slider.value * 16;
		MainColorScript.P2_Hue = (int)P2_Slider.value * 16;

		SceneManager.LoadSceneAsync(playLvl);
	}
	//Called when hue slider is moved
	public void onColorSliderMoved()
	{
		if(P1_Slider.value == P2_Slider.value)
			P2_Slider.value = (P2_Slider.value + 1) % 16;

		Image p1Box = P1_Slider.transform.FindChild("Box_Image").GetComponent<Image>();
		Image p2Box = P2_Slider.transform.FindChild("Box_Image").GetComponent<Image>();

		p1Box.color = MainColorScript.HsvToRgb((P1_Slider.value * 16f), 0.9f, 0.7f);
		p2Box.color = MainColorScript.HsvToRgb((P2_Slider.value * 16f), 0.9f, 0.7f);
	}

	//Called when size slider is moved
	public void onGridSliderMoved()
	{
		int currSizeValue = (int)Grid_Slider.value + 1;

		Transform boxGroup = GameObject.Find("GridSizes").transform;

		foreach(Transform T in boxGroup)
		{
			if ((int)Char.GetNumericValue(T.name.ToString()[5]) != currSizeValue)
			{
				Renderer[] allChildRenderers = T.GetComponentsInChildren<Renderer>();
				//Debug.Log(T.name.ToString() + " != " + currSizeValue);

				foreach(Renderer R in allChildRenderers )
					R.enabled = false;
			}
			else
			{
				Renderer[] allChildRenderers = T.GetComponentsInChildren<Renderer>();
				//Debug.Log(T.name.ToString() + " == " + currSizeValue);
				foreach(Renderer R in allChildRenderers )
					R.enabled = true;
			}
		}
	}
    #endregion
}

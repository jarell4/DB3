using UnityEngine;
using System.Collections;

public class MainColorScript : MonoBehaviour {
	
	#region Vars

	//Personal Color
	public static int P1_Hue = 0;
	public static int P2_Hue = 230;

	//DuplicateFixingVars
	private static MainColorScript Instance_Ref;
	#endregion

	#region Unity Methods

	void Awake()
	{
		if (Instance_Ref != null)
			Destroy(this.gameObject);
		else
			Instance_Ref = this;
	}

	void Start()
	{
		P1_Hue = 0;
		P2_Hue = 230;
	}

	#endregion

    /// <summary>
	/// Converted to C# by Chris Hulbert
    /// Convert HSV to RGB
    /// h is from 0-360
    /// s,v values are 0-1
    /// r,g,b values are 0-255
    /// Based upon http://ilab.usc.edu/wiki/index.php/HSV_And_H2SV_Color_Space#HSV_Transformation_C_.2F_C.2B.2B_Code_2
    /// </summary>
	public static Color HsvToRgb(double h, double S, double V)
    {
        // ######################################################################
        // T. Nathan Mundhenk
        // mundhenk@usc.edu
        // C/C++ Macro HSV to RGB

        double H = h;
        while (H < 0) { H += 360; }; ; ;
        while (H >= 360) { H -= 360; };
        double R, G, B;
        if (V <= 0)
        { R = G = B = 0; }
        else if (S <= 0)
        {
            R = G = B = V;
        }
        else
        {
            float hf = (float)H / 60.0f;
            int i = (int)Mathf.Floor(hf);
            double f = hf - i;
            double pv = V * (1 - S);
            double qv = V * (1 - S * f);
            double tv = V * (1 - S * (1 - f));
            switch (i)
            {

                // Red is the dominant color

                case 0:
                    R = V;
                    G = tv;
                    B = pv;
                    break;

                // Green is the dominant color

                case 1:
                    R = qv;
                    G = V;
                    B = pv;
                    break;
                case 2:
                    R = pv;
                    G = V;
                    B = tv;
                    break;

                // Blue is the dominant color

                case 3:
                    R = pv;
                    G = qv;
                    B = V;
                    break;
                case 4:
                    R = tv;
                    G = pv;
                    B = V;
                    break;

                // Red is the dominant color

                case 5:
                    R = V;
                    G = pv;
                    B = qv;
                    break;

                // Just in case we overshoot on our math by a little, we put these here. Since its a switch it won't slow us down at all to put these here.

                case 6:
                    R = V;
                    G = tv;
                    B = pv;
                    break;
                case -1:
                    R = V;
                    G = pv;
                    B = qv;
                    break;

                // The color is not defined, we should throw an error.

                default:
                    //LFATAL("i Value error in Pixel conversion, Value is %d", i);
                    R = G = B = V; // Just pretend its black/white
                    break;
            }
        }

		return new Color
			(Clamp((int)(R * 255.0)) / 255f, 
			Clamp((int)(G * 255.0)) / 255f, 
			Clamp((int)(B * 255.0)) / 255f);
    }

    /// <summary>
    /// Clamp a value to 0-255
    /// </summary>
    private static int Clamp(int i)
    {
        if (i < 0) return 0;
        if (i > 255) return 255;
        return i;
    }

	/* Untouched Version
	public static Color HsvToRgb(double h, double S, double V, out int r, out int g, out int b)
	{
		// ######################################################################
		// T. Nathan Mundhenk
		// mundhenk@usc.edu
		// C/C++ Macro HSV to RGB

		double H = h;
		while (H < 0) { H += 360; }; ; ;
		while (H >= 360) { H -= 360; };
		double R, G, B;
		if (V <= 0)
		{ R = G = B = 0; }
		else if (S <= 0)
		{
			R = G = B = V;
		}
		else
		{
			float hf = (float)H / 60.0f;
			int i = (int)Mathf.Floor(hf);
			double f = hf - i;
			double pv = V * (1 - S);
			double qv = V * (1 - S * f);
			double tv = V * (1 - S * (1 - f));
			switch (i)
			{

			// Red is the dominant color

			case 0:
				R = V;
				G = tv;
				B = pv;
				break;

				// Green is the dominant color

			case 1:
				R = qv;
				G = V;
				B = pv;
				break;
			case 2:
				R = pv;
				G = V;
				B = tv;
				break;

				// Blue is the dominant color

			case 3:
				R = pv;
				G = qv;
				B = V;
				break;
			case 4:
				R = tv;
				G = pv;
				B = V;
				break;

				// Red is the dominant color

			case 5:
				R = V;
				G = pv;
				B = qv;
				break;

				// Just in case we overshoot on our math by a little, we put these here. Since its a switch it won't slow us down at all to put these here.

			case 6:
				R = V;
				G = tv;
				B = pv;
				break;
			case -1:
				R = V;
				G = pv;
				B = qv;
				break;

				// The color is not defined, we should throw an error.

			default:
				//LFATAL("i Value error in Pixel conversion, Value is %d", i);
				R = G = B = V; // Just pretend its black/white
				break;
			}
		}
		r = Clamp((int)(R * 255.0));
		g = Clamp((int)(G * 255.0));
		b = Clamp((int)(B * 255.0));
	}

	/// <summary>
	/// Clamp a value to 0-255
	/// </summary>
	private static int Clamp(int i)
	{
		if (i < 0) return 0;
		if (i > 255) return 255;
		return i;
	}*/
}

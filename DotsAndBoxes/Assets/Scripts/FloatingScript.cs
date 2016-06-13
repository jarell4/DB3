using UnityEngine;
using System.Collections;

public class FloatingScript : MonoBehaviour {

	Vector3 originalPos;

	public float floatStrength = 0.1f;

	float offsetX;
	float offsetY;
	float offsetZ;

	// Use this for initialization
	void Start () {
	
		// Stores starting position of this object
		this.originalPos = this.transform.localPosition;

		// Creates three random values for offsetting the animation
		offsetX = Random.Range(-5, 5);
		offsetY = Random.Range(-5, 5);
		offsetZ = Random.Range(-5, 5);

		// Initializes floatStrength to conform to grid size
		floatStrength = floatStrength *((float)MainGameScript.GRID_SIZE / 2f);
	}
	
	// Update is called once per frame
	void Update () {
	
		// Update the object's position based on sin function
		transform.localPosition = new Vector3
			(originalPos.x + (Mathf.Sin(Time.time + offsetX)*floatStrength),
				originalPos.y + (Mathf.Sin(Time.time + offsetY)*floatStrength),
				originalPos.z + (Mathf.Sin(Time.time + offsetZ)*floatStrength));
		
	}
}

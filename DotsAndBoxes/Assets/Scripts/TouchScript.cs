using UnityEngine;
using System.Collections;

public class TouchScript : MonoBehaviour {

	public float rotateSpeed = 1;
	//public float rotateMax = 30;

	public float scaleSpeed = 0.01f;
	public float scaleMin = 1f;
	public float scaleMax = 4f;

	void Update()
	{
		// If there is one touch on the device...
		if (Input.touchCount == 1)
		{
			// Store touch
			Touch touch = Input.GetTouch(0);

			// If touch is moving while on screen...
			if (touch.phase == TouchPhase.Moved)
			{
				// Set the horizontal change to the change in touch on x axis
				float h = rotateSpeed * touch.deltaPosition.x;

				// Set the vertical change to the change in touch on y axis
				float v = rotateSpeed * touch.deltaPosition.y;

				// Transform this object's rotation from defined float variables
				transform.Rotate(v, -h, 0, Space.World);
			}
		}
		// If there are two touches on the device...
		if (Input.touchCount == 2)
		{
			// Store both touches
			Touch touchZero = Input.GetTouch(0);
			Touch touchOne = Input.GetTouch(1);

			// Find the position in the previous frame of each touch.
			Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

			// Find the magnitude of the vector (the distance) between the touches in each frame
			float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
			float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

			// Find the difference in the distances between each frame
			float deltaMagnitudeDiff = touchDeltaMag - prevTouchDeltaMag;

			// Create new float from product of diff in magnitude by the scale speed
			float scaleDelta = deltaMagnitudeDiff * scaleSpeed;

			// Create new float from sum of change in scale and current scale
			float newScale = scaleDelta + transform.localScale.x;

			// Clamp the size of the cube to make sure it doesn't invert scale or get too large
			transform.localScale = new Vector3(
				Mathf.Clamp(newScale, scaleMin, scaleMax), Mathf.Clamp(newScale, scaleMin, scaleMax), Mathf.Clamp(newScale, scaleMin, scaleMax));
		}
	}
}

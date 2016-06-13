using UnityEngine;
using System.Collections;

public class ParentRotateScript : MonoBehaviour {

    public bool isRot = false;
    public float rotSpeed = 5f;
    public float inSpeed = 50f;
    public float scaleSpeed = 0.05f;
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (isRot)
            this.transform.Rotate(rotSpeed * Time.deltaTime, rotSpeed * Time.deltaTime, rotSpeed * Time.deltaTime);

        float xRot = Input.GetAxis("Vertical");
        float yRot = Input.GetAxis("Horizontal");
        transform.Rotate(xRot * inSpeed * Time.deltaTime, yRot * inSpeed * Time.deltaTime, 0);

        if (Input.GetKey("e"))
        {
            transform.localScale = new Vector3(transform.localScale.x + scaleSpeed, transform.localScale.y + scaleSpeed, transform.localScale.z + scaleSpeed);
        }

        if (Input.GetKey("q"))
        {
            transform.localScale = new Vector3(transform.localScale.x - scaleSpeed, transform.localScale.y - scaleSpeed, transform.localScale.z - scaleSpeed);
        }
	}
}

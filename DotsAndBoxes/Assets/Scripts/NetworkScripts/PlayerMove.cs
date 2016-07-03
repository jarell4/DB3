using UnityEngine;
using UnityEngine.Networking;

public class PlayerMove : NetworkBehaviour
{
	void Update()
	{
		if (!isLocalPlayer)
			return;

		var x = Input.GetAxis("Horizontal")*0.1f;
		var y = Input.GetAxis("Vertical")*0.1f;

		transform.Translate(x, y, 0);
	}
}
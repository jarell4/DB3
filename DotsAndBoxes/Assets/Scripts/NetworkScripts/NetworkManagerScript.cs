using UnityEngine;
using System.Collections;

public class NetworkManagerScript : MonoBehaviour {

	private const string typeName = "DB3_NetworkTestJK";
	private const string gameName = "NetworkRoom";

	private HostData[] hostList;

	public GameObject networkBoxPrefab;

	private void RefreshHostList()
	{
		MasterServer.RequestHostList(typeName);
	}

	private void StartServer()
	{
		Network.InitializeServer(2, 25000, !Network.HavePublicAddress());
		MasterServer.RegisterHost(typeName, gameName);
	}

	private void JoinServer(HostData hostData)
	{
		Network.Connect(hostData);
	}

	public static void LeaveServer()
	{
		Network.Disconnect();

		GameObject[] NetworkObjs = GameObject.FindGameObjectsWithTag("NetworkObj");
		foreach (GameObject go in NetworkObjs)
		{
			Destroy(go);
		}
	}

	private void SpawnPlayer()
	{
		Network.Instantiate(networkBoxPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity, 0);
	}

	void OnServerInitialized()
	{
		SpawnPlayer();
		Debug.Log("Server Initialized");
	}

	void OnGUI()
	{
		if (!Network.isClient && !Network.isServer)
		{
				if (GUI.Button(new Rect(100, 100, 250, 100), "Start Server"))
					StartServer();

				if (GUI.Button(new Rect(100, 250, 250, 100), "Refresh Hosts"))
					RefreshHostList();

				if (hostList != null)
				{
					for (int i = 0; i < hostList.Length; i++)
					{
						if (GUI.Button(new Rect(400, 100 + (110 * i), 300, 100), hostList[i].gameName))
							JoinServer(hostList[i]);
					}
				}
			}

		else
		{
			if (GUI.Button(new Rect(100, 100, 250, 100), "Exit Server"))
				LeaveServer();
		}

	}

	void OnMasterServerEvent(MasterServerEvent msEvent)
	{
		if (msEvent == MasterServerEvent.HostListReceived)
			hostList = MasterServer.PollHostList();
	}

	void OnConnectedToServer()
	{
		Debug.Log("Server Joined");
	}
}

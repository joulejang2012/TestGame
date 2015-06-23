using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//public enum EGameState : byte
//{
//	StartMenu = 0,
//	InGame = 1,
//	EndGame
//}

public class NetworkManager : MonoBehaviour {

	private const string typeName = "MookJiPaGameName";
	private const string gameName = "JisooRoomName";
	private HostData[] hostList;

	public GameObject playerOb;

	//public EGameState currentGameState;

//	void Start() 
//	{
//		player1Behavior = player1.GetComponent<PlayerBehavior>();
//
//		player2Behavior = player2.GetComponent<PlayerBehavior>();

//	}

	void Update()
	{
		if (Input.GetKey("escape"))
			Application.Quit();
//		switch (currentGameState)
//		{
//		case EGameState.StartMenu:
//			if (Input.GetKeyDown(KeyCode.Return))
//			{
//				StartGame();
//				currentGameState = EGameState.InGame;
//			}
//			break;
//		case EGameState.InGame:
//			InGame();
//			break;
//		case EGameState.EndGame:
//			StartCoroutine(EndGame());
//			currentGameState = EGameState.StartMenu;
//			break;
//		}
	}
//	
//	void StartGame()
//	{
//		player1Behavior.reset();
//		player2Behavior.reset();
//		
//		canvas.SetActive(false);
//	}
//	
//	void InGame()
//	{
//		// Player Logic
//		player1Behavior.Draw();
//		player2Behavior.Draw();
//		
//		// Input
//		CheckPlayer1Input();
//		CheckPlayer2Input();
//	}
//	
//	public void DeclareLoser(bool isPlayer1)
//	{
//		if (isPlayer1)
//			isP1Winner = false;
//		else
//			isPlayer1 = true;
//	}
//	
//	void DisplayWinner() 
//	{
//		if (isP1Winner)
//			p1winscanvas.SetActive (true);
//		else
//			p2winscanvas.SetActive (true);
//	}
//	
//	IEnumerator EndGame() {
//		DisplayWinner();
//		yield return new WaitForSeconds(2.0f);
//		p1winscanvas.SetActive (false);
//		p2winscanvas.SetActive (false);
//		player1Behavior.reset();
//		player2Behavior.reset();
//		
//		canvas.SetActive (true);
//	}
//	
//	void CheckPlayer1Input()
//	{
//		if (Input.GetKeyDown(KeyCode.DownArrow)) player1Behavior.Fire();
//		else if (Input.GetKeyDown (KeyCode.LeftArrow)) player1Behavior.Reload ();
//		else if (Input.GetKeyDown (KeyCode.RightArrow)) player1Behavior.Jump ();
//		else player1Behavior.Idle();
//		
//	}
//	
//	void CheckPlayer2Input()
//	{
//		if (Input.GetKeyDown(KeyCode.S)) player2Behavior.Fire();
//		else if (Input.GetKeyDown (KeyCode.A)) player2Behavior.Reload ();
//		else if (Input.GetKeyDown (KeyCode.D)) player2Behavior.Jump ();
//		else player2Behavior.Idle();
//	}

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
	}
	
	private void StartServer()
	{
		Network.InitializeServer(5, 25000, !Network.HavePublicAddress());
		MasterServer.RegisterHost(typeName, gameName);
		//MasterServer.ipAddress = "127.0.0.1";
	}
	
	void OnServerInitialized()
	{
		SpawnPlayer ();
	}

	
	private void RefreshHostList()
	{
		MasterServer.RequestHostList(typeName);
	}
	
	void OnMasterServerEvent(MasterServerEvent msEvent)
	{
		if (msEvent == MasterServerEvent.HostListReceived)
			hostList = MasterServer.PollHostList();
	}

	private void JoinServer(HostData hostData)
	{
		Network.Connect(hostData);
	}
	
	void OnConnectedToServer()
	{
		SpawnPlayer ();
	}
	
	private void SpawnPlayer()
	{
		GameObject player = Network.Instantiate(playerOb, new Vector3(2.0f, 0f, 0), Quaternion.Euler(0, -90, 0), 0) as GameObject;
	}
}

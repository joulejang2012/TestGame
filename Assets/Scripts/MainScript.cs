//using UnityEngine;
//using UnityEngine.UI;
//using System.Collections;
//
//public enum EGameState : byte
//{
//	StartMenu = 0,
//	InGame = 1,
//	EndGame
//}
//
//public class MainScript : MonoBehaviour
//{
//	public GameObject canvasOb, playerOb, bulletOb, HealthOb;
//	private GameObject canvas, player1, player2, p1winscanvas, p2winscanvas;
//	private PlayerBehavior player1Behavior, player2Behavior;
//	private bool isP1Winner;
//
//	public EGameState currentGameState;
//
//	// Use this for initialization
//	void Start() 
//	{
//		//player1 = Instantiate(playerOb, new Vector3(2.0f, 0f, 0), Quaternion.Euler(0, 270, 0)) as GameObject;
//		//player2 = Instantiate(playerOb, new Vector3(-2.0f, 0f, 0), Quaternion.Euler(0, 180, 0)) as GameObject;
//		player1 = Instantiate(playerOb, new Vector3(2.0f, 0f, 0), Quaternion.identity) as GameObject;
//		player2 = Instantiate(playerOb, new Vector3(-2.0f, 0f, 0), Quaternion.identity) as GameObject;
//		canvas = Instantiate(canvasOb, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
//		canvas.SetActive (true);
//
//		p1winscanvas = Instantiate(canvasOb, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
//		Text t1 = p1winscanvas.GetComponentInChildren<Text> ();
//		t1.text = "Player1 wins";
//		p1winscanvas.SetActive (false);
//
//		p2winscanvas = Instantiate(canvasOb, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
//		Text t2 = p2winscanvas.GetComponentInChildren<Text> ();
//		t2.text = "Player2 wins";
//		p2winscanvas.SetActive (false);
//
//		player1Behavior = player1.GetComponent<PlayerBehavior>();
////		player1Behavior.amIPlayer1 (true);
////		player1Behavior.SetMainScript (this, HealthOb, player2, bulletOb);
//
//		player2Behavior = player2.GetComponent<PlayerBehavior>();
////		player2Behavior.amIPlayer1 (false);
////		player2Behavior.SetMainScript (this, HealthOb, player1, bulletOb);
//
//		currentGameState = EGameState.StartMenu;
//	}
//
//	void Update()
//	{
//		if (Input.GetKey("escape"))
//			Application.Quit();
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
//	}
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
//		//player1Behavior.Draw();
//		//player2Behavior.Draw();
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
//		//if (Input.GetKeyDown(KeyCode.Keypad0)) player1Behavior.Fire();
//		if (Input.GetKeyDown(KeyCode.DownArrow)) player1Behavior.Fire();
//		else if (Input.GetKeyDown (KeyCode.LeftArrow)) player1Behavior.Reload ();
//		else if (Input.GetKeyDown (KeyCode.RightArrow)) player1Behavior.Jump ();
//		//if (Input.GetKey (KeyCode.LeftArrow)) player1Behavior.MoveLeft ();
//		//else if (Input.GetKey (KeyCode.RightArrow)) player1Behavior.MoveRight ();
//		//if (Input.GetKey(KeyCode.UpArrow)) player1Behavior.MoveUp();
//		//if (Input.GetKey(KeyCode.DownArrow)) player1Behavior.MoveDown();
//		else player1Behavior.Idle();
//
//	}
//
//	void CheckPlayer2Input()
//	{
//		//if (Input.GetKeyDown(KeyCode.LeftShift)) player2Behavior.Fire();
//		if (Input.GetKeyDown(KeyCode.S)) player2Behavior.Fire();
//		else if (Input.GetKeyDown (KeyCode.A)) player2Behavior.Reload ();
//		else if (Input.GetKeyDown (KeyCode.D)) player2Behavior.Jump ();
//		//if (Input.GetKey(KeyCode.A)) player2Behavior.MoveLeft();
//		//else if (Input.GetKey(KeyCode.D)) player2Behavior.MoveRight();
//		//if (Input.GetKey(KeyCode.W)) player2Behavior.MoveUp();
//		//if (Input.GetKey(KeyCode.S)) player2Behavior.MoveDown();
//		else player2Behavior.Idle();
//	}
//}

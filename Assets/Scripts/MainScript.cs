using UnityEngine;
using System.Collections;

public enum EGameState : byte
{
	StartMenu = 0,
	InGame = 1,
	EndGame
}

public class MainScript : MonoBehaviour
{
	public GameObject canvasOb, playerOb, bulletOb, HealthOb;
	private GameObject canvas, player1, player2;
	private PlayerBehavior player1Behavior, player2Behavior;

	public EGameState currentGameState;

	// Use this for initialization
	void Start() 
	{
		player1 = Instantiate(playerOb, new Vector3(2.0f, 0.5f, 0), Quaternion.identity) as GameObject;
		player2 = Instantiate(playerOb, new Vector3(-2.0f, 0.5f, 0), Quaternion.identity) as GameObject;
		canvas = Instantiate(canvasOb, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
		canvas.SetActive (true);

		player1Behavior = player1.GetComponent<PlayerBehavior>();
		player1Behavior.SetMainScript (this, HealthOb, player2, bulletOb);
		player1Behavior.amIPlayer1 (true);

		player2Behavior = player2.GetComponent<PlayerBehavior>();
		player2Behavior.SetMainScript (this, HealthOb, player1, bulletOb);
		player2Behavior.amIPlayer1 (false);

		currentGameState = EGameState.StartMenu;
	}

	void Update()
	{
		switch (currentGameState)
		{
		case EGameState.StartMenu:
			if (Input.GetKeyDown(KeyCode.Return))
			{
				player1Behavior.reset();
				player2Behavior.reset();

				canvas.SetActive(false);
				currentGameState = EGameState.InGame;
			}
			break;
		case EGameState.InGame:
			InGame();
			break;
		case EGameState.EndGame:
			player1Behavior.reset();
			player2Behavior.reset();

			canvas.SetActive (true);
			currentGameState = EGameState.StartMenu;
			break;
		}
	}

	IEnumerator WaitForKeyDown(KeyCode keycode)
	{
		while (!Input.GetKeyDown(keycode)) 
			yield return null;
		canvas.SetActive(false);
	}

	void StartGame()
	{
		if (!canvas.activeSelf) canvas.SetActive (true);
		StartCoroutine(WaitForKeyDown(KeyCode.Return));
	}

	void InGame()
	{
		// Player Logic
		player1Behavior.Draw();
		player2Behavior.Draw();
		
		// Input
		CheckPlayer1Input();
		CheckPlayer2Input();
	}

	public void EndGame()
	{
		CancelInvoke("Draw");
		player1Behavior.reset ();
		player2Behavior.reset ();
		StartGame ();
	}

	void CheckPlayer1Input()
	{
		if (Input.GetKey(KeyCode.LeftArrow)) player1Behavior.MoveLeft();
		if (Input.GetKey(KeyCode.RightArrow)) player1Behavior.MoveRight();
		//if (Input.GetKey(KeyCode.UpArrow)) player1Behavior.MoveUp();
		//if (Input.GetKey(KeyCode.DownArrow)) player1Behavior.MoveDown();
		if (Input.GetKeyDown(KeyCode.Keypad0)) player1Behavior.Fire();
	}

	void CheckPlayer2Input()
	{
		if (Input.GetKey(KeyCode.A)) player2Behavior.MoveLeft();
		if (Input.GetKey(KeyCode.D)) player2Behavior.MoveRight();
		//if (Input.GetKey(KeyCode.W)) player2Behavior.MoveUp();
		//if (Input.GetKey(KeyCode.S)) player2Behavior.MoveDown();
		if (Input.GetKeyDown(KeyCode.LeftShift)) player2Behavior.Fire();
	}
}

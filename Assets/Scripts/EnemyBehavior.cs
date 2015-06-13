using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {

	private GameObject bullet, player;
	private MainScript mainScript;
	private Transform playerTransform, enemyTransform;
	private float speed, health;
	private Vector3 lookVector;
	
	// Use this for initialization
	void Start () 
	{
		speed = 1.0f;
		health = 100.0f;
		enemyTransform = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	public void Draw () 
	{
		lookVector = playerTransform.position - enemyTransform.position;
		lookVector.Normalize();
	}

	public void MoveLeft() 
	{
		enemyTransform.position = enemyTransform.position + Time.deltaTime * (new Vector3 (-speed, 0.0f, 0.0f));
	}

	public void MoveRight() 
	{
		enemyTransform.position = enemyTransform.position + Time.deltaTime * (new Vector3(speed, 0.0f, 0.0f));
	}

	public void MoveUp() 
	{
		enemyTransform.position = enemyTransform.position + Time.deltaTime * (new Vector3(0.0f, 0.0f, speed));
	}

	public void MoveDown() 
	{
		enemyTransform.position = enemyTransform.position + Time.deltaTime * (new Vector3(0.0f, 0.0f, -speed));
	}

	public void Fire()
	{
		GameObject bulletClone;
		BulletBehavior bulletCloneBehavior;
		bulletClone = Instantiate(bullet, enemyTransform.position, enemyTransform.rotation) as GameObject;
		bulletCloneBehavior = bulletClone.GetComponent<BulletBehavior> ();
		bulletCloneBehavior.setLookVector (lookVector);
		bulletCloneBehavior.Draw ();
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ( "Bullet"))
		{
			other.gameObject.SetActive (false);
			health = health - 10.0f;
			checkDead();
		}
	}

	void checkDead()
	{
		//if (health <= 0)
		//	mainScript.EndGame();
		if (health <= 0)
			mainScript.currentGameState = EGameState.EndGame;
	}

	public void reset() 
	{
		speed = 1.0f;
		health = 100.0f;
		playerTransform = GetComponent<Transform> ();
	}
	
	public void SetMainScript(MainScript m) 
	{
		mainScript = m;
		SetMainScriptPlayer(mainScript.GetPlayer());
		SetMainScriptBullet (mainScript.GetBullet());
	}

	public void SetMainScriptPlayer(GameObject p) 
	{
		player = p;
		playerTransform = player.GetComponent<Transform> ();
	}

	public void SetMainScriptBullet(GameObject b) 
	{
		bullet = b;
	}
}

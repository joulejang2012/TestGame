using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerBehavior : MonoBehaviour {

	private GameObject healthbar, bullet, enemy;
	private MainScript mainScript;
	private Transform playerTransform, enemyTransform;
	private Slider healthSlider;
	private float speed, health;
	private Vector3 lookVector;
	private bool isPlayer1;

	// Use this for initialization
	void Start() 
	{
		playerTransform = GetComponent<Transform> ();
	}

	// Update is called once per frame
	public void Draw() 
	{
		lookVector = enemyTransform.position - playerTransform.position;
		lookVector.Normalize();
	}

	public void MoveLeft() 
	{
		playerTransform.position = playerTransform.position + Time.deltaTime * (new Vector3(-speed, 0.0f, 0.0f));
	}

	public void MoveRight() 
	{
		playerTransform.position = playerTransform.position + Time.deltaTime * (new Vector3(speed, 0.0f, 0.0f));
	}

	public void MoveUp() 
	{
		playerTransform.position = playerTransform.position + Time.deltaTime * (new Vector3(0.0f, 0.0f, speed));
	}

	public void MoveDown() 
	{
		playerTransform.position = playerTransform.position + Time.deltaTime * (new Vector3(0.0f, 0.0f, -speed));
	}

	public void Fire()
	{
		GameObject bulletClone;
		BulletBehavior bulletCloneBehavior;
		bulletClone = Instantiate(bullet, playerTransform.position, playerTransform.rotation) as GameObject;
		bulletCloneBehavior = bulletClone.GetComponent<BulletBehavior>();
		bulletCloneBehavior.setLookVector(lookVector);
		bulletCloneBehavior.Draw();
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ( "Bullet"))
		{
			other.gameObject.SetActive (false);
			health = health - 10.0f;
			healthSlider.value = health;
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
		//playerTransform = GetComponent<Transform> ();
	}

	public void SetMainScript(MainScript m, GameObject h, GameObject e, GameObject b) 
	{
		mainScript = m;
		SetMainScriptHealth(h);
		SetMainScriptEnemy(e);
		SetMainScriptBullet(b);
	}

	public void SetMainScriptHealth(GameObject h)
	{
		healthbar = h;
		healthbar = Instantiate(h, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
		Text t = healthbar.GetComponentInChildren<Text> ();
		if (!isPlayer1) {
			t.text = "Player2 Health";
			RectTransform rt = t.GetComponent<RectTransform> ();
			rt.anchorMin = new Vector2 (1.0f, 1.0f);
			rt.anchorMax = new Vector2 (1.0f, 1.0f);
			rt.pivot = new Vector2 (1.0f, 1.0f);

			Slider s = healthbar.GetComponentInChildren<Slider> ();
			RectTransform rs = s.GetComponent<RectTransform> ();
			rs.anchorMin = new Vector2 (1.0f, 1.0f);
			rs.anchorMax = new Vector2 (1.0f, 1.0f);
			rs.pivot = new Vector2 (1.0f, 1.0f);

			healthSlider = healthbar.GetComponentInChildren<Slider> ();
		} else {
			t.text = "Player1 Health";
		}
	}
	
	public void SetMainScriptEnemy(GameObject e) 
	{
		enemy = e;
		enemyTransform = enemy.GetComponent<Transform>();
	}

	public void SetMainScriptBullet(GameObject b) 
	{
		bullet = b;
	}
	public void amIPlayer1(bool b)
	{
		isPlayer1 = b;
	}
}

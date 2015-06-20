using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerBehavior : MonoBehaviour {

	private GameObject healthbar, bullet, enemy;
	private MainScript mainScript;
	private Transform playerTransform, enemyTransform;
	private Animation anim;
	private Slider healthSlider;
	private float speed, health;
	private Vector3 lookVector;
	private bool isPlayer1;

	// Use this for initialization
	void Start() 
	{
		playerTransform = GetComponent<Transform> ();

		if (GetComponent<NetworkView>().isMine)
			playerTransform.position = new Vector3(2.0f, 0f, 0);

		anim = GetComponent<Animation> ();
		anim ["Idle"].wrapMode = WrapMode.Loop;
		anim ["Fire"].wrapMode = WrapMode.Once;
		anim ["Jump"].wrapMode = WrapMode.Once;
		anim ["Reload"].wrapMode = WrapMode.Once;

		anim ["Jump"].layer = 3;
		anim ["Fire"].layer = 1;
		anim ["Reload"].layer = 2;
		anim.Stop();
	}

	// Update is called once per frame
	public void Draw() 
	{
		lookVector = enemyTransform.position - playerTransform.position;
		lookVector.Normalize();
		playerTransform.LookAt (enemyTransform.position);
	}

	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		Vector3 syncPosition = Vector3.zero;
		if (stream.isWriting)
		{
			syncPosition = GetComponent<Rigidbody>().position;
			stream.Serialize(ref syncPosition);
		}
		else
		{
			stream.Serialize(ref syncPosition);
			GetComponent<Rigidbody>().position = syncPosition;
		}
	}

//	public void MoveLeft() 
//	{
//		if (isSameXDirection(lookVector.x, -1.0f)) anim.CrossFade ("Walkfwd");
//		else anim.CrossFade ("Walkbwd");
//		playerTransform.position = playerTransform.position + Time.deltaTime * (new Vector3(-speed, 0.0f, 0.0f));
//	}
//
//	public void MoveRight() 
//	{
//		if (isSameXDirection(lookVector.x, 1.0f)) anim.CrossFade ("Walkfwd");
//		else anim.CrossFade ("Walkbwd");
//		playerTransform.position = playerTransform.position + Time.deltaTime * (new Vector3(speed, 0.0f, 0.0f));
//	}
//
//	public void MoveUp() 
//	{
//		playerTransform.position = playerTransform.position + Time.deltaTime * (new Vector3(0.0f, 0.0f, speed));
//	}
//
//	public void MoveDown() 
//	{
//		playerTransform.position = playerTransform.position + Time.deltaTime * (new Vector3(0.0f, 0.0f, -speed));
//	}

	public void Idle() 
	{
		anim.CrossFade ("Idle");
	}

	public void Reload() 
	{
		anim.CrossFade ("Reload");
	}

	public void Jump() 
	{
		anim.CrossFade ("Jump");
	}

	public void Fire()
	{
		GameObject bulletClone;
		BulletBehavior bulletCloneBehavior;
		bulletClone = Instantiate(bullet, playerTransform.position, playerTransform.rotation) as GameObject;
		bulletCloneBehavior = bulletClone.GetComponent<BulletBehavior>();
		bulletCloneBehavior.setLookVector(lookVector);
		bulletCloneBehavior.Draw();
		anim.CrossFade ("Fire");
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
		if (health <= 0) 
		{
			mainScript.DeclareLoser(isPlayer1);
			mainScript.currentGameState = EGameState.EndGame;
		}
	}

	public void reset() 
	{
		speed = 0.73f;
		health = 100.0f;
		healthSlider.normalizedValue = health;
	}
	
	bool isSameXDirection(float x1, float x2)
	{
		return ((x1<0) == (x2<0)); 
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
		healthSlider = healthbar.GetComponentInChildren<Slider> ();
		if (!isPlayer1) {
			t.text = "Player1 Health";
			RectTransform rt = t.GetComponent<RectTransform> ();
			rt.anchorMin = new Vector2 (0f, 1.0f);
			rt.anchorMax = new Vector2 (0f, 1.0f);
			rt.pivot = new Vector2 (0f, 1.0f);

			RectTransform rs = healthSlider.GetComponent<RectTransform> ();
			rs.anchorMin = new Vector2 (0f, 1.0f);
			rs.anchorMax = new Vector2 (0f, 1.0f);
			rs.pivot = new Vector2 (0f, 1.0f);
		} 
		else {
			t.text = "Player2 Health";
			RectTransform rt = t.GetComponent<RectTransform> ();
			rt.anchorMin = new Vector2 (1.0f, 1.0f);
			rt.anchorMax = new Vector2 (1.0f, 1.0f);
			rt.pivot = new Vector2 (1.0f, 1.0f);
			
			RectTransform rs = healthSlider.GetComponent<RectTransform> ();
			rs.anchorMin = new Vector2 (1.0f, 1.0f);
			rs.anchorMax = new Vector2 (1.0f, 1.0f);
			rs.pivot = new Vector2 (1.0f, 1.0f);
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

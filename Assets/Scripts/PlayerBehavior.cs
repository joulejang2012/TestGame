using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerBehavior : MonoBehaviour {
	
	private Transform playerTransform;
	private Animation anim;
	private NetworkView netwrkView;

	// Use this for initialization
	void Start() 
	{
		playerTransform = GetComponent<Transform> ();
		netwrkView = GetComponent<NetworkView> ();

		if (GetComponent<NetworkView> ().isMine) {
			playerTransform.position = new Vector3 (-2.0f, 0f, 0);
			playerTransform.rotation = Quaternion.Euler(0, 90, 0);
		}

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

	private void InputAnimChange()
	{
		if (Input.GetKeyDown (KeyCode.R)) {
			netwrkView.RPC("ChangeAnimTo", RPCMode.All, "Fire");
		}
	}

	[RPC] void ChangeAnimTo(string a)
	{	
		anim.CrossFade (a);
	}

	void Update()
	{
		if (GetComponent<NetworkView>().isMine) {
			InputAnimChange();
		}
	}
//	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
//	{
//		Vector3 syncPosition = Vector3.zero;
//		if (stream.isWriting)
//		{
//			syncPosition = playerTransform.position;
//			stream.Serialize(ref syncPosition);
//		}
//		else
//		{
//			stream.Serialize(ref syncPosition);
//			playerTransform.position = syncPosition;
//		}
//	}

}

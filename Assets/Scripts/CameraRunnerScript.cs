using UnityEngine;
using System.Collections;

public class CameraRunnerScript : MonoBehaviour {

	public Transform player;
	void Update () 
	{
		transform.position = new Vector3 (player.position.x +3,player.position.y+3 , -10);
	}
}

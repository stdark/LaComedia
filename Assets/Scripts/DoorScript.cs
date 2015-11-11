using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {

	void Start () {

	}
	// Update is called once per frame
	void Update () {
		
	}
	// Use this for initialization
	void OnTriggerEnter2D(Collider2D col)
	{

		if (col.gameObject.name == "CharIdle") {

			Application.LoadLevel (1);
		}

	}
}

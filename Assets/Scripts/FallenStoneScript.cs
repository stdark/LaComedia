using UnityEngine;
using System.Collections;

public class FallenStoneScript : MonoBehaviour {


	void Start () {
	}
	void Update(){
	}
void OnCollisionEnter2D(Collision2D col){
		if ((col.gameObject.tag == "stone")) {
			Vector3 v = new Vector3(col.gameObject.transform.position.x,col.gameObject.transform.position.y+20,0);
			Quaternion q = new Quaternion(0,0,0,0);
			Instantiate(col.gameObject,v,q);
			Destroy(col.gameObject);
		
		}
	}
}

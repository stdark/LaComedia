using UnityEngine;
using System.Collections;

public class RemoveLock4 : MonoBehaviour {

	PlayerControl pl = new PlayerControl();
	public Sprite spr1 = new Sprite();
	public Sprite spr2 = new Sprite();
	public Sprite spr3 = new Sprite();
	// Use this for initialization
	void Start () {
		if (pl.ReadData ("level3")==0)
			gameObject.transform.position = new Vector3(0,0,-100);
		if (pl.ReadData ("level3") == 1)
		{
			GetComponent<SpriteRenderer> ().sprite = spr1;
			GetComponent<SpriteRenderer> ().sortingLayerName = "Player";;
			GetComponent<Transform>().position = new Vector2(float.Parse((-27).ToString ()),float.Parse((-24.8).ToString ()));
			GetComponent<Transform>().localScale = new Vector2(1,1);
			
		}
		if (pl.ReadData ("level3") == 2) {
			GetComponent<SpriteRenderer> ().sprite = spr2;
			GetComponent<SpriteRenderer> ().sortingLayerName  = "Player";;
			GetComponent<Transform>().position = new Vector2(float.Parse((-27).ToString ()),float.Parse((-24.8).ToString ()));
			GetComponent<Transform>().localScale = new Vector2(1,1);
		}
		if (pl.ReadData ("level3") == 3) {
			GetComponent<SpriteRenderer> ().sprite = spr3;
			GetComponent<SpriteRenderer> ().sortingLayerName  = "Player";;
			GetComponent<Transform>().position = new Vector2(float.Parse((-27).ToString ()),float.Parse((-24.8).ToString ()));
			GetComponent<Transform>().localScale = new Vector2(1,1);
		}
	}
	// Update is called once per frame
	void Update () {
		
	}
}

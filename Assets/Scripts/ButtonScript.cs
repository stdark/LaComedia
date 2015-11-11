using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour {
	float white=0;
	float a = 0; //для вывода
	void OnClick() {
		float coordinat = gameObject.transform.position.x;
		if (Mathf.Abs(white-coordinat)>60){
			a=0;//nelza
		}
		else{
			float i = coordinat;
			gameObject.transform.position = new Vector2(white, 0);
			white = i;
		}}
}

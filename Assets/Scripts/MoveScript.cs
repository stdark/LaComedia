using UnityEngine;

public class MoveScript : MonoBehaviour
{

	public Vector2 speed = new Vector2(10, 10);

	public Vector2 direction = new Vector2(-1, 0);
	
	private Vector2 movement;
	public Rigidbody2D r;
	
	void Update()
	{
		r = GetComponent<Rigidbody2D> ();
		movement = new Vector2(
			speed.x * direction.x,
			speed.y * direction.y);
	}

	void FixedUpdate()
	{

		r.velocity = movement;
	}
}

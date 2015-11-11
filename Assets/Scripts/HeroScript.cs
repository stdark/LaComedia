using UnityEngine;
using System.Collections;
using System.IO;

public class HeroScript : MonoBehaviour {
	public float maxSpeed = 10f;
	public int dir = 0;
	public float jumpForce = 700f;
	bool facingRight = true;
	public Transform groundCheck;
	public float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	public float score;
	public float move;
	public bool jump=false;
	public Rigidbody2D r;
	public bool IsOnFloar = true;
	public bool isRunner = false;
	public Animator anim;
	// Use this for initialization
	void Start () {
		r = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator> ();

	}
	public float ScoreReturn
	{
		get{return score;}
	}
	// Update is called once per frame
	void FixedUpdate () {


		move = Input.GetAxis ("Horizontal");

	}

	void Update(){
		bool shoot = Input.GetKey (KeyCode.LeftControl);
		shoot |= Input.GetKey (KeyCode.Space);
		// Замечание: Для пользователей Mac, Ctrl + стрелка - это плохая идея
		
		if (shoot)
		{
			WeaponScript weapon = GetComponent<WeaponScript>();
			if (weapon != null)
			{
				// ложь, так как игрок не враг
				weapon.Attack(false);
			}
		}

		if ((jump==false)  && (Input.GetKeyDown (KeyCode.W)||Input.GetKeyDown (KeyCode.UpArrow))) {

			r.AddForce (new Vector2(0f,jumpForce));
			jump=true;
			anim.SetBool("isOnFloor",false);
		}
		if (move != 0) {
			anim.SetBool ("isRunning", true);
			r.velocity = new Vector2 (move * maxSpeed, r.velocity.y);

		}
		
		if (move > 0 && !facingRight) {
			Flip ();
		} else if (move < 0 && facingRight) {
			Flip ();
		}



		if (Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
		}

		if (Input.GetKey(KeyCode.R))
		{
			Application.LoadLevel(Application.loadedLevel);
		}
		if (r.velocity.y != 0) {
			anim.SetBool ("IsOnFloar", false);

		} else {
			anim.SetBool ("IsOnFloar", true);
		}


	}
	
	void Flip(){
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	void OnCollisionEnter2D(Collision2D col){
		if ((col.gameObject.name == "ground")&&(jump==true))
			jump=false;

		if (col.gameObject.name == "endLevel") {
			if (!(GameObject.Find("Монетка"))) Application.LoadLevel ("scene2");
		}
		//Аннигиляция
		if ((col.gameObject.name == "Зло") || (col.gameObject.tag == "stone")|| (col.gameObject.name == "Огненная река")) {
			if(File.Exists ("G:/save.dat")==false)//ИСПРАВИТЬ ПУТЬ!!Ё!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
			{
				File.Create ("G:/save.dat");
				StreamWriter cr = new StreamWriter("save.dat");
				cr.Write (0);
				cr.Close ();
			}
			StreamReader scoreRead = new StreamReader("save.dat");
			float temp = float.Parse (scoreRead.ReadLine ());
			scoreRead.Close ();
			if(temp<score)
			{
				//File.Delete ("G:/save.dat");
				//File.Create ("G:/save.dat");
				StreamWriter cr = new StreamWriter("G:/save.dat");
				cr.Write (score);
				cr.Close ();
			}
			Application.LoadLevel (Application.loadedLevel);
		}
	}
	
	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.name == "Монетка") {
			score++;
			Destroy (col.gameObject);
		}
		if (col.gameObject.name == "DeathPlane")
			Application.LoadLevel (Application.loadedLevel);
	}
	void Stop(){
		r.velocity = new Vector2 (0, r.velocity.y);
		anim.SetBool ("isRunning", false);}


	void OnGUI(){
				GUI.Box (new Rect (0, 0, 100, 100), "Монеты: " + score);
		}



		
}

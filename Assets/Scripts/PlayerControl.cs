using UnityEngine;
using System.Collections;
using System.IO;

public class PlayerControl : MonoBehaviour {


	public float moveSpeed;
	public float jumpHeight;
	public float score;
	public AudioClip jump;

	public AudioClip sht;
	public Transform groundCheck;
	public float groundCheckRadius;
	public LayerMask whatIsGround;
	private bool grounded;
	private Animator anim;
	private float[] scoreinfo = new float[5];
	// Use this for initialization
	void Start () {
		if(File.Exists ("save.dat")==false)
		{
			File.Create ("save.dat");
			StreamWriter cr = new StreamWriter("save.dat");
			cr.Write (0);
			cr.Close ();
		}
		anim = GetComponent<Animator> ();
	}

	void FixedUpdate()
	{
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, whatIsGround);
	}
	
	// Update is called once per frame
	void Update () {
		bool shoot = Input.GetKeyDown (KeyCode.LeftControl);
		shoot |= Input.GetKeyDown (KeyCode.Space);

		
		if (shoot)
		{
			//AudioSource.PlayClipAtPoint (sht, new Vector3(GetComponent<Rigidbody2D>().position.x, GetComponent<Rigidbody2D>().position.y, 0));

			WeaponScript weapon = GetComponent<WeaponScript>();
			if (weapon != null)
			{
				AudioSource.PlayClipAtPoint (sht, new Vector3(GetComponent<Rigidbody2D>().position.x, GetComponent<Rigidbody2D>().position.y, 0));
				weapon.Attack(false);
			}

		}
		anim.SetBool ("Shot", shoot);
		anim.SetBool ("Grounded", grounded);



	if (Input.GetKeyDown (KeyCode.UpArrow) && grounded ) {
			GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x,jumpHeight);
			AudioSource.PlayClipAtPoint (jump, new Vector3(GetComponent<Rigidbody2D>().position.x, GetComponent<Rigidbody2D>().position.y, 0));
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed,GetComponent<Rigidbody2D>().velocity.y);


		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed,GetComponent<Rigidbody2D>().velocity.y);


		}

		anim.SetFloat ("Speed", Mathf.Abs(GetComponent<Rigidbody2D> ().velocity.x));

		if (GetComponent<Rigidbody2D> ().velocity.x > 0)
			transform.localScale = new Vector3 (1f, 1f, 1f);
		else if (GetComponent<Rigidbody2D> ().velocity.x < 0)
			transform.localScale = new Vector3 (-1f, 1f, 1f);
	}
	void OnCollisionEnter2D(Collision2D col){

		
		if (col.gameObject.name == "endLevel") {
			if (!(GameObject.Find("Монетка"))) Application.LoadLevel ("scene2");

		}
		//Аннигиляция
		if ((col.gameObject.name == "Зло") || (col.gameObject.tag == "stone")|| (col.gameObject.name == "Огненная река")) {

			Application.LoadLevel (Application.loadedLevel);
		}
	}
	
	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.name == "Монетка") {
			score++;
			Destroy (col.gameObject);
		}
		if (col.gameObject.name == "Door") {
			SaveData ();
			Application.LoadLevel (1);
		}	
		if (col.gameObject.name == "DeathPlane")
			Application.LoadLevel (Application.loadedLevel);
	}

	public void SaveData()
	{
		if(File.Exists ("save.dat")==false)
		{
			File.Create ("save.dat");
			StreamWriter cr = new StreamWriter("save.dat");
			cr.Write (0);
			cr.Close ();
		}

		string[] temp = File.ReadAllLines ("save.dat");
		StreamWriter wcr = new StreamWriter("save.dat");
		for(int i = 0; i<temp.Length; i++)
		{
			wcr.WriteLine (temp[i]);
		}
		wcr.WriteLine (Application.loadedLevelName + '/' + score);
		wcr.Close ();
	}

	public float ReadData(string LevelName)
	{

		StreamReader read = new StreamReader ("save.dat");

		while (!read.EndOfStream) {
			string[] temp = read.ReadLine ().Split ('/');
				scoreinfo[int.Parse(temp[0][5].ToString ())-1]  =float.Parse(temp[1]); 
		}
		read.Dispose ();
		return scoreinfo[int.Parse (LevelName[5].ToString())-1];

	}
	
	void OnGUI(){
		GUI.Box (new Rect (0, 0, 100, 100), "Монеты: " + score);
	}
}

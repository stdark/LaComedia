using UnityEngine;

public class WeaponScript : MonoBehaviour

{
	public float shootingRate = 0.25f;
	public Transform shotPrefab;
	private float shootCooldown;


	void Start()
	{
		shootCooldown = 0f;
	}
	
	void Update()
	{

		if (shootCooldown > 0)
		{
			shootCooldown -= Time.deltaTime;
		}
	}

	public void Attack(bool isEnemy)
	{
		if (CanAttack)
		{
			shootCooldown = shootingRate;

			var shotTransform = Instantiate(shotPrefab) as Transform;

			shotTransform.position = transform.position;

			ShotScript shot = shotTransform.gameObject.GetComponent<ShotScript>();
			if (shot != null)
			{
				shot.isEnemyShot = isEnemy;
			}
			PlayerControl pl = GetComponent<PlayerControl>();
			MoveScript move = shotTransform.gameObject.GetComponent<MoveScript>();
			move.direction = new Vector2(pl.GetComponent<Rigidbody2D>().transform.localScale.x, 0);

		}
	}
	public bool CanAttack
	{
		get
		{
			return shootCooldown <= 0f;
		}
	}
}

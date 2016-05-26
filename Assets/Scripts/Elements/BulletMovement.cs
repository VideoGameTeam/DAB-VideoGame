using UnityEngine;
using System.Collections;

public class BulletMovement : MonoBehaviour {
	public float bulletSpeed;
	public Vector3 dir;

	//public Player scriptplayer;


	void Start () {
		AudioSource sound = GetComponent<AudioSource> ();
		sound.Play ();

		Vector3 sp = Camera.main.WorldToScreenPoint(transform.position);
		dir = (Input.mousePosition - sp).normalized;
		GetComponent<Rigidbody2D>().AddForce (dir * bulletSpeed);

		Destroy (gameObject,3);



	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Enemy")
		{
			MonsterHealth monsterScript = other.gameObject.GetComponent<MonsterHealth>();
			monsterScript.ReceiveDamage(40);
			Destroy (gameObject);
		}
	}

}

/*
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hit))
		{
			Vector2 target = new Vector2 (hit.point.x, hit.point.y);

			direction = target - new Vector2(transform.position.x, transform.position.y);
			direction.Normalize();

			GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

		}
		*/
//Vector2 target = Camera.main.ScreenToWorldPoint( Input.mousePosition );
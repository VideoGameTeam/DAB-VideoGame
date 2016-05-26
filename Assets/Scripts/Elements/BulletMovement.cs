using UnityEngine;
using System.Collections;

public class BulletMovement : MonoBehaviour {
	public float bulletSpeed;
	public Vector3 dir;
	private float todestroy=3;

	void Start () {
		AudioSource sound = GetComponent<AudioSource> ();
		sound.Play ();

		Vector3 sp = Camera.main.WorldToScreenPoint(transform.position);
		dir = (Input.mousePosition - sp);
		dir=(new Vector3(dir.x,dir.y,0));
		dir=dir.normalized;
	
		Destroy (gameObject,3);

	}

	void Update ()
	{
		transform.Translate(dir * Time.deltaTime * bulletSpeed);
		todestroy = todestroy - Time.deltaTime;

	}


	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Enemy")
		{
			MonsterHealth monsterScript = other.gameObject.GetComponent<MonsterHealth>();
			monsterScript.ReceiveDamage(40);

			if (todestroy < 2.98f && gameObject.tag=="Bullet") {
				Destroy (gameObject);
			}
		
		}
	}

}

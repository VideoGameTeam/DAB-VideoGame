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
		Destroy (gameObject,3);


	}

	void Update ()
	{
		transform.Translate(dir * Time.deltaTime * bulletSpeed);
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

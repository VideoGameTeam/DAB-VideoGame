using UnityEngine;
using System.Collections;

public class BulletMovement : MonoBehaviour {
	public float bulletSpeed;
	float duration = 5;

	Vector3 direction;


	void Start () {

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


		Vector3 sp = Camera.main.WorldToScreenPoint(transform.position);
		Vector3 dir = (Input.mousePosition - sp).normalized;
		GetComponent<Rigidbody2D>().AddForce (dir * bulletSpeed);

		StartCoroutine(WaitAndDestroy());
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Enemy")
		{
			MonsterHealth monsterScript = other.gameObject.GetComponent<MonsterHealth>();
			monsterScript.ReceiveDamage(40);
			StopAllCoroutines();
			Destroy(gameObject);
		}
	}

	IEnumerator WaitAndDestroy(){
		yield return new WaitForSeconds(5);	
		Object.Destroy (this.gameObject);
	}
}
using UnityEngine;
using System.Collections;

public class BulletMovement : MonoBehaviour {
	public float bulletSpeed;

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

	}
}
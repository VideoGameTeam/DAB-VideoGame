using UnityEngine;
using System.Collections;

public class BulletMovement : MonoBehaviour {
	public float bulletSpeed;
	float duration = 5;

	Vector3 direction;

	
	void Start () {
		AudioSource audio = GetComponent<AudioSource>();

		audio.Play();


		Vector3 sp = Camera.main.WorldToScreenPoint(transform.position);
		Vector3 dir = (Input.mousePosition - sp).normalized;
		GetComponent<Rigidbody2D>().AddForce (dir * bulletSpeed);

		StartCoroutine(WaitAndDestroy());
	}


	IEnumerator WaitAndDestroy(){
		yield return new WaitForSeconds(5);	
		Object.Destroy (this.gameObject);
	}
}
using UnityEngine;
using System.Collections;

public class Puzzle : MonoBehaviour {

	public float PosYInit;
	public Vector3 PosY1,PosY2,PosY3,PosY4;
	public GameObject Platform1;
	public GameObject Platform2;
	public GameObject Platform3;
	public GameObject Platform4;

	public float timegame = 3;

	private bool btndown=false;
	private bool playon = false;
	private float timerem;

	public int lastplat,plat=0;

	// Use this for initialization
	void Start () {
		
		timerem = timegame + 0.5f * (2 - Gamestate.EstadoJuego.Dificult);
		PosYInit = gameObject.transform.position.y;
		PosY1= Platform1.transform.position;
		PosY2 = Platform2.transform.position;
		PosY3 = Platform3.transform.position;
		PosY4 = Platform4.transform.position;


	}


	// Update is called once per frame
	void Update () {


		if (playon==true)
		{
			timerem = timerem - Time.deltaTime;	
			if (timerem < 0) {
				timerem = timegame + 0.5f * (2 - Gamestate.EstadoJuego.Dificult);		
				playon = false;
		}
		}
		else
		{
			if (btndown==false && gameObject.transform.position.y<PosYInit) {
				gameObject.transform.position = new Vector3 (gameObject.transform.position.x,( gameObject.transform.position.y+0.1f*Time.deltaTime), gameObject.transform.position.z);

			}

			posoff();

		}

		if (btndown && gameObject.transform.position.y>(PosYInit-0.12f)) {
			gameObject.transform.position = new Vector3 (gameObject.transform.position.x,( gameObject.transform.position.y-0.3f*Time.deltaTime), gameObject.transform.position.z);
		
		}
		if (btndown) {
			pospaly ();
		}
		if (lastplat != plat) {
		
			if (plat == lastplat + 1) {
				lastplat = plat;
			} else {
				lastplat = 0;
				plat = 0;
				playon = false;
				timerem = timegame + 0.5f * (2 - Gamestate.EstadoJuego.Dificult);	
			}
		}
	
	}

	void pospaly()
	{

		Platform1.GetComponent<Rigidbody2D>().isKinematic = true;
		Platform1.transform.position = new Vector3 (PosY1.x,PosY1.y,PosY1.z);
		Platform1.transform.rotation = Quaternion.Euler(0, 0, -270);
	
		Platform2.GetComponent<Rigidbody2D>().isKinematic = true;
		Platform2.transform.position = new Vector3 (PosY2.x,PosY2.y,PosY2.z);
		Platform2.transform.rotation = Quaternion.Euler(0, 0, -270);

		Platform3.GetComponent<Rigidbody2D>().isKinematic = true;
		Platform3.transform.position = new Vector3 (PosY3.x,PosY3.y,PosY3.z);
		Platform3.transform.rotation = Quaternion.Euler(0, 0, -270);

		Platform4.GetComponent<Rigidbody2D>().isKinematic = true;
		Platform4.transform.position = new Vector3 (PosY4.x,PosY4.y,PosY4.z);
		Platform4.transform.rotation = Quaternion.Euler(0, 0, -270);


	}
	void posoff()
	{
		Platform1.GetComponent<Rigidbody2D>().isKinematic = false;	
		Platform2.GetComponent<Rigidbody2D>().isKinematic = false;	
		Platform3.GetComponent<Rigidbody2D>().isKinematic = false;	
		Platform4.GetComponent<Rigidbody2D>().isKinematic = false;

	}


	void OnTriggerStay2D(Collider2D objeto)
	{

		if (objeto.tag == "Player") {
			timerem = timegame + 0.5f * (2 - Gamestate.EstadoJuego.Dificult);	

		}
	}


	void OnTriggerEnter2D(Collider2D objeto)
	{

		if (objeto.tag == "Player") {
			
			btndown = true;


	


		}
	}

	void OnTriggerExit2D(Collider2D objeto)
	{

		if (objeto.tag == "Player") {

			btndown = false;
			playon = true;

		}
	}




	/*

	public float smooth = 2.0F;
	public float tiltAngle = 30.0F;
	float tiltAroundZ = Input.GetAxis("Horizontal") * tiltAngle;
		float tiltAroundX = Input.GetAxis("Vertical") * tiltAngle;
		Quaternion target = Quaternion.Euler(tiltAroundX, 0, tiltAroundZ);
		transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
*/




}

using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Controller2D))]
public class Player : MonoBehaviour {

	public float maxJumpHeight = 50;
	public float minJumpHeight = 1;
	public float timeToJumpApex = .4f;
	float accelerationTimeAirborne = .2f;
	float accelerationTimeGrounded = .1f;
	public float moveSpeed;

	float originalMoveSpeed;
    
	public Vector2 wallJumpClimb;
	public Vector2 wallJumpOff;
	public Vector2 wallLeap;

	public float wallSlideSpeedMax = 2;
	public float wallStickTime = .25f;
	float timeToWallUnstick;

	float gravity;
	float maxJumpVelocity;
	float minJumpVelocity;
	Vector3 velocity;
	float velocityXSmoothing;

	Controller2D controller;
    Animator anim;
    private int walkHash;
    private int idleHash;


	Vector2 input;
	float sprint;
	float jump;

	float jumpIdle;
	float walk;
	float idle;
	float wall;
	float fall;
	float stick;
	float shoot;
	float magic;


	// Shoot variables
	public float cadence;
	public GameObject shootPrefab;
	public GameObject magicPrefab;


	bool forward;
	float playerDir;

	float medicineDelay =0;
	float weaponDelay =0;
	float fireCadence =0;
	float timetrap=10;

	Transform animTransform;
	Transform shootTransform;
	Transform magicTransform;


	public AudioClip[] carlSounds;

	public Animation animSprint;

	private AudioSource jumpSound;

    void Start() {


		jumpSound = gameObject.AddComponent<AudioSource> ();
		jumpSound.clip = carlSounds [0];

		originalMoveSpeed = moveSpeed;
		controller = GetComponent<Controller2D> ();
		forward = true;
		animTransform = FindTransform ("Human");
		shootTransform = animTransform.Find("ShootSpawner");
		magicTransform = animTransform.Find ("MagicSpawner");

		if (animTransform != null) {
			anim = animTransform.GetComponent<Animator> ();
			walkHash = Animator.StringToHash ("Walking");
			idleHash = Animator.StringToHash ("Idle");
		}
		gravity = -(2 * maxJumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		maxJumpVelocity = Mathf.Abs (gravity) * timeToJumpApex;
		minJumpVelocity = Mathf.Sqrt (2 * Mathf.Abs (gravity) * minJumpHeight);
		print ("Gravity: " + gravity + "  Jump Velocity: " + maxJumpVelocity);
	}

	void Update() {
		if(Time.timeScale == 0){
			return;
		}
		input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		int wallDirX = (controller.collisions.left) ? -1 : 1;

		playerDir = (int)Mathf.Sign(input.x);

		RotateCarl ();
		Sprinting();
		Walking ();
		Sticking ();
		Jumping ();
		if (jump < 0.1) {
			JumpingWall ();
		}
		Falling ();
		Shooting ();

		medicineDelay -= Time.deltaTime;
		if (Input.GetButton ("FirstAid") && medicineDelay <=0) {
			medicineDelay = 2;
			if (Gamestate.EstadoJuego.Medicine > 0) {
				Gamestate.EstadoJuego.Medicine -=1;	
				Gamestate.EstadoJuego.ChangeHealth (30);
			}

		}
		weaponDelay -= Time.deltaTime;
		if (Input.GetButton ("ChangeWeapon") && weaponDelay <=0) {
			Gamestate.EstadoJuego.Trident = !Gamestate.EstadoJuego.Trident;
			weaponDelay = 2;
		}

		float targetVelocityX = input.x * moveSpeed;
		velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirborne);

		bool wallSliding = false;
		if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0) {
			wallSliding = true;

			if (velocity.y < -wallSlideSpeedMax) {
				velocity.y = -wallSlideSpeedMax;
			}

			if (timeToWallUnstick > 0) {
				velocityXSmoothing = 0;
				velocity.x = 0;

				if (input.x != wallDirX && input.x != 0) {
					timeToWallUnstick -= Time.deltaTime;
				}
				else {
					timeToWallUnstick = wallStickTime;
				}
			}
			else {
				timeToWallUnstick = wallStickTime;
				jump = 0.2F;
			}

		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			if (wallSliding) {
				if (wallDirX == input.x) {
					velocity.x = -wallDirX * wallJumpClimb.x;
					velocity.y = wallJumpClimb.y;
				}
				else if (input.x == 0) {
					velocity.x = -wallDirX * wallJumpOff.x;
					velocity.y = wallJumpOff.y;
				}
				else {
					velocity.x = -wallDirX * wallLeap.x;
					velocity.y = wallLeap.y;
				}
			}
			if (controller.collisions.below) {
				velocity.y = maxJumpVelocity;
			}
		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			if (velocity.y > minJumpVelocity) {
				velocity.y = minJumpVelocity;
			}
		}


		velocity.y += gravity * Time.deltaTime;
		controller.Move (velocity * Time.deltaTime, input);

		if (controller.collisions.above || controller.collisions.below) {
			velocity.y = 0;
			jump = 0.0F;
		}

		if ((controller.collisions.left || controller.collisions.right) && stick > 0.1F) {
			jump = 0.0F;
		}


		/*
		animSprint ["Basic_Run_03"].speed = 1f;
		if (controller.collisions.inTrap) {
			animSprint ["Basic_Run_03"].speed = 0.5f;
		} */

		Quaternion currentRotation = animTransform.rotation;

		anim.SetFloat ("Jump", jump);
		anim.SetFloat ("Sprint", sprint);
		anim.SetFloat ("Walk", walk);
		anim.SetFloat ("Wall", wall);
		anim.SetFloat ("Fall",fall);
		anim.SetFloat ("Stick", stick);
		anim.SetFloat ("Shoot", shoot);
		anim.SetFloat ("Magic", magic);

		animTransform.rotation = currentRotation;	
		animTransform.localPosition = Vector3.zero * Time.deltaTime;

		if (timetrap < 10) {
			timetrap += Time.deltaTime; 
		}

	}
		


	Transform FindTransform(string name){
		Component[] transforms = transform.GetComponentsInChildren<Transform>();
		foreach(Transform t in transforms){
			if (t.name == name) {
				return t;
			} 
		}
		return null;
	}

	void Shooting(){
		if (Input.GetButton ("Fire1") && (Gamestate.EstadoJuego.Admo > 0) &&(!Gamestate.EstadoJuego.Trident)&& !(controller.collisions.right || controller.collisions.left)){
			
			shoot = 0.2F;
			moveSpeed = 0.0F;

			fireCadence -=Time.deltaTime;
			if (fireCadence <= 0) {
				GameObject bullet = Instantiate (shootPrefab, shootTransform.position, Quaternion.identity) as GameObject;
				Gamestate.EstadoJuego.Admo -= 1;
				fireCadence = 0.5F;
			}

		}else if(Input.GetButton ("Fire1") && (Gamestate.EstadoJuego.mana > 0) &&(Gamestate.EstadoJuego.Trident) && !(controller.collisions.right || controller.collisions.left)){
			magic = 0.2F;
			moveSpeed = 0.0F;

			fireCadence -=Time.deltaTime;
			if (fireCadence <= 0) {
				GameObject bullet = Instantiate (magicPrefab, magicTransform.position, Quaternion.identity) as GameObject;
				Gamestate.EstadoJuego.mana -= 10;
				fireCadence = 1.0F;
			}

		}
		else {
			shoot = 0.0F;
			magic = 0.0F;
		}
	}

	void Falling(){
		if (!controller.collisions.below) {
			fall = 0.1F;
		} else {
			fall = 0.0F;
		}
	}

	void Sprinting () {
		if( input.x !=0 && !(controller.collisions.left || controller.collisions.right) && !Input.GetButton("Walk") && shoot == 0.0F && magic == 0.0F) {
			sprint = Mathf.Abs (input.x);
			moveSpeed = originalMoveSpeed;
			if (controller.collisions.inTrap) {
				moveSpeed = originalMoveSpeed / 2;
			}
		}
		else {
			sprint = 0.0F;
		}

	}
	void JumpingWall(){
		if (Input.GetButton("Jump") && (controller.collisions.left || controller.collisions.right)) {
			jumpSound.Play ();
			jump = 0.2F;
			sprint = 0.0F;
			wall = 0.1F;
		} else {
			jump = 0.0F;
			wall = 0.0F;
		}
	}

	void Jumping(){
		if (Input.GetButton("Jump") && controller.collisions.below && (input.x != 0 || input.x == 0)) {
			jumpSound.Play ();
			jump = 0.2F;
		} else {
			jump = 0.0F;
		}
	}

	void Walking(){
		if (Input.GetButton("Walk") && controller.collisions.below && Input.GetButton("Horizontal") && shoot == 0.0F) {
			walk = 0.2F;
			sprint = 0.0F;
			moveSpeed = originalMoveSpeed/3;
		} else {
			walk = 0.0F;
		}
	}

	void Sticking(){
		if (!controller.collisions.below && (controller.collisions.left || controller.collisions.right)) {
			stick = 0.2F;
			if (forward && controller.collisions.left) {
				stick = 0.0F;
			}else if(!forward && controller.collisions.right){
				stick = 0.0F;
			}
		} else {
			stick = 0.0F;
		}

	}

	void RotateCarl(){
		if (Time.timeScale != 0) {
			if (Input.GetButton ("Horizontal") && playerDir < 0 && forward) {
				print ("izquierda");
				animTransform.Rotate (0, 160, 0);
				forward = false;
			} else if (Input.GetButton ("Horizontal") && playerDir > 0 && !forward) {
				print ("derecha");
				animTransform.Rotate (0, 200, 0);
				forward = true;
			}
		}
	}
	//Trampas Collider


	void OnTriggerStay2D(Collider2D objeto)
	{
		if (timetrap >= 1) {
			timetrap = 0;
			if (objeto.tag == "SpaceTrap") {
				Gamestate.EstadoJuego.ChangeHealth (-10);

			} else if (objeto.tag == "Lasser") {
				Gamestate.EstadoJuego.ChangeHealth (-30);

			} else if (objeto.tag == "Enemy") {
				Gamestate.EstadoJuego.ChangeHealth (-30);
			}
		}
	
	}

	void OnTriggerExit2D(Collider2D objeto)
	{
		if (objeto.tag == "SpaceTrap"||objeto.tag == "Lasser" ||objeto.tag == "Enemy")
		{
			timetrap=10;
		}
		
	}


}
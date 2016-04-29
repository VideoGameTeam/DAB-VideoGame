using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Controller2D))]
public class Player : MonoBehaviour {

	public float maxJumpHeight = 4;
	public float minJumpHeight = 1;
	public float timeToJumpApex = .4f;
	float accelerationTimeAirborne = .2f;
	float accelerationTimeGrounded = .1f;
	public float moveSpeed = 6;
    
	public Vector2 wallJumpClimb;
	public Vector2 wallJumpOff;
	public Vector2 wallLeap;

	public float wallSlideSpeedMax = 3;
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
	float walk;


	bool forward;
	float playerDir;

	Transform animTransform;

    void Start() {
		controller = GetComponent<Controller2D> ();
		forward = true;
		animTransform = FindTransform ("Human");

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
		input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		int wallDirX = (controller.collisions.left) ? -1 : 1;

		playerDir = (int)Mathf.Sign(input.x);

		RotateCarl ();
		Walking ();
		Jumping ();
		Sprinting();

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

	void Sprinting () {
		if(Input.GetButton("Run")) {
			sprint = 0.2F;
			moveSpeed = moveSpeed *3.5F;
		}
		else {
			sprint = 0.0F;
		}

	}
	void Jumping(){
		if (Input.GetButton("Jump")) {
			jump = 0.2F;
		} else {
			jump = 0.0F;
		}
	}

	void Walking(){
		if (input.x != 0) {
			walk = Mathf.Abs (input.x);
			moveSpeed = 30;
		} else {
			walk = 0;
		}
	}

	void RotateCarl(){

		if (Input.GetButton("Horizontal") && playerDir < 0 && forward) {
			print ("izquierda");
			animTransform.Rotate (0, 180,0);
			forward = false;
		} else if(Input.GetButton("Horizontal") && playerDir > 0 && !forward) {
			print ("derecha");
			animTransform.Rotate (0, 180,0);
			forward = true;
		}
	}

	void FixedUpdate () {

		//set the "Walk" parameter to the v axis value
		anim.SetFloat ("Walk", walk);
		anim.SetFloat("Sprint", sprint);
		anim.SetFloat ("Jump", jump);
	}
}
using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Controller2D))]
public class Player : MonoBehaviour {

    private enum playerStates
    {
        WALKING,
        IDLE
    };

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
    playerStates pState;


	Vector2 input;
	float sprint;
	float jump;


    void Start() {
		controller = GetComponent<Controller2D> ();

		Transform animTransform = FindTransform ("Human");

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

		Sprinting();
		Jumping ();

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

        print(velocity);



		if(velocity.x > 0 && anim != null)
        {

			anim.SetTrigger(idleHash);
        }
        else
        {
			anim.SetTrigger(walkHash);
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
		if(Input.GetButton("Fire1")) {
			sprint = 0.2F;
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

	void FixedUpdate () {

		//set the "Walk" parameter to the v axis value
		anim.SetFloat ("Walk", input.x);
		anim.SetFloat("Sprint", sprint);
		anim.SetFloat ("Jump", jump);
	}
}
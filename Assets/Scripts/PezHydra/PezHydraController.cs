using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PezHydra2D))]
public class PezHydraController : MonoBehaviour
{
    public float Health;

    private Transform myTransform;
	private Transform targetTransform;
    private Vector3 originalPosition;
	private LayerMask raycastLayer;
	public float radius = 10;
	private float originTime = .0f;
	private float followTime = 3f;

    float accelerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;
    public float wallSlideSpeedMax = 2;
    public float wallStickTime = .25f;
    float timeToWallUnstick;

    private PezHydra2D controller;
    public Vector3 velocity;
    public Vector2 MonsterInput;
    public float AttackDistance;
    public float moveSpeed;
    public float maxFollowDistance;
    public float stoppingDistance;
    private bool goingBack;
    float velocityXSmoothing;
    float jump;
    private float lastAttackTime;
    public float attackTime;

    private Transform modelGO; 
    private Animation anim;


	void Start () 
	{
		myTransform = transform;
        originalPosition = myTransform.position;
		raycastLayer = 1 << LayerMask.NameToLayer("Player");

        controller = GetComponent<PezHydra2D>();

        modelGO = transform.FindChild("PezHydraModel");
	    anim = modelGO.GetComponent<Animation>();

		StartCoroutine(DoCheck());
	}

    void Update()
    {
        if(targetTransform != null)
        {
            MoveToTarget();
        }
    }

	void SearchForTarget()
	{
        if (targetTransform == null)
		{
			Collider2D hitCollider = Physics2D.OverlapCircle(myTransform.position, radius, raycastLayer);

			if (hitCollider)
			{
				targetTransform = hitCollider.transform;
				originTime = Time.time;
			    lastAttackTime = 0;
			}
		}
	}

	void MoveToTarget()
	{
		if (targetTransform != null)
		{
			SetNavDestination(targetTransform.position);
		}
	}

    void SetNavDestination(Vector3 targetPosition)
    {
        Vector2 myPosition = myTransform.position;

        if (Mathf.Abs(targetPosition.y - myPosition.y) > 2)
        {
            print("Distance to high");
            return;
        }

        if (targetTransform == null && Mathf.Abs(Vector3.Distance(myPosition, targetPosition)) <= stoppingDistance)
        {
            goingBack = false;
            print("No need to move");
            return;
        }

        if (targetTransform != null && Vector2.Distance(myPosition, targetPosition) < AttackDistance)
        {
            AttackTarget();
            return;
        }

        anim.wrapMode = WrapMode.Loop;
        anim.CrossFade("Walk");

        // TODO Set rotation based on MonsterInput
        print(myPosition.x);
        print(targetPosition.x);
        if (myPosition.x - targetPosition.x < 0)
        {
            modelGO.transform.Rotate(0, 180, 0);
            MonsterInput.x = 0.5f;
            MonsterInput.y = 0;
        }
        else
        {
            modelGO.transform.Rotate(0, 180, 0);
            MonsterInput.x = -0.5f;
            MonsterInput.y = 0;
        }

        int wallDirX = (controller.collisions.left) ? -1 : 1;
        float targetVelocityX = MonsterInput.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);

        bool wallSliding = false;
        if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0)
        {
            print("wall sliding?");
            wallSliding = true;

            if (velocity.y < -wallSlideSpeedMax)
            {
                velocity.y = -wallSlideSpeedMax;
            }

            if (timeToWallUnstick > 0)
            {
                velocityXSmoothing = 0;
                velocity.x = 0;

                if (MonsterInput.x != wallDirX && MonsterInput.x != 0)
                {
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
        controller.Move(velocity, MonsterInput);
    }

    private void AttackTarget()
    {
        if (Time.time - lastAttackTime > attackTime)
        {
            // TODO Fix error with animation iteration
            lastAttackTime = Time.time;
            anim.wrapMode = WrapMode.Once;
            anim.CrossFade("Attack");
            anim.wrapMode = WrapMode.Loop;
            anim.CrossFadeQueued("Idle");
            // TODO Add code to damage player
        }
    }

    public void ReceiveDamage(float damage)
    {
        Health -= damage;
        if (damage <= 0)
        {
            anim.wrapMode = WrapMode.Once;
            anim.CrossFade("Dead");
            Destroy(gameObject, 1.5f);
        }
    }

	IEnumerator DoCheck()
	{
        for (;;)
		{
			yield return new WaitForSeconds(1.0f);
            bool checkIfTarget = false;

            if (Mathf.Abs(Vector3.Distance(originalPosition, myTransform.position)) >= maxFollowDistance)
            {
                targetTransform = null;
                velocity = new Vector3(0, 0, 0);
                goingBack = true;
            } else
            {
                checkIfTarget = true;
            }

            if (checkIfTarget && !goingBack)
            {
                SearchForTarget();
            }
            else
            {
                print("Paso por aca");
                SetNavDestination(originalPosition);
            }
		}
	}

}

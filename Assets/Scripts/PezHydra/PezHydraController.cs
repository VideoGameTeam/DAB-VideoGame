using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PezHydra2D))]
public class PezHydraController : MonoBehaviour
{
    public float Health;

    private Transform myTransform;
	public Transform targetTransform;
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
    float velocityXSmoothing;
    float jump;
    private float lastAttackTime;
    public float attackTime;

    private Transform modelGO; 
    private Animation anim;


	void Start () 
	{
		myTransform = transform;
		raycastLayer = 1 << LayerMask.NameToLayer("Player");

        controller = GetComponent<PezHydra2D>();

        modelGO = transform.FindChild("PezHydraModel");
	    anim = modelGO.GetComponent<Animation>();

		StartCoroutine(DoCheck());
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
			SetNavDestination(targetTransform);
		}
	}

    void SetNavDestination(Transform dest)
    {
        Vector2 myPosition = myTransform.position;
        Vector2 targetPosition = dest.position;

        if (Mathf.Abs(targetPosition.y - myPosition.y) > 2)
        {
            print("Distance to high");
            return;
        }

        if (Vector2.Distance(myPosition, targetPosition) < AttackDistance)
        {
            AttackTarget();
            return;
        }

        anim.wrapMode = WrapMode.Loop;
        anim.CrossFade("Walk");

        // TODO Set rotation based on MonsterInput
        if (myPosition.x - targetPosition.x < 0)
        {
            MonsterInput.x = 1;
            MonsterInput.y = 0;
        }
        else
        {
            MonsterInput.x = -1;
            MonsterInput.y = 0;
        }

        int wallDirX = (controller.collisions.left) ? -1 : 1;
        float targetVelocityX = MonsterInput.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);

        bool wallSliding = false;
        if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0)
        {
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
        // TODO Optimize iteration, move every frame but chase only certain frames
        for (;;)
		{
			yield return new WaitForSeconds(0.02f);
			if (targetTransform != null)
			{
				if (Time.time - originTime >= followTime)
				{
					targetTransform = null;
				}
			}
			SearchForTarget();
			MoveToTarget();
		}
	}

}

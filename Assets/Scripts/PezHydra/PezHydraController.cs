using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PezHydra2D))]
public class PezHydraController : MonoBehaviour
{
    public float Health;

    private Transform myTransform;
	public Transform targetTransform;
    private Player playerScript;
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
    private bool goingBack = false;
    private bool goingRight;
    float velocityXSmoothing;
    float jump;
    private float lastAttackTime;
    public float attackTime;

    private Transform modelGO; 
    private Animation anim;

	public float deltay;


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
            SetNavDestination(targetTransform.position);
        }

        if (goingBack)
        {
            SetNavDestination(originalPosition);
        }
    }


    void _SetOrientation(Vector2 input)
    {
        if(input.x >= 0){
            modelGO.transform.rotation = Quaternion.Euler(0, 90, 0);
            goingRight = true;
        } else if (input.x < 0){
            modelGO.transform.rotation = Quaternion.Euler(0, 270, 0);
            goingRight = false;
        }
    }

    void SetNavDestination(Vector3 targetPosition)
    {
        Vector2 myPosition = myTransform.position;

        if (Mathf.Abs(targetPosition.y - myPosition.y) > deltay)
            return;

        if (targetTransform == null && Mathf.Abs(Vector3.Distance(myPosition, targetPosition)) <= stoppingDistance)
        {
            velocity.x = 0;
            velocity.y = 0;
            goingBack = false;
            return;
        }

        if (targetTransform != null && Vector2.Distance(myPosition, targetPosition) < AttackDistance)
        {
            AttackTarget();
            return;
        }

        anim.wrapMode = WrapMode.Loop;
        anim.CrossFade("Walk");

        if (myPosition.x < targetPosition.x)
        {
            MonsterInput.x = 0.5f;
            MonsterInput.y = 0;
        }

        if (myPosition.x > targetPosition.x)
        {
            MonsterInput.x = -0.5f;
            MonsterInput.y = 0;
        }

        _SetOrientation(MonsterInput);
        
        int wallDirX = (controller.collisions.left) ? -1 : 1;
        float targetVelocityX = MonsterInput.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);

        if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0)
        {
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
		velocity.y = -100;
        controller.Move(velocity, MonsterInput);
    }

    private void AttackTarget()
    {
        if (Time.time - lastAttackTime > attackTime)
        {
            lastAttackTime = Time.time;
            anim.wrapMode = WrapMode.Once;
            anim.CrossFade("Attack");
            anim.wrapMode = WrapMode.Loop;
            anim.CrossFadeQueued("Idle");
            playerScript.PlayPlayerDamage();
            Gamestate.EstadoJuego.ChangeHealth (-10);
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

    void _SearchForTarget()
    {
        if (targetTransform == null)
        {
            Collider2D hitCollider = Physics2D.OverlapCircle(myTransform.position, radius, raycastLayer);

            if (hitCollider)
            {
                targetTransform = hitCollider.transform;
                playerScript = targetTransform.GetComponent<Player>();
                originTime = Time.time;
                lastAttackTime = 0;
            }
        }
    }

    IEnumerator DoCheck()
	{
        for (;;)
		{
			yield return new WaitForSeconds(0.5f);
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
                _SearchForTarget();
            }
		}
	}

}

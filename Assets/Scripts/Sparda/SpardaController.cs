using UnityEngine;
using System.Collections;

public class SpardaController : MonoBehaviour {

    private enum MonsterState
    {
        IDLE,
        SHOOTING,
        DEAD
    };

    public float health;
    
    public float visionDistance;
    public float followDistance;
    public float gunRotationAngle;
    private Quaternion gunRotation;

    public bool shootingStarted;

    public Transform enemyTarget;
    public Transform myTransform;

    public Vector2 originalPosition;
    private LayerMask playerLayerMask;
    private Transform gunCenter;
    private Transform gun;

    // Shoot variables
    public float cadence;
    public GameObject shootPrefab;

    // Animator variables
    private Animator spardaAnimator;
    private MonsterState monsterState;
    private int idleHash;
    private int shootingHash;
    private int deadHash;

    void Start ()
    {
        shootingStarted = false;
        enemyTarget = null;
        myTransform = transform;
        originalPosition = myTransform.position;
        playerLayerMask = 1 << LayerMask.NameToLayer("Player");

        // Get the gun related components
        gunCenter = transform.FindChild("GunCenter");
        gun = gunCenter.FindChild("Gun");
        gunRotationAngle = 0;

        // Set animator required variables
	    spardaAnimator = myTransform.Find("SpardaModel").transform.GetComponent<Animator>();
        idleHash = Animator.StringToHash("Resting");
        shootingHash = Animator.StringToHash("Shoot");
        deadHash = Animator.StringToHash("Dead");
    }
	
	void Update ()
    {
        if (!enemyTarget)
        {
            Collider2D hitCollider = Physics2D.OverlapCircle(myTransform.position, visionDistance, playerLayerMask);
            if(hitCollider)
            {
                enemyTarget = hitCollider.transform;
            }
        }

        else if (enemyTarget)
        {
            if(Vector2.Distance(myTransform.position, enemyTarget.position) > followDistance)
            {
                enemyTarget = null;
                shootingStarted = false;
                StopAllCoroutines();
            }
            else
            {
                var sign = Mathf.Sign(myTransform.position.x - enemyTarget.position.x);
                Vector2 enemyPosition = enemyTarget.position;
                gunRotationAngle = sign * Vector2.Angle(originalPosition, enemyPosition);
                gunRotation = Quaternion.Euler(gunCenter.rotation.x, gunCenter.rotation.y, gunRotationAngle);
                gunCenter.localRotation = gunRotation;

                if (!shootingStarted)
                {
                    StartCoroutine("ShootPlayer");
                    shootingStarted = true;
                }
                else
                {
                    monsterState = MonsterState.IDLE;
                }
            }
        }

        SetAnimationController(monsterState);


    }

    public void ReceiveDamage(float damage)
    {
        health -= damage;

        if (health > 0) return;

        monsterState = MonsterState.DEAD;
        Destroy(gameObject, 4.0f);
    }

    private IEnumerator ShootPlayer()
    {
        for(;;)
        {
            monsterState = MonsterState.SHOOTING;
            SetAnimationController(monsterState);
            Instantiate(shootPrefab, gun.position, gunCenter.rotation);
            yield return new WaitForSeconds(cadence);
        }
    }

    private void SetAnimationController(MonsterState currentState)
    {
        if (currentState == MonsterState.IDLE)
            spardaAnimator.SetTrigger(idleHash);
        if (currentState == MonsterState.SHOOTING)
            spardaAnimator.SetTrigger(shootingHash);
        if (currentState == MonsterState.DEAD)
            spardaAnimator.SetTrigger(deadHash);
    }
}

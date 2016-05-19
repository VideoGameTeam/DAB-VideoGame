using UnityEngine;
using System.Collections;

public class SpardaController : MonoBehaviour {

    private enum MonsterState
    {
        IDLE,
        SHOOTING,
        DEAD
    };

    public float visionDistance;
    public float followDistance;
    public float gunRotationAngle;
    private Quaternion gunRotation;

    public bool shootingStarted;

    public Transform enemyTarget;
    public Transform myTransform;
    private Transform myModelTransform;
    private Player enemyPlayer;
    private bool attack;
    public bool shouldCheckSpeed;

    private LayerMask playerLayerMask;
    private Transform gunCenter;
    private Transform gun;

    // Calculate monster - player rotation variables
    Vector3 originalPosition;
    Vector3 targetPosition;
    Vector3 _targetPosition;
    float temporal_y;
    float base_rot;
    float sign;
    Quaternion _lookRotation;
    Vector3 eulerRotation;
    Quaternion spardaRotation;

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
        myModelTransform = myTransform.Find("SpardaModel").transform;
        originalPosition = myTransform.position;
        playerLayerMask = 1 << LayerMask.NameToLayer("Player");
        attack = false;

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

    bool _checkEnemyPlayer(Player player)
    {
        if (player.moveSpeed > 4 || !shouldCheckSpeed)
        {
            return true;
        }
        return false;
    }

    void _SetOrientation()
    {
        if (!enemyTarget)
        {
            spardaRotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            if (enemyTarget.position.x - myTransform.position.x > 0)
            {
                spardaRotation = Quaternion.Euler(0, 90, 0);
            }
            else
            {
                spardaRotation = Quaternion.Euler(0, 270, 0);
            }
        }
        myModelTransform.rotation = spardaRotation;
    }
	
	void Update ()
    {
        if (!enemyTarget)
        {
            Collider2D hitCollider = Physics2D.OverlapCircle(myTransform.position, visionDistance, playerLayerMask);
            if(hitCollider)
            {
                enemyTarget = hitCollider.transform;
                enemyPlayer = enemyTarget.GetComponent<Player>();
                attack = _checkEnemyPlayer(enemyPlayer);
            }
        }

        else if (enemyTarget && !attack)
        {
            attack = _checkEnemyPlayer(enemyPlayer);
        }

        else if (enemyTarget && attack)
        {
            if(Vector2.Distance(myTransform.position, enemyTarget.position) >= followDistance)
            {
                enemyTarget = null;
                enemyPlayer = null;
                attack = false;
                shootingStarted = false;
                StopAllCoroutines();
            }
            else
            {
                originalPosition = gunCenter.position;
                targetPosition = enemyTarget.position;
                _targetPosition = targetPosition - originalPosition;
                temporal_y = gunCenter.rotation.eulerAngles.y;
                base_rot = 90.0f;
                sign = -1;

                _targetPosition.z = originalPosition.z;
                _lookRotation = Quaternion.LookRotation(_targetPosition.normalized);
                eulerRotation = _lookRotation.eulerAngles;

                if (targetPosition.x - originalPosition.x > 0)
                {
                    base_rot = -90.0f;
                    sign = 1;
                }
                
                gunCenter.rotation = Quaternion.Euler(
                    eulerRotation.z,
                    temporal_y,
                    base_rot + (eulerRotation.x * -sign)
                );

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
        _SetOrientation();
        SetAnimationController(monsterState);
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

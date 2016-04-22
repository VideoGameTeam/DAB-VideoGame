using UnityEngine;
using System.Collections;

public class MonsterTarget : MonoBehaviour {

	private NavMeshAgent agent;
	private Transform myTransform;
	public Transform targetTransform;
	private LayerMask raycastLayer;
	private float radius = 10;
	private float originTime = .0f;
	private float followTime = 3f;
	private Vector3 originalPosition;


	void Start () 
	{
		agent = GetComponent<NavMeshAgent>();
		myTransform = transform;
		raycastLayer = 1 << LayerMask.NameToLayer("Player");
		originalPosition = myTransform.position;

		StartCoroutine(DoCheck());

	}

	void SearchForTarget()
	{
		
		if (targetTransform == null)
		{
			Collider[] hitColliders = Physics.OverlapSphere(myTransform.position, radius, raycastLayer);

			if (hitColliders.Length>0)
			{
				int randomint = Random.Range(0, hitColliders.Length);
				targetTransform = hitColliders[randomint].transform;
				originTime = Time.time;
			}
		}

		if (targetTransform != null && !targetTransform.GetComponent<CapsuleCollider>().enabled)
		{
			targetTransform = null;
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
		agent.SetDestination(dest.position);
	}

	IEnumerator DoCheck()
	{
		for (; ; )
		{
			yield return new WaitForSeconds(0.2f);
			if (targetTransform != null)
			{
				if (Time.time - originTime >= followTime)
				{
					targetTransform = null;
					agent.SetDestination(originalPosition);

				}
			}
			SearchForTarget();
			MoveToTarget();
		}
	}

}

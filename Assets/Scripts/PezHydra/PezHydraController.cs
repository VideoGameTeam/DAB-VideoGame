using UnityEngine;
using System.Collections;

public class PezHydraController : MonoBehaviour {

	private NavMeshAgent agent;
	private Transform myTransform;
	public Transform targetTransform;
	private LayerMask raycastLayer;
	public float radius = 10;
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
			Collider2D hitCollider = Physics2D.OverlapCircle(myTransform.position, radius, raycastLayer);

			if (hitCollider)
			{
				targetTransform = hitCollider.transform;
				originTime = Time.time;
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

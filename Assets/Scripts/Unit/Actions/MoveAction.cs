using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction
{
	private readonly Animator unitAnimator;
	private readonly Unit unit;
	private int maxMoveDistance;
	public bool moveRequested;

	private Vector3 targetPosition;

	public MoveAction(Unit unit, Animator unitAnimator, int maxMoveDistance = 4)
	{
		this.unit = unit;
		this.unitAnimator = unitAnimator;

		targetPosition = unit.transform.position;
		this.maxMoveDistance = maxMoveDistance;
	}

	public void Update()
	{
		if (!moveRequested) return;
		float stoppingDistance = .1f;
		if (Vector3.Distance(unit.transform.position, targetPosition) > stoppingDistance)
		{
			Vector3 moveDirection = (targetPosition - unit.transform.position).normalized;
			float moveSpeed = 4f;
			unit.transform.position += moveDirection * moveSpeed * Time.deltaTime;

			float rotateSpeed = 10f;
			unit.transform.forward = Vector3.Lerp(unit.transform.forward, moveDirection, Time.deltaTime * rotateSpeed);

			unitAnimator.SetBool("IsWalking", true);
		}
		else
		{
			unitAnimator.SetBool("IsWalking", false);
			moveRequested = false;
		}
	}

	public void Move(Vector3 targetPosition)
	{
		this.targetPosition = targetPosition;
		moveRequested = true;
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : BaseAction
{
	private readonly Animator unitAnimator;
	private int maxMoveDistance;
	public bool moveRequested;

	private Vector3 targetPosition;

	public MoveAction(Unit unit, Animator unitAnimator, int maxMoveDistance = 2) : base(unit)
	{
		this.unitAnimator = unitAnimator;

		targetPosition = unit.transform.position;
		this.maxMoveDistance = maxMoveDistance;
		ShowGrid = true;
	}

	public override void Update()
	{
		if (!isActive) return;
		if (!moveRequested) return;
		float stoppingDistance = .1f;
		Vector3 moveDirection = (targetPosition - unit.transform.position).normalized;
		if (Vector3.Distance(unit.transform.position, targetPosition) > stoppingDistance)
		{

			float moveSpeed = 4f;
			unit.transform.position += moveDirection * moveSpeed * Time.deltaTime;
			unitAnimator.SetBool("IsWalking", true);
		}
		else
		{
			unitAnimator.SetBool("IsWalking", false);
			moveRequested = false;
			onActionComplete?.Invoke();
		}

		float rotateSpeed = 10f;
		unit.transform.forward = Vector3.Lerp(unit.transform.forward, moveDirection, Time.deltaTime * rotateSpeed);
	}





	public override List<GridPosition> GetValidGridPositionList()
	{
		List<GridPosition> validGridPos = new List<GridPosition>();

		GridPosition currentGridPos = unit.gridPosition;


		for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
		{
			for (int z = -maxMoveDistance; z <= maxMoveDistance; z++)
			{
				GridPosition offset = new GridPosition(x, z);
				GridPosition newGridPos = currentGridPos + offset;

				if (!LevelGrid.Instance.IsValidGridPosition(newGridPos)) continue; // Check if the grid position is vali

				if (newGridPos == currentGridPos) continue; // Check if the grid position is the same as the unit's current grid positio

				if (LevelGrid.Instance.IsGridPositionOccupied(newGridPos)) continue; // Check if the grid position is occupie


				validGridPos.Add(newGridPos);
			}
		}

		return validGridPos;
	}

	public override string ToString()
	{
		return $"Move";
	}

	public override void TakeAction(GridPosition targetPosition, Action onActionComplete)
	{
		if (!IsValidGridPositionAction(targetPosition)) return;
		isActive = true;
		this.onActionComplete = onActionComplete;
		this.targetPosition = LevelGrid.Instance.GetWorldPosition(targetPosition);
		moveRequested = true;
	}
}

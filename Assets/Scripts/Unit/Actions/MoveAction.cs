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

	public MoveAction(Unit unit, Animator unitAnimator, int maxMoveDistance = 2)
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

	public void Move(GridPosition targetPosition)
	{
		if (!IsValidGridPositionAction(targetPosition)) return;

		this.targetPosition = LevelGrid.Instance.GetWorldPosition(targetPosition);
		moveRequested = true;
	}

	public bool IsValidGridPositionAction(GridPosition gridPosition)
	{
		List<GridPosition> validGridPositionList = GetValidGridPositionList();
		return validGridPositionList.Contains(gridPosition);
	}

	public List<GridPosition> GetValidGridPositionList()
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
}

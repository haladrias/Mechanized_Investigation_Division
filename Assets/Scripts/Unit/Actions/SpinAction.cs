using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAction : BaseAction
{
	private float totalSpinAmount;
	public SpinAction(Unit unit) : base(unit)
	{
		ShowGrid = true;
	}

	public override void Update()
	{
		if (!isActive) return;

		float sprinAddAmount = 360f * Time.deltaTime;
		unit.transform.eulerAngles += new Vector3(0, sprinAddAmount, 0);

		totalSpinAmount += sprinAddAmount;
		if (totalSpinAmount >= 360f)
		{
			isActive = false;
			totalSpinAmount = 0;
			onActionComplete?.Invoke();
		}
	}

	public override string ToString()
	{
		return $"Spin";
	}

	public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
	{
		this.onActionComplete = onActionComplete;
		isActive = true;
	}

	public override List<GridPosition> GetValidGridPositionList()
	{
		GridPosition unitGridPos = unit.gridPosition;
		return new List<GridPosition> { unitGridPos };
	}
}

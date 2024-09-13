using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAction
{
	protected readonly Unit unit;
	protected bool isActive = false;
	public bool ShowGrid { get; protected set; }
	protected Action onActionComplete;
	public int ActionPointsCost { get; protected set; }

	public BaseAction(Unit unit)
	{
		this.unit = unit;
		isActive = false;
		ActionPointsCost = 1;
	}

	public virtual void Update()
	{

	}
	public bool IsValidGridPositionAction(GridPosition gridPosition)
	{
		List<GridPosition> validGridPositionList = GetValidGridPositionList();
		return validGridPositionList.Contains(gridPosition);
	}
	public abstract void TakeAction(GridPosition gridPosition, Action onActionComplete);

	public abstract List<GridPosition> GetValidGridPositionList();

}

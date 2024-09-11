using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Information about the object in the grid
/// </summary>
public class GridObject
{

	private GridSystem gridSystem;
	private GridPosition gridPosition;
	private Unit unit;

	public GridObject(GridSystem gridSystem, GridPosition gridPosition)
	{
		this.gridSystem = gridSystem;
		this.gridPosition = gridPosition;
	}

	public void SetUnit(Unit unit)
	{
		this.unit = unit;
	}

	public Unit GetUnit()
	{
		return unit;
	}

	public override string ToString()
	{
		return $"{gridPosition}\n{unit}";
	}

}

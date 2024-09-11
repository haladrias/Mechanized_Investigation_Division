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

	public GridObject(GridSystem gridSystem, GridPosition gridPosition)
	{
		this.gridSystem = gridSystem;
		this.gridPosition = gridPosition;
	}

	public override string ToString()
	{
		return gridPosition.ToString();
	}

}

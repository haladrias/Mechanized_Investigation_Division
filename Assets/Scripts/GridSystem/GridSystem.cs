using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GridSystem
{
	public int Width { get; private set; }
	public int Height { get; private set; }
	private float cellSize;
	private GridObject[,] gridObjectArray;

	public GridSystem(int width, int height, float cellSize)
	{
		this.Width = width;
		this.Height = height;
		this.cellSize = cellSize;

		gridObjectArray = new GridObject[width, height];

		for (int x = 0; x < width; x++)
		{
			for (int z = 0; z < height; z++)
			{
				GridPosition gridPosition = new GridPosition(x, z);
				gridObjectArray[x, z] = new GridObject(this, gridPosition);
			}
		}
	}

	public Vector3 GetWorldPosition(Vector3 worldPosition)
	{
		GridPosition gridPosition = GetGridPosition(worldPosition);
		return GetWorldPosition(gridPosition);
	}

	public Vector3 GetWorldPosition(GridPosition gridPosition)
	{
		return new Vector3(gridPosition.x, 0, gridPosition.z) * cellSize;
	}

	public GridPosition GetGridPosition(Vector3 worldPosition)
	{
		return new GridPosition(
			Mathf.RoundToInt(worldPosition.x / cellSize),
			Mathf.RoundToInt(worldPosition.z / cellSize)
		);
	}

	public void CreateDebugObjects(Transform debugPrefab, Transform parent)
	{

		for (int x = 0; x < Width; x++)
		{
			for (int z = 0; z < Height; z++)
			{
				GridPosition gridPosition = new GridPosition(x, z);

				Transform debugTransform = GameObject.Instantiate(debugPrefab, GetWorldPosition(gridPosition), Quaternion.identity);
				debugTransform.parent = parent;
				GridDebugObject gridDebugObject = debugTransform.GetComponent<GridDebugObject>();
				gridDebugObject.SetGridObject(GetGridObject(gridPosition));
			}
		}
	}

	public GridObject GetGridObject(GridPosition gridPosition)
	{
		if (gridPosition.x < 0 || gridPosition.z < 0) return null;
		return gridObjectArray[gridPosition.x, gridPosition.z];
	}

	public bool IsValidGridPosition(GridPosition gridPosition)
	{
		return gridPosition.x >= 0 && gridPosition.x < Width && gridPosition.z >= 0 && gridPosition.z < Height;
	}

}

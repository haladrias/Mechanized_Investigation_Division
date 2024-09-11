using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
	public static LevelGrid Instance { get; private set; }
	[SerializeField] private Transform gridDebugObjectPrefab;
	[SerializeField] private LayerMask mouseLayerMask;


	private GridSystem gridSystem;

	private void Awake()
	{
		gridSystem = new GridSystem(10, 10, 2f);
		Instance = this;
	}
	private void Start()
	{


		// Create debug objects (For testing purposes)
		gridSystem.CreateDebugObjects(gridDebugObjectPrefab);
	}

	public void SetUnitAtGridPosition(Unit unit, Vector3 worldPosition)
	{
		GridPosition gridPosition = gridSystem.GetGridPosition(worldPosition);
		GridObject gridObject = gridSystem.GetGridObject(gridPosition);
		if (gridObject == null) return; // If the grid position is invalid

		gridObject.SetUnit(unit);
	}

	public Unit GetUnitAtGridPosition(GridPosition gridPosition)
	{
		GridObject gridObject = gridSystem.GetGridObject(gridPosition);
		return gridObject.GetUnit();
	}

	public void ClearUnitAtGridPosition(GridPosition gridPosition)
	{
		GridObject gridObject = gridSystem.GetGridObject(gridPosition);
		gridObject.SetUnit(null);
	}

	public GridPosition GetGridPosition(Vector3 worldPosition)
	{
		return gridSystem.GetGridPosition(worldPosition);
	}

}

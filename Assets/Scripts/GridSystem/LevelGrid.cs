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

	public void AddUnitAtGridPosition(GridPosition gridPosition, Unit unit)
	{
		GridObject gridObject = gridSystem.GetGridObject(gridPosition);
		gridObject.AddUnit(unit);
	}

	public List<Unit> GetUnitListAtGridPosition(GridPosition gridPosition)
	{
		GridObject gridObject = gridSystem.GetGridObject(gridPosition);
		return gridObject.GetUnitList();
	}

	public void RemoveUnitAtGridPosition(GridPosition gridPosition, Unit unit)
	{
		GridObject gridObject = gridSystem.GetGridObject(gridPosition);
		gridObject.RemoveUnit(unit);
	}

	public void UnitMovedGridPosition(Unit unit, GridPosition fromGridPosition, GridPosition toGridPosition)
	{
		Debug.Log($"Unit moved from {fromGridPosition} to {toGridPosition}");
		RemoveUnitAtGridPosition(fromGridPosition, unit);

		AddUnitAtGridPosition(toGridPosition, unit);
	}

	public GridPosition GetGridPosition(Vector3 worldPosition)
	{
		return gridSystem.GetGridPosition(worldPosition);
	}

	public Vector3 GetWorldPosition(GridPosition gridPosition)
	{
		return gridSystem.GetWorldPosition(gridPosition);
	}

	public Vector3 GetWorldPosition(Vector3 worldPosition)
	{
		return gridSystem.GetWorldPosition(worldPosition);
	}




}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystemVisual : MonoBehaviour
{
	[SerializeField] private Transform debugPrefab;

	private Transform[,] visualGrid;
	private bool IsUnitSelected = false;
	private void Start()
	{
		SpawnGridVisuals();

		PlayerManager.Instance.OnSelectedUnitChanged += OnSelectedUnitChanged;
	}

	private void OnSelectedUnitChanged(object sender, EventArgs e)
	{
		HideGridVisuals();
		Unit selectedUnit = PlayerManager.Instance.CurrentSelectedUnit;
		if (selectedUnit == null)
		{
			IsUnitSelected = false;
			return;
		}
		ShowGridPositionList(selectedUnit.MoveAction.GetValidGridPositionList());
		IsUnitSelected = true;

	}

	private void Update()
	{
		if (IsUnitSelected) // TODO: Refactor to only when neccessary
		{
			HideGridVisuals();
			Unit selectedUnit = PlayerManager.Instance.CurrentSelectedUnit;
			ShowGridPositionList(selectedUnit.MoveAction.GetValidGridPositionList());
		}
	}

	private void SpawnGridVisuals()
	{
		visualGrid = new Transform[LevelGrid.Instance.GetWidth(), LevelGrid.Instance.GetHeight()];
		for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
		{
			for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++)
			{
				GridPosition gridPosition = new GridPosition(x, z);
				visualGrid[x, z] = Instantiate(debugPrefab, LevelGrid.Instance.GetWorldPosition(gridPosition), Quaternion.identity);
			}
		}

		HideGridVisuals();
	}

	private void HideGridVisuals()
	{
		foreach (var item in visualGrid)
		{
			item.gameObject.SetActive(false);
		}
	}

	private void ShowGridPositionList(List<GridPosition> gridPositionList)
	{
		foreach (var item in gridPositionList)
		{
			visualGrid[item.x, item.z].gameObject.SetActive(true);
			// Debug.Log("Show grid at " + item.x + " " + item.z);
		}
	}

	// DO Show grid when player selected unit


}

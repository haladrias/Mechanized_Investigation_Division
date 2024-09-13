using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystemVisual : MonoBehaviour
{
	public static GridSystemVisual Instance { get; private set; }
	[SerializeField] private Transform debugPrefab;

	private Transform[,] visualGrid;
	private bool ShowGrid = false;

	private void Awake()
	{
		Instance = this;

	}
	private void Start()
	{
		PlayerManager.Instance.OnSelectedUnitChanged += Instance_OnSelectedUnitChanged;
		SpawnGridVisuals();

		HideGridVisuals();
	}

	private void Instance_OnSelectedUnitChanged(object sender, EventArgs e)
	{
		if (PlayerManager.Instance.CurrentSelectedUnit == null)
		{
			Debug.Log("No selected unit");
			DisableGridVisual();
		}
	}

	public void ToggleGridVisual(bool flag)
	{

		if (flag)
		{
			EnableGridVisual();
		}
		else
		{
			DisableGridVisual();
		}
	}

	private void EnableGridVisual()
	{
		Debug.Log("Enable grid visual");
		ShowGridPositionList(PlayerManager.Instance.SelectedAction.GetValidGridPositionList());
		ShowGrid = true;
	}

	private void DisableGridVisual()
	{
		ShowGrid = false;
		HideGridVisuals();
	}


	private void Update()
	{
		if (ShowGrid) // TODO: Refactor to only when neccessary
		{
			HideGridVisuals();

			ShowGridPositionList(PlayerManager.Instance.SelectedAction.GetValidGridPositionList());
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

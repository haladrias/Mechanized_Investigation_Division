using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	public static LevelManager Instance { get; private set; }


	private Unit[] playerUnits;
	private Unit[] aiUnits;

	private int playerUnitNoEnergy = 0;
	private int aiUnitNoEnergy = 0;

	[SerializeField] private int playerStartingRow = 0; // Min row of the grid
	[SerializeField] private int aiStartingRow = 10; // Max row of the grid

	[SerializeField] private GameObject playerUnitPrefab;
	[SerializeField] private GameObject aiUnitPrefab;

	[SerializeField] private int playerUnitCount;
	[SerializeField] private int aiUnitCount;

	public event EventHandler CanEndRoundEvent;

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		TurnSystem.Instance.OnRoundChanged += OnRoundChangedHandler;
		TurnSystem.Instance.OnTurnChanged += OnTurnChangedHandler;
		SpawnUnits();
	}

	private void OnTurnChangedHandler(object sender, TurnSystem.OnTurnChangedEventArgs e)
	{

	}

	private void OnRoundChangedHandler(object sender, System.EventArgs e)
	{
		foreach (var unit in playerUnits)
		{
			unit.RefreshActionPoints();
		}

		foreach (var unit in aiUnits)
		{
			unit.RefreshActionPoints();
		}
	}

	public void SpawnUnits()
	{

		aiUnits = new Unit[aiUnitCount];


		playerUnits = new Unit[playerUnitCount];

		SpawnUnits(playerUnits, playerUnitCount, "Player", playerUnitPrefab, playerStartingRow);
		SpawnUnits(aiUnits, aiUnitCount, "Enemy", aiUnitPrefab, aiStartingRow);

		foreach (var item in aiUnits)
		{
			item.transform.rotation = Quaternion.Euler(0, 180, 0);
		}

	}

	private void SpawnUnits(Unit[] array, int count, string tag, GameObject prefab, int startingRow)
	{
		List<int> spawnedColumns = new List<int>();

		for (int i = 0; i < count; i++)
		{
			// Increment row if necessary
			if (i >= LevelGrid.Instance.GetWidth()) // If the number of units is greater than the width of the grid
			{
				startingRow++;  // Move to the next row
				spawnedColumns.Clear(); // Clear the columns for the new row
			}

			// Find a unique column that has not been used
			int column;
			do
			{
				column = UnityEngine.Random.Range(0, LevelGrid.Instance.GetWidth());
			} while (spawnedColumns.Contains(column));

			// Add the column to the list of used columns
			spawnedColumns.Add(column);

			// Set spawn position and instantiate the unit
			GridPosition spawnPos = new GridPosition(column, startingRow);
			array[i] = Instantiate(prefab, LevelGrid.Instance.GetWorldPosition(spawnPos), Quaternion.identity).GetComponent<Unit>();
			array[i].tag = tag;
			array[i].OnActionPointsChanged += OnActionPointsChangedHandler;
		}
	}

	private void OnActionPointsChangedHandler(object sender, Unit.ActionPointsChangedEventArgs e)
	{
		Unit unit = sender as Unit;
		if (unit.transform.tag == "Player")
		{
			if (e.actionPoints == 0) playerUnitNoEnergy++;
		}
		else if (unit.transform.tag == "Enemy")
		{
			if (e.actionPoints == 0) aiUnitNoEnergy++;
		}
		CheckIfAllUnitsHaveNoEnergy();
	}

	private void CheckIfAllUnitsHaveNoEnergy()
	{
		if (playerUnitNoEnergy == playerUnits.Length && aiUnitNoEnergy == aiUnits.Length)
		{
			CanEndRoundEvent?.Invoke(this, EventArgs.Empty);
			Debug.Log("End Round");
		}

	}
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Testing purposes for the grid system
/// </summary>
public class GridTesting : MonoBehaviour
{
	public Unit unit;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.T))
		{
			foreach (var item in unit.MoveAction.GetValidGridPositionList())
			{
				Debug.Log("Valid Grid Position: " + item.x + " " + item.z);
			}
		}
	}
}

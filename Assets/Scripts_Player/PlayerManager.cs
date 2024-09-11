using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
	public static PlayerManager Instance { get; private set; }
	private Unit selectedUnit;
	public Unit CurrentSelectedUnit => selectedUnit;

	public LayerMask unitLayerMask;
	public event EventHandler OnSelectedUnitChanged;

	private void Awake()
	{
		Instance = this;
	}

	private void OnEnable()
	{
		OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0)) // LMB 
		{
			if (selectedUnit != null) selectedUnit.OnDeselected();
			selectedUnit = null; // Clear selected unit

			// Raycast from mouse position to get unit
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, unitLayerMask))
			{
				if (!hit.transform.TryGetComponent<Unit>(out selectedUnit)) return;
				selectedUnit.OnSelected();

			}

			OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
		}

		if (Input.GetMouseButtonDown(1)) // RMB
		{
			if (selectedUnit != null) // Move Unit to mouse position
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue))
				{
					selectedUnit.Move(hit.point);
				}
			}
		}
	}

}

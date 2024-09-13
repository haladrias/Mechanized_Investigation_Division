using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerManager : MonoBehaviour
{
	public static PlayerManager Instance { get; private set; }
	public event EventHandler OnUnitSelected;
	public event EventHandler OnUnitDeselected;
	public event EventHandler<SelectedActionChangedEventArgs> OnSelectedActionChanged;
	public class SelectedActionChangedEventArgs : EventArgs
	{
		public BaseAction selectedAction;
	}
	public event EventHandler<BusyChangedEventArgs> OnBusyChanged;
	public class BusyChangedEventArgs : EventArgs
	{
		public bool isBusy;
	}

	public LayerMask unitLayerMask;
	[SerializeField] private LayerMask mouseLayerMask;

	private Unit selectedUnit;
	public Unit CurrentSelectedUnit => selectedUnit;
	public BaseAction SelectedAction { get; private set; }


	private bool IsBusy;
	private void Awake()
	{
		Instance = this;
		IsBusy = false;
	}

	private void OnEnable()
	{
		OnUnitSelected?.Invoke(this, EventArgs.Empty);
		OnSelectedActionChanged?.Invoke(this, new SelectedActionChangedEventArgs { selectedAction = SelectedAction });
	}

	private void Update()
	{

		if (Input.GetMouseButtonDown(0)) // LMB 
		{
			if (EventSystem.current.IsPointerOverGameObject()) return; // If mouse is over UI, return

			if (selectedUnit != null)
			{
				OnUnitDeselected?.Invoke(this, EventArgs.Empty);
				selectedUnit.OnDeselected();
			}
			selectedUnit = null; // Clear selected unit

			// Raycast from mouse position to get unit
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, unitLayerMask))
			{
				if (!hit.transform.TryGetComponent<Unit>(out selectedUnit))
				{
					SelectedAction = null;
					OnSelectedActionChanged?.Invoke(this, new SelectedActionChangedEventArgs { selectedAction = SelectedAction });
					return;
				};
				selectedUnit.OnSelected();

			}

			OnUnitSelected?.Invoke(this, EventArgs.Empty);
		}

		if (Input.GetMouseButtonDown(1)) // RMB
		{
			if (IsBusy) return;
			SetIsBusy();
			if (selectedUnit != null) // Move Unit to mouse position
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, mouseLayerMask))
				{

					Vector3 targetPosition = LevelGrid.Instance.GetWorldPosition(hit.point);
					GridPosition gridPosition = LevelGrid.Instance.GetGridPosition(targetPosition);

					if (selectedUnit.TrySpendActionPoints(SelectedAction))
						SelectedAction.TakeAction(gridPosition, SetIsNotBusy);
				}
			}
		}
	}

	public void SetIsBusy()
	{
		IsBusy = true;
		OnBusyChanged?.Invoke(this, new BusyChangedEventArgs { isBusy = IsBusy });
	}
	public void SetIsNotBusy()
	{
		IsBusy = false;
		OnBusyChanged?.Invoke(this, new BusyChangedEventArgs { isBusy = IsBusy });
	}
	public void SetSelectedAction(BaseAction action)
	{
		// if (SelectedAction != null)

		// if (action == SelectedAction) return;
		SelectedAction = action;
		GridSystemVisual.Instance.ToggleGridVisual(action.ShowGrid);
		// switch (SelectedAction)
		// {
		// 	case MoveAction moveAction:
		// 		Debug.Log("Move Action Selected");
		// 		break;
		// 	case SpinAction spinAction:
		// 		Debug.Log("Spin Action Selected");
		// 		break;
		// }
		OnSelectedActionChanged?.Invoke(this, new SelectedActionChangedEventArgs { selectedAction = SelectedAction });
	}

	// public void TakeAction()
	// {
	// 	if (selectedAction == null) return;

	// 	selectedAction.
	// }

}

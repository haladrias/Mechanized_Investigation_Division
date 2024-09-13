using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
	[SerializeField] private Animator unitAnimator;
	[SerializeField] private Transform visualSelect;
	public GridPosition gridPosition { get; private set; }

	public List<BaseAction> baseActionArray;
	public MoveAction MoveAction { get; private set; }
	public SpinAction SpinAction { get; private set; }

	// Add Unit Mech Portrait
	// Add Weapon Info
	// Add Mech Info

	private void Awake()
	{
		OnDeselected();

		MoveAction = new MoveAction(this, unitAnimator);
		SpinAction = new SpinAction(this);
		baseActionArray = new List<BaseAction> { MoveAction, SpinAction };
	}

	private void Start()
	{
		gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
		LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);

	}

	private void Update()
	{
		MoveAction.Update();
		SpinAction.Update();
		GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);

		if (newGridPosition != gridPosition)
		{
			// Unit changed Grid Position
			LevelGrid.Instance.UnitMovedGridPosition(this, gridPosition, newGridPosition);
			gridPosition = newGridPosition;
		}

	}



	public void OnSelected()
	{
		visualSelect.gameObject.SetActive(true);
	}

	public void OnDeselected()
	{
		visualSelect.gameObject.SetActive(false);
	}

}

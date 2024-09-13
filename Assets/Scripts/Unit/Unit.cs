using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
	[SerializeField] private Animator unitAnimator;
	[SerializeField] private Transform visualSelect;
	[SerializeField] private Transform cameraPortfolio;
	public GridPosition gridPosition { get; private set; }

	public List<BaseAction> baseActionArray;
	public MoveAction MoveAction { get; private set; }
	public SpinAction SpinAction { get; private set; }

	public event EventHandler<ActionPointsChangedEventArgs> OnActionPointsChanged;
	public class ActionPointsChangedEventArgs : EventArgs
	{
		public int actionPoints;
	}
	[SerializeField] private int actionPoints = 2;

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

	public bool TrySpendActionPoints(BaseAction action)
	{
		if (CanSpendActionPoints(action))
		{
			SpendActionPoints(action.ActionPointsCost);
			return true;
		}
		return false;
	}

	public bool CanSpendActionPoints(BaseAction action)
	{
		if (actionPoints >= action.ActionPointsCost)
		{
			return true;
		}
		return false;
	}

	private void SpendActionPoints(int amount)
	{
		actionPoints -= amount;
		OnActionPointsChanged?.Invoke(this, new ActionPointsChangedEventArgs { actionPoints = actionPoints });
	}

	public int ActionPoints => actionPoints;


	public void OnSelected()
	{
		visualSelect.gameObject.SetActive(true);
		cameraPortfolio.gameObject.SetActive(true);
	}

	public void OnDeselected()
	{
		visualSelect.gameObject.SetActive(false);
		cameraPortfolio.gameObject.SetActive(false);
	}

}

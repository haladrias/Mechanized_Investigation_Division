using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
	[SerializeField] private Animator unitAnimator;
	[SerializeField] private Transform visualSelect;
	private GridPosition gridPosition;
	public MoveAction MoveAction { get; private set; }

	private void Awake()
	{
		OnDeselected();
		MoveAction = new MoveAction(this, unitAnimator);
	}

	private void Start()
	{
		gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
		LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);

	}

	private void Update()
	{
		MoveAction.Update();
		GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);

		if (newGridPosition != gridPosition)
		{
			// Unit changed Grid Position
			Debug.Log("Unit changed Grid Position");
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

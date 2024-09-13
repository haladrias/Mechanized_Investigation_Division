using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MechInfoUI : MonoBehaviour
{
	public static MechInfoUI Instance { get; private set; }
	[SerializeField] private Transform parentContainer;
	[SerializeField] private Transform actionsContainer;
	[SerializeField] private Transform actionButtonPrefab;
	[SerializeField] private TextMeshProUGUI actionText;
	[SerializeField] private TextMeshProUGUI actionPointsText;

	private Unit currentUnit;
	// [SerializeField] private Button takeActionButton;

	private void Awake()
	{
		Instance = this;
	}
	private void Start()
	{
		PlayerManager.Instance.OnUnitSelected += Instance_OnSelectedUnit;
		PlayerManager.Instance.OnUnitDeselected += OnUnitDeselectedHandler;
		PlayerManager.Instance.OnSelectedActionChanged += Instance_OnSelectedActionChanged;
		ClearActionButtons();
		parentContainer.gameObject.SetActive(false); // Hide the container

		// takeActionButton.onClick.AddListener(() =>
		// {
		// 	Unit selectedUnit = PlayerManager.Instance.CurrentSelectedUnit;

		// });
	}

	private void OnUnitDeselectedHandler(object sender, EventArgs e)
	{
		Unit selectedUnit = PlayerManager.Instance.CurrentSelectedUnit;

		selectedUnit.OnActionPointsChanged -= SelectedUnit_OnActionPointsChanged;
	}

	private void Instance_OnSelectedActionChanged(object sender, PlayerManager.SelectedActionChangedEventArgs e)
	{
		if (e.selectedAction == null)
		{
			actionText.text = "NA";
			return;
		}
		actionText.text = e.selectedAction.ToString();
	}

	private void Instance_OnSelectedUnit(object sender, EventArgs e)
	{
		Toggle();
		Unit selectedUnit = PlayerManager.Instance.CurrentSelectedUnit;
		if (selectedUnit != null)
		{
			actionPointsText.text = selectedUnit.ActionPoints.ToString();
			selectedUnit.OnActionPointsChanged += SelectedUnit_OnActionPointsChanged;
		}

	}

	private void SelectedUnit_OnActionPointsChanged(object sender, Unit.ActionPointsChangedEventArgs e)
	{
		actionPointsText.text = e.actionPoints.ToString();
	}

	private void Toggle()
	{
		Unit selectedUnit = PlayerManager.Instance.CurrentSelectedUnit;

		if (selectedUnit == null)
		{
			parentContainer.gameObject.SetActive(false);
			return;
		}

		parentContainer.gameObject.SetActive(true);

		SpawnActionButtons(selectedUnit);
	}

	private void SpawnActionButtons(Unit selectedUnit)
	{
		ClearActionButtons();

		foreach (var action in selectedUnit.baseActionArray)
		{
			Transform actionTransform = Instantiate(actionButtonPrefab, actionsContainer);
			actionTransform.GetComponent<ActionButtonUI>().SetBaseAction(action);
		}
	}

	private void ClearActionButtons()
	{
		foreach (Transform child in actionsContainer)
		{
			Destroy(child.gameObject);
		}
	}
}

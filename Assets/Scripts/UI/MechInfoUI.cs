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
	// [SerializeField] private Button takeActionButton;

	private void Awake()
	{
		Instance = this;
	}
	private void Start()
	{
		PlayerManager.Instance.OnSelectedUnitChanged += Instance_OnSelectedUnitChanged;
		PlayerManager.Instance.OnSelectedActionChanged += Instance_OnSelectedActionChanged;
		ClearActionButtons();
		parentContainer.gameObject.SetActive(false); // Hide the container

		// takeActionButton.onClick.AddListener(() =>
		// {
		// 	Unit selectedUnit = PlayerManager.Instance.CurrentSelectedUnit;

		// });
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

	private void Instance_OnSelectedUnitChanged(object sender, EventArgs e)
	{
		Toggle();
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

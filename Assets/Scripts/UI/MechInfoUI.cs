using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MechInfoUI : MonoBehaviour
{
	[SerializeField] private Transform parentContainer;
	[SerializeField] private Transform actionsContainer;
	[SerializeField] private Transform actionButtonPrefab;

	private void Start()
	{
		PlayerManager.Instance.OnSelectedUnitChanged += Instance_OnSelectedUnitChanged;
		ClearActionButtons();
		parentContainer.gameObject.SetActive(false); // Hide the container
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

		foreach (var actions in selectedUnit.baseActionArray)
		{
			Transform actionTransform = Instantiate(actionButtonPrefab, actionsContainer);
			actionTransform.GetChild(0).GetComponent<TextMeshProUGUI>().text = actions.ToString();
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

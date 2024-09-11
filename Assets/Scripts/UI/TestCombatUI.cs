using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestCombatUI : MonoBehaviour
{
	public TextMeshProUGUI SelectedUnitName;
	private void Start()
	{
		PlayerManager.Instance.OnSelectedUnitChanged += PlayerManager_OnSelectedUnitChanged;
	}

	private void PlayerManager_OnSelectedUnitChanged(object sender, EventArgs e)
	{
		if (PlayerManager.Instance.CurrentSelectedUnit == null)
		{
			SelectedUnitName.text = ""; // Clear text
			return;
		}
		SelectedUnitName.text = PlayerManager.Instance.CurrentSelectedUnit.transform.name; // Set text to selected unit name
	}
}

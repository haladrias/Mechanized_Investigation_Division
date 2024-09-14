using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystemUI : MonoBehaviour
{
	public TextMeshProUGUI TurnText;
	public Button endTurnButton;

	private void OnEnable()
	{
		TurnSystem.Instance.OnTurnNumberChanged += UpdateTurnText;
		endTurnButton.onClick.AddListener(() => TurnSystem.Instance.EndTurn());
	}

	private void UpdateTurnText(object sender, EventArgs e)
	{
		TurnText.text = $"Turn: {TurnSystem.Instance.TurnNumber}";
	}
}

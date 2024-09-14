using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystemUI : MonoBehaviour
{
	public TextMeshProUGUI RoundText;
	public TextMeshProUGUI TurnText;
	public Button endTurnButton;

	private void OnEnable()
	{
		Debug.Log("TurnSystemUI OnEnable");
		TurnSystem.Instance.OnRoundChanged += UpdateRoundText;
		TurnSystem.Instance.OnTurnChanged += UpdateTurnText;
		endTurnButton.onClick.AddListener(() => TurnSystem.Instance.EndTurn());
	}

	private void UpdateTurnText(object sender, TurnSystem.OnTurnChangedEventArgs e)
	{
		Debug.Log($"Current Turn: {e.currentTurn}");
		TurnText.text = $"{e.currentTurn}";
	}

	private void UpdateRoundText(object sender, EventArgs e)
	{
		RoundText.text = $"{TurnSystem.Instance.RoundNumber}";
	}
}

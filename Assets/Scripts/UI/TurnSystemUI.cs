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
	public Button endRoundButton;

	private void OnEnable()
	{
		Debug.Log("TurnSystemUI OnEnable");
		TurnSystem.Instance.OnRoundChanged += UpdateRoundText;
		TurnSystem.Instance.OnTurnChanged += UpdateTurnText;
		LevelManager.Instance.CanEndRoundEvent += HandleCanRoundEnd;
		endTurnButton.onClick.AddListener(() => TurnSystem.Instance.EndTurn());
		endRoundButton.onClick.AddListener(() => EndRound());
		endRoundButton.interactable = false;
		endTurnButton.interactable = true;
	}

	private void EndRound()
	{
		endRoundButton.interactable = false;
		endTurnButton.interactable = true;
		TurnSystem.Instance.EndRound();
	}

	private void HandleCanRoundEnd(object sender, EventArgs e)
	{
		endRoundButton.interactable = true;
		endTurnButton.interactable = false;
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

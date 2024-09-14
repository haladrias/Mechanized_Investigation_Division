using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
	public static TurnSystem Instance { get; private set; }
	public int RoundNumber { get; private set; }

	public event EventHandler OnRoundChanged;
	public event EventHandler<OnTurnChangedEventArgs> OnTurnChanged;
	public class OnTurnChangedEventArgs : EventArgs
	{
		public TurnState currentTurn;
	}
	public enum TurnState
	{
		Player, // Player
		AI // AI
	}
	public TurnState CurrentTurn { get; private set; }

	private void Awake()
	{
		Instance = this;
		RoundNumber = 0;
		CurrentTurn = TurnState.AI;
	}



	private void Start()
	{
		Debug.Log("TurnSystem Start");
		EndRound();
		EndTurn();
	}

	public void EndRound()
	{
		RoundNumber++;
		OnRoundChanged?.Invoke(this, EventArgs.Empty);
	}

	public void EndTurn()
	{

		switch (CurrentTurn)
		{
			case TurnState.Player:
				CurrentTurn = TurnState.AI;
				break;
			case TurnState.AI:
				CurrentTurn = TurnState.Player;
				break;
		}
		OnTurnChanged?.Invoke(this, new OnTurnChangedEventArgs { currentTurn = CurrentTurn });
	}


}


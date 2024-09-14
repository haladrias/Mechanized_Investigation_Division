using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
	public static TurnSystem Instance { get; private set; }
	public int TurnNumber { get; private set; }

	public event EventHandler OnTurnNumberChanged;
	private void Awake()
	{
		Instance = this;
		TurnNumber = 0;
	}

	private void OnEnable()
	{
		EndTurn(); // Start the game with turn 1
	}

	public void EndTurn()
	{
		TurnNumber++;
		OnTurnNumberChanged?.Invoke(this, EventArgs.Empty);
	}


}


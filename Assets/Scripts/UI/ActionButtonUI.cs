using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionButtonUI : MonoBehaviour
{

	[SerializeField] private Button button;
	[SerializeField] private TextMeshProUGUI text;

	private void Start()
	{
		PlayerManager.Instance.OnBusyChanged += Instance_OnBusyChanged;
	}

	private void Instance_OnBusyChanged(object sender, PlayerManager.BusyChangedEventArgs e)
	{
		if (button != null)
			button.interactable = !e.isBusy;
	}

	public void SetBaseAction(BaseAction baseAction)
	{
		text.text = baseAction.ToString();
		button.onClick.AddListener(() =>
		{
			PlayerManager.Instance.SetSelectedAction(baseAction);
		});
	}
}

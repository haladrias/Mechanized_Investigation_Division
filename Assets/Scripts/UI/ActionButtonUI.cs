using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionButtonUI : MonoBehaviour
{

	[SerializeField] private Button button;
	[SerializeField] private TextMeshProUGUI text;
	public void SetBaseAction(BaseAction baseAction)
	{
		text.text = baseAction.ToString();
		button.onClick.AddListener(() =>
		{
			PlayerManager.Instance.SetSelectedAction(baseAction);
		});
	}
}

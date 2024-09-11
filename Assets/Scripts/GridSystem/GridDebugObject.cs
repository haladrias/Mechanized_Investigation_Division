using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


/// <summary>
/// Testing purposes for the grid system
/// </summary>
public class GridDebugObject : MonoBehaviour
{

	[SerializeField] private TextMeshPro textMeshPro;


	private GridObject gridObject;

	public void SetGridObject(GridObject gridObject)
	{
		this.gridObject = gridObject;
	}

	private void Start()
	{
		textMeshPro.text = gridObject.ToString();
	}

}

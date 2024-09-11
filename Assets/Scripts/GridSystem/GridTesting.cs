using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTesting : MonoBehaviour
{
	[SerializeField] private Transform gridDebugObjectPrefab;


	private GridSystem gridSystem;
	[SerializeField] private LayerMask mouseLayerMask;


	private void Start()
	{
		gridSystem = new GridSystem(10, 10, 2f);
		gridSystem.CreateDebugObjects(gridDebugObjectPrefab);

		// Debug.Log(new GridPosition(5, 7));
	}

	private void Update()
	{
		Debug.Log(gridSystem.GetGridPosition(HelperMethods.GetPosition(mouseLayerMask)));
	}


}

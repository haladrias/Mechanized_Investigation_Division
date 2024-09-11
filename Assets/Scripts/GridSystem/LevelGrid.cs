using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
	[SerializeField] private Transform gridDebugObjectPrefab;


	private GridSystem gridSystem;
	[SerializeField] private LayerMask mouseLayerMask;


	private void Start()
	{
		gridSystem = new GridSystem(10, 10, 2f);

		// Create debug objects (For testing purposes)
		gridSystem.CreateDebugObjects(gridDebugObjectPrefab);
	}



}

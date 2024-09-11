using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperMethods
{


	public static Vector3 GetPosition(LayerMask layerMask)
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, layerMask);
		return raycastHit.point;
	}

	public static Vector3 GetPosition()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue);
		return raycastHit.point;
	}

}

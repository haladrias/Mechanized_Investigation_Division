using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAction
{
	protected readonly Unit unit;
	protected bool isActive;

	public BaseAction(Unit unit)
	{
		this.unit = unit;
	}

	public virtual void Update()
	{
		if (!isActive) return;
	}

}

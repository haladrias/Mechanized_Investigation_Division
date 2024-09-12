using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAction
{
	protected readonly Unit unit;
	protected bool isActive = false;
	protected Action onActionComplete;

	public BaseAction(Unit unit)
	{
		this.unit = unit;
		isActive = false;
	}

	public virtual void Update()
	{

	}

}

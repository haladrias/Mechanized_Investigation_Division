using UnityEngine;
using System.Collections;
public class BattleController : StateMachine
{
	
	void Start()
	{
		ChangeState<InitBattleState>();
	}
}
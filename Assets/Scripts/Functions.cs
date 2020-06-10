using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using System.Diagnostics;

//[Serializable]
public class Functions : MonoBehaviour
{

    public NodeState Test()
	{
		return NodeState.FAILURE;
    }

	public void ThisFunctionWillFuckThingsUp()
	{
		return;
	}

	public NodeState func1()
	{
		return NodeState.FAILURE; 
	}

	public NodeState finc3()
	{
		return NodeState.FAILURE; 
	}
}

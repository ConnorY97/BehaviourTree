using UnityEngine;
using System.Collections;
using UnityEditor;

public enum NodeState
{
	RUNNING,
	SUCCESS,
	FAILURE
}
[System.Serializable]
public abstract class Node
{
	//Parent node 
	private Node m_parent = null;

	private string m_name; 

	/* Delegate that returns the state of the node.*/
	public delegate NodeState NodeReturn();

	/* The current state of the node */
	protected NodeState m_nodeState;

	public NodeState nodeState
	{
		get { return m_nodeState; }
	}

	public void SetParent(Node parent)
	{
		m_parent = parent; 
	}

	public Node GetParent()
	{
		return m_parent; 
	}

	/* The constructor for the node */
	public Node(string name)
	{
		m_name = name; 
	}

	public string GetName()
	{
		return m_name; 
	}

	public abstract Node GetCurrentChild();

	/* Implementing classes use this method to evaluate the desired set of conditions */
	public abstract NodeState Evaluate();


	public abstract void PrintGUIElement(BehaviourTreeEditor src);
	
	//public abstract Node CurrentRunningChild(); 

}















/*
 * new rootacitons(true);
 * connor.add(movetodoor - action);
 * connor.add(findkey -> selector); 
 * 
 * update(connor.currentrunniognchild)
 * 
 * 
 * public nodestate movetodoor()
 * {
 *		if (trans = door.trans)
 *		return succ;
 *		if (trans != door.tran
 *		return running
 * 
 */



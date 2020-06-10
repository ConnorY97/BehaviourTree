using System;
using UnityEngine;
using System.Collections;
using UnityEditor;

public class ActionNode : Node
{
	/* Method signature for the action. */
	public delegate NodeState ActionNodeDelegate();

	/* The delegate that is called to evaluate this node */
	private ActionNodeDelegate m_action;

	/* Because this node contains no logic itself, 
     * the logic must be passed in the form of  
     * a delegate. As the signature states, the action 
     * needs to return a NodeStates enum */
	public ActionNode(ActionNodeDelegate action, string name) : base(name)
	{
		m_action = action;
	}

	public override Node GetCurrentChild()
	{
		return this;
	}

	/* Evaluates the node using the passed in delegate and  
     * reports the resulting state as appropriate */
	public override NodeState Evaluate()
	{
		switch (m_action())
		{
			case NodeState.SUCCESS:
				m_nodeState = NodeState.SUCCESS;
				return m_nodeState;
			case NodeState.FAILURE:
				m_nodeState = NodeState.FAILURE;
				return m_nodeState;
			case NodeState.RUNNING:
				m_nodeState = NodeState.RUNNING;
				return m_nodeState;
			default:
				m_nodeState = NodeState.FAILURE;
				return m_nodeState;
		}
	}

	public override void PrintGUIElement(BehaviourTreeEditor src)
	{
		//string indetationString = "	";
		//for (int i = 0; i < depth; i++)
		//{
		//	indetationString += "	"; 
		//}
		if (GUILayout.Button((this.GetParent() == null ? "" : "Parent: "  + this.GetParent().GetName() + "       ")
			+ this.GetName() + (src.currentNode == this ? "***" : "")
			+ "           Action"))
		{
			src.currentNode = this; 
		}
	}
	//string indentationString = new string();
	//for (int i = 0; i < depth; i++)
	//{
	//	indentationString += "   ";
	//}
	//if(GUI.DoText(indentationString + "TheNameOfTheNode"))
	//{
	//	//Guimanager.selectedNode = this???
	//}
	//public override Node CurrentRunningChild()
	//{
	//	switch (m_action())
	//	{
	//		case NodeStates.SUCCESS:
	//			m_nodeState = NodeStates.SUCCESS;
	//			return m_nodeState;
	//		case NodeStates.FAILURE:
	//			m_nodeState = NodeStates.FAILURE;
	//			return m_nodeState;
	//		case NodeStates.RUNNING:
	//			m_nodeState = NodeStates.RUNNING;
	//			return m_nodeState;
	//		default:
	//			m_nodeState = NodeStates.FAILURE;
	//			return m_nodeState;
	//	}
	//}
}
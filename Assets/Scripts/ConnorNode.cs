using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ConnorNode : Node
{
	//Is this node a sequence or a selector
	protected bool m_isSequence = true;

	private int m_index = 0; 

	//Current running child node 
	protected Node m_runningChild; 

	/** The child nodes for this selector */
	protected List<Node> m_children = new List<Node>();

	/** The constructor requires a list of child nodes to be  
     * passed in*/
	public ConnorNode(List<Node> nodes, string name) : base(name)
	{
		m_children = nodes;
	}

	public ConnorNode(bool isSequence, string name) : base(name)
	{
		m_isSequence = isSequence;
	}

	public void AddChild(Node child)
	{
		//Node temp = new ConnorNode(false, "default");
		m_children.Add(child);
		child.SetParent(this);
	}

	public Node GetRunningChild()
	{
		return m_runningChild; 
	}


	#region Attempts 
	/*
	public Node CurrentRunningChild()
	{
		//Is this node is a selector 
		if (!m_isSequence)
		{
			//If any of the nodes succeed then the selector will exit 
			foreach (Node currentNode in m_children)
			{
				switch (currentNode.Evaluate())
				{
					//If the child fails it will try the next 
					case NodeStates.FAILURE:
						continue;
					//If the child succeeds then it is returned 
					case NodeStates.SUCCESS:
						m_nodeState = NodeStates.SUCCESS;
						return this.GetParent();
					//if the child is running then it is returned; 
					case NodeStates.RUNNING:
						m_nodeState = NodeStates.RUNNING;
						return currentNode;
				}
			}
			//If all the children fail then the selector has failed
			m_nodeState = NodeStates.FAILURE;
			return this.GetParent(); 
		}
		//If this node is a sequence 
		else
		{
			foreach (Node currentNode in m_children)
			{
				switch (currentNode.Evaluate())
				{
					//If any of the children fail, the sequence will quit with a failed state
					case NodeStates.FAILURE:
						m_nodeState = NodeStates.FAILURE;
						return this.GetParent();
					//If the children are succeeding then the loop will continue to run 
					case NodeStates.SUCCESS:
						m_nodeState = NodeStates.RUNNING;
						continue;
					//If any of the children are running then they are returned to be evaluated again 
					case NodeStates.RUNNING:
						m_nodeState = NodeStates.RUNNING;
						return currentNode;
				}
			}
			//If all the child have succeeded then the sequence will end with node state success 
			m_nodeState = NodeStates.SUCCESS;
			return this.GetParent();
		}
	}
	*/


	/*
	public override NodeStates Evaluate()
	{
		//How the node will handle evaluating its children if it is a sequence 
		/* If any child node returns a failure, the entire node fails. Whence all  
		* nodes return a success, the node reports a success. 
		if (m_isSequence)
		{
			bool anyChildRunning = false;
		
			foreach (Node node in m_children)
			{
				switch (node.Evaluate())
				{
					case NodeStates.FAILURE:
						m_nodeState = NodeStates.FAILURE;
						return m_nodeState;
					case NodeStates.SUCCESS:
						continue;
					case NodeStates.RUNNING:
						anyChildRunning = true;
						m_runningChild = node;
						continue;
					default:
						m_nodeState = NodeStates.SUCCESS;
						return m_nodeState;
				}
			}
			m_nodeState = anyChildRunning ? NodeStates.RUNNING : NodeStates.SUCCESS;
			return m_nodeState;
		}
		//How the node will handle evaluating its children if it is a selector																																																									/**/
	/* If any of the children reports a success, the selector will 
	* immediately report a success upwards. If all children fail, 
	* it will report a failure instead.		
	else
	{
		foreach (Node node in m_children)
		{
			switch (node.Evaluate())
			{
				case NodeStates.FAILURE:
					continue;
				case NodeStates.SUCCESS:
					m_nodeState = NodeStates.SUCCESS;
					return m_nodeState;
				case NodeStates.RUNNING:
					m_nodeState = NodeStates.RUNNING;
					m_runningChild = node; 
					return m_nodeState;
				default:
					continue;
			}
		}
		m_nodeState = NodeStates.FAILURE;
		return m_nodeState;
	}
	*/
	#endregion

	public override NodeStates Evaluate()
	{
		if (m_index < m_children.Count)
		{
			//This is a selector, if one child succeeds it will exit 
			if (!m_isSequence)
			{
				switch (m_children[m_index].Evaluate())
				{
					//Selector is still running, move onto next node 
					case NodeStates.FAILURE:
						++m_index;
						m_nodeState = NodeStates.RUNNING;
						return m_nodeState;
					//Selector has succeeded and now will exit 
					case NodeStates.SUCCESS:
						//Still want to increase the index to show that all the children have been evaluated 
						++m_index; 
						m_nodeState = NodeStates.SUCCESS;
						return m_nodeState;
					//Child is still running so evaluate child
					case NodeStates.RUNNING:
						m_nodeState = NodeStates.RUNNING;
						m_runningChild = m_children[m_index];
						return m_nodeState;
				}
			}
			//This is a sequence, if one child fails it will exit
			else
			{
				switch (m_children[m_index].Evaluate())
				{
					//If the node fails the whole sequence will exit 
					case NodeStates.FAILURE:
						//Still want to increase the index to show that all the children have been evaluated 
						++m_index;
						m_nodeState = NodeStates.FAILURE;
						return m_nodeState;
					//If the child succeeds the sequence will continue with a running state 
					case NodeStates.SUCCESS:
						++m_index;
						m_nodeState = NodeStates.RUNNING;
						return m_nodeState;
					//If the child is running it will run this child again 
					case NodeStates.RUNNING:
						m_nodeState = NodeStates.RUNNING;
						m_runningChild = m_children[m_index];
						return m_nodeState;
				}
			}
		}
		//If it is a selector and we get to the end with everything failing 
		else if (m_index > m_children.Count && !m_isSequence)
		{
			m_nodeState = NodeStates.FAILURE;
			m_runningChild = null;
			return m_nodeState;
		}
		else if (m_index > m_children.Count && m_isSequence)
		{
			m_nodeState = NodeStates.SUCCESS;
			m_runningChild = null;
			return m_nodeState;
		}
		return NodeStates.FAILURE;
	}
}



﻿using UnityEngine;
using System.Collections;
using System;

public class Inverter : Node
{
    /* Child node to evaluate */
    private Node m_child;

    public Node node
    {
        get { return m_child; }
    }

    /* The constructor requires the child node that this inverter decorator 
     * wraps*/
    public Inverter(string name) : base(name)
    { 
    }

    public void AddChild(Node child)
    {
        m_child = child;
        m_child.SetParent(this); 
    }

    public override Node GetCurrentChild()
    {
        if (m_child.GetType() == typeof(ActionNode))
            return m_child;
        else
            return m_child.GetCurrentChild(); 
    }

    /* Reports a success if the child fails and 
     * a failure if the child succeeds. Running will report 
     * as running */
    public override NodeState Evaluate()
    {
        switch (m_child.Evaluate())
        {
            case NodeState.FAILURE:
                m_nodeState = NodeState.SUCCESS;
                return m_nodeState;
            case NodeState.SUCCESS:
                m_nodeState = NodeState.FAILURE;
                return m_nodeState;
            case NodeState.RUNNING:
                m_nodeState = NodeState.RUNNING;
                return m_nodeState;
        }
        m_nodeState = NodeState.SUCCESS;
        return m_nodeState;
    }

    public override void PrintGUIElement(BehaviourTreeEditor src)
    {
        throw new NotImplementedException();
    }
}
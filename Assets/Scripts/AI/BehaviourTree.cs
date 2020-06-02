using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

public class BehaviourTree : MonoBehaviour
{
    public ConnorNode m_root;


    // Start is called before the first frame update
    void Start()
    {
        m_root.Evaluate(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (m_root.nodeState != NodeStates.SUCCESS && m_root.GetRunningChild() != null)
            m_root.Evaluate();
    }

    public void CreateRoot(bool isSequence, string name)
    {
        m_root = new ConnorNode(isSequence, name); 
    }

    public ConnorNode GetRoot()
    {
        return m_root; 
    }


}

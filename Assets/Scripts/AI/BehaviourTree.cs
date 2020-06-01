using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

public class BehaviourTree : MonoBehaviour
{
    private ConnorNode m_root;

    private List<Node> m_branches = new List<Node>();
    private List<Node> m_leafs = new List<Node>(); 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateRoot(bool isSequence, string name)
    {
        m_root = new ConnorNode(isSequence, name); 
    }

    public void AddBranch(bool isSequence, string name, Node Parent)
    {
        ConnorNode temp = new ConnorNode(isSequence, name);
        m_branches.Add(temp);
    }



}

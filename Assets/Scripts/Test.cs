using System;
using UnityEngine;

public class Test : MonoBehaviour
{

	private ConnorNode m_root;
	private ActionNode m_walkToDoor;
	private ActionNode m_print;
	
	
	private ConnorNode m_attemptToOpenDoor;
	private ConnorNode m_findKeySequence; 
	private ActionNode m_openDoor;
	private ActionNode m_findkey; 

	public float speed = 10.0f;

	public Transform target;

	public GameObject key;
	public GameObject door;
	private bool m_doorUnlocked; 

	// Start is called before the first frame update
	void Start()
	{
		m_root = new ConnorNode(true, "root node");
		m_walkToDoor = new ActionNode(Walk, "Walking to door");

		m_attemptToOpenDoor = new ConnorNode(false, "Attempt to open the door selector");
		m_findKeySequence = new ConnorNode(true, "Find the key sequence"); 
		m_openDoor = new ActionNode(OpenDoor, "action of opening door");
		m_findkey = new ActionNode(FindKey, "find the key");
		m_print = new ActionNode(Print, "Print if success"); 

		m_root.AddChild(m_walkToDoor);
		m_root.AddChild(m_attemptToOpenDoor);
		m_attemptToOpenDoor.AddChild(m_openDoor);
		m_attemptToOpenDoor.AddChild(m_findKeySequence);
		m_findKeySequence.AddChild(m_findkey);
		m_findKeySequence.AddChild(m_walkToDoor);
		m_attemptToOpenDoor.AddChild(m_openDoor);
		m_root.AddChild(m_print);

		//m_root.CurrentRunningChild();
		m_root.Evaluate(); 

	}

	// Update is called once per frame
	void Update()
	{
		if (m_root.nodeState != NodeStates.SUCCESS && m_root.GetRunningChild() != null)
			m_root.Evaluate();

		//Debug.Log(m_root.GetRunningChild().GetName()); 
	}

	public NodeStates Walk()
	{
		float step = speed * Time.deltaTime;
		if (Vector3.Distance(transform.position, door.transform.position) > 0.001f)
		{
			transform.position = Vector3.MoveTowards(transform.position, door.transform.position, step);
			return NodeStates.RUNNING; 
		}
		else
		{
			return NodeStates.SUCCESS; 
		}
	}

	public NodeStates Print()
	{
		Debug.Log("PLEASE WORK");
		return NodeStates.SUCCESS;
	}

	public NodeStates OpenDoor()
	{
		if (Vector3.Distance(transform.position, door.transform.position) < 0.001f && m_doorUnlocked)
		{
			return NodeStates.SUCCESS;
		}
		else
			return NodeStates.FAILURE; 
	}

	public NodeStates FindKey()
	{
		float step = speed * Time.deltaTime;
		if (Vector3.Distance(transform.position, key.transform.position) > 0.001f)
		{
			transform.position = Vector3.MoveTowards(transform.position, key.transform.position, step);
			return NodeStates.RUNNING;
		}
		else
		{
			m_doorUnlocked = true; 
			return NodeStates.SUCCESS;
		}
	}
}

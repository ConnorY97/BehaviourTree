using System;
using System.Transactions;
using UnityEngine;

public class Test : MonoBehaviour
{

	private ConnorNode m_root;
	private ActionNode m_walkToDoor;
	private ActionNode m_print;
	
	
	private ConnorNode m_attemptToOpenDoorSelector;
	private ConnorNode m_findKeySequence; 
	private ActionNode m_openDoorA1;
	private ActionNode m_openDoorA2;
	private ActionNode m_findkey;

	private Inverter m_inverter; 

	public float speed = 10.0f;

	public Transform target;

	public GameObject key;
	public GameObject door;
	private bool m_doorUnlocked;


	[SerializeField]
	public string currentAction; 

	// Start is called before the first frame update
	void Start()
	{
		m_root = new ConnorNode(true, "root node");
		m_walkToDoor = new ActionNode(Walk, "Walking to door");

		m_attemptToOpenDoorSelector = new ConnorNode(false, "Attempt to open the door selector");
		m_findKeySequence = new ConnorNode(true, "Find the key sequence"); 
		m_openDoorA1 = new ActionNode(OpenDoor, "action of opening door attempt 1");
		m_openDoorA2 = new ActionNode(OpenDoor, "action of opening door attempt 2");
		m_findkey = new ActionNode(FindKey, "find the key");
		m_print = new ActionNode(Print, "Print if success");

		m_inverter = new Inverter("Attempt at an inverter"); 

		m_root.AddChild(m_walkToDoor);
		m_root.AddChild(m_attemptToOpenDoorSelector);
		m_attemptToOpenDoorSelector.AddChild(m_openDoorA1);
		m_attemptToOpenDoorSelector.AddChild(m_inverter);
		//m_attemptToOpenDoor.AddChild(m_findKeySequence);
		m_inverter.AddChild(m_findKeySequence); 
		m_findKeySequence.AddChild(m_findkey);
		m_findKeySequence.AddChild(m_walkToDoor);
		m_attemptToOpenDoorSelector.AddChild(m_openDoorA1);
		m_root.AddChild(m_print);

		//m_root.CurrentRunningChild();
		m_root.Evaluate();
		currentAction = m_root.GetCurrentChild().GetName(); 

	}

	// Update is called once per frame
	void Update()
	{
		if (m_root.nodeState != NodeStates.SUCCESS && m_root.GetRunningChild() != null)
			m_root.Evaluate();

		if (m_root.GetCurrentChild() != null)
			currentAction = m_root.GetCurrentChild().GetName(); 
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
		Debug.Log("IT WORKED");
		return NodeStates.SUCCESS;
	}

	public NodeStates OpenDoor()
	{
		if (Vector3.Distance(transform.position, door.transform.position) < 0.001f && m_doorUnlocked)
		{
			door.gameObject.SetActive(false);
			return NodeStates.SUCCESS;
		}
		else if (Vector3.Distance(transform.position, door.transform.position) > 0.001f)
		{
			return NodeStates.RUNNING;
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

	//public void OnTriggerEnter(Collider other)
	//{
	//	Debug.Log("FUCK"); 
	//}
}

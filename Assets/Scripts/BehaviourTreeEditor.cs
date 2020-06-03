using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;

//[CustomEditor(typeof(Test))]
public class BehaviourTreeEditor : EditorWindow
{
	public GameObject source;

	private BehaviourTree behaviourTree;

	private List<Button> branches = new List<Button>();
	private List<Button> leafs = new List<Button>();

	public Node currentNode;

	
	public string nodeName;


	//public EditorWindow inst;
	public void OnEnable()
	{
		//inst = ScriptableObject.CreateInstance<EditorWindow>();
		//inst.Show();
	}

	[MenuItem("Window/BehaviorTree")]
	public static void ShowWindow()
	{
		GetWindow<BehaviourTreeEditor>("Tree");
	}



	private void OnGUI()
	{
		//this.Close();

		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("AI Actor", EditorStyles.boldLabel);
		source = (GameObject)EditorGUILayout.ObjectField(source, typeof(GameObject), true);
		EditorGUILayout.EndHorizontal();

		if (GUILayout.Button("Create Behaviour Tree"))
		{
			//if (source.GetComponent<BehaviourTree>() == null)
			//{
			//get the behaviour tree we've been building up and actually set it to the source object.
			source.AddComponent<BehaviourTree>();
			behaviourTree = source.GetComponent<BehaviourTree>();
			behaviourTree.CreateRoot(true, "Behaviour Tree Root");
			currentNode = behaviourTree.GetRoot();
			//}
		}

		if (behaviourTree.GetRoot() != null)
		{
			EditorGUILayout.BeginHorizontal();
			GUILayout.Label("Root", EditorStyles.boldLabel);
			GUILayout.Box(behaviourTree.GetRoot().GetName());
			EditorGUILayout.EndHorizontal();
			//---------------------------------------------------
			EditorGUILayout.BeginHorizontal();
			GUILayout.Label("Node name: ");
			nodeName = EditorGUILayout.TextField("", nodeName); 
			EditorGUILayout.EndHorizontal();


			EditorGUILayout.BeginHorizontal();
			if (GUILayout.Button("Create Sequence"))
			{
				ConnorNode temp = (ConnorNode)currentNode;
				//behaviourTree.GetRoot().AddChild(new ConnorNode(true, "Test sequence")); 
				if (temp != null)
				{
					temp.AddChild(new ConnorNode(true, nodeName));
					//inst.Show(); 
				}
			}
			if (GUILayout.Button("Create Selector"))
			{
				ConnorNode temp = (ConnorNode)currentNode;
				if (temp != null)
				{
					temp.AddChild(new ConnorNode(false, nodeName));
					GetWindow<BehaviourTreeEditor>("Name");
				}
			}
			if (GUILayout.Button("Create Action"))
			{
				//behaviourTree.GetRoot().AddChild(new ActionNode(temp, nodeName));//behaviourTree.AddBranch(true, "Move sequence", behaviourTree.GetRoot());
				ConnorNode temp = (ConnorNode)currentNode;
				if (temp != null)
				{
					temp.AddChild(new ActionNode(temporary, nodeName));
				}
			}
			EditorGUILayout.EndHorizontal();
		}

		if (behaviourTree.GetRoot() != null)
		{
			//behaviourTree.GetRoot().
		}

		//if (source != null)
		//{
		//	if (GUILayout.Button())
		//	{
		//		Debug.Log("FUCK");
		//	}
		//	int yPos = 60;
		//	int xPos = 40;
		//	foreach (string thisValue in buttons)
		//	{
		//		GUI.Button(new Rect(xPos, yPos, 80, 30), thisValue);
		//		xPos += 5;
		//		yPos += 30;
		//	}
		//	if (GUILayout.Button("Add button..."))
		//	{
		//		buttons.Add("something");
		//	}

		//}

		behaviourTree.GetRoot().PrintGUIElement(this);



	}

	private void AddNewItem(Node node)
	{
		if (node.GetType() == typeof(ActionNode))
		{
			string code; 
			EditorGUILayout.BeginHorizontal();
			GUILayout.Label("Name:", EditorStyles.boldLabel);
			GUILayout.Box(behaviourTree.GetRoot().GetName());
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.BeginHorizontal();
			GUILayout.Label("Create Action", EditorStyles.boldLabel);
			code = EditorGUILayout.TextField("Write code here", EditorStyles.boldLabel); 
		}

	}

	private NodeStates temporary()
	{
		return NodeStates.FAILURE; 
	}

}


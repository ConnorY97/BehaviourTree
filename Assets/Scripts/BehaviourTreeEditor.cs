using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;
using TMPro;
using System.Reflection;

//[CustomEditor(typeof(Test))]
public class BehaviourTreeEditor : EditorWindow
{
	public GameObject source = null;
	
	private BehaviourTree behaviourTreeSrc = null;

	private Functions funcstionsSrc;// = new Functions();
	MethodInfo[] functionListSrc;  

	public Node currentNode;

	//private string[] functionList = new string[] { "Empty", "empty1", "empyt2" };
	int index = 0;
	private List<string> functionList = new List<string>(); 
	
	public string nodeName;
	public void OnEnable()
	{
	}

	[MenuItem("Window/BehaviorTree")]
	public static void ShowWindow()
	{
		GetWindow<BehaviourTreeEditor>("Tree");
	}

	private void OnGUI()
	{
		funcstionsSrc = source.GetComponent<Functions>();
		Type t = funcstionsSrc.GetType();
		functionListSrc = t.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
		for (int i = 0; i < functionListSrc.Length; i++)
		{
			//functionList[i] = functionListSrc[i].Name;
			functionList.Add(functionListSrc[i].Name);
		}

		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("AI Actor", EditorStyles.boldLabel);
		source = (GameObject)EditorGUILayout.ObjectField(source, typeof(GameObject), true);
		EditorGUILayout.EndHorizontal();
		
		
		//Stops things from drawing if there is no gameobject assgined 
		if (source != null)
		{
			if (GUILayout.Button("Create Behaviour Tree"))
			{
				if (source.GetComponent<BehaviourTree>() == null)
				{
					source.AddComponent<BehaviourTree>();
					behaviourTreeSrc = source.GetComponent<BehaviourTree>();

					behaviourTreeSrc.CreateRoot(true, "Behaviour Tree Root");
					currentNode = behaviourTreeSrc.GetRoot();
				}
			}
			if (GUILayout.Button("Find"))
			{
				behaviourTreeSrc = null;
				//behaviourTree.SetRoot(behaviourTree.GetRoot()); 
				behaviourTreeSrc = source.GetComponent<BehaviourTree>();
				behaviourTreeSrc.SetRoot(source.GetComponent<BehaviourTree>().GetRoot());
			}
			if (GUILayout.Button("Clear"))
			{
				DestroyImmediate(source.GetComponent<BehaviourTree>());
				behaviourTreeSrc = null;
			}

			if (behaviourTreeSrc != null)
			{
				EditorGUILayout.BeginHorizontal();
				GUILayout.Label("Root", EditorStyles.boldLabel);
				GUILayout.Box(behaviourTreeSrc.GetRoot().GetName());
				EditorGUILayout.EndHorizontal();
				//---------------------------------------------------
				EditorGUILayout.BeginHorizontal();
				GUILayout.Label("Node name:", EditorStyles.boldLabel);
				//nodeName = EditorGUILayout.TextField("", nodeName);
				nodeName = EditorGUILayout.TextField(nodeName); 
				EditorGUILayout.EndHorizontal();
				EditorGUILayout.BeginHorizontal();
				GUILayout.Label("Functions", EditorStyles.boldLabel);
				//EditorGUILayout.DropdownButton(funclist, FocusType.Passive);
				index = EditorGUILayout.Popup(index, functionList.ToArray()); 
				EditorGUILayout.EndHorizontal();


				//if (currentNode.GetType() != typeof(ActionNode))
				//{
				EditorGUILayout.BeginHorizontal();
				if (GUILayout.Button("Create Sequence"))
				{
					ConnorNode temp = (ConnorNode)currentNode;
					if (temp != null)
					{
						temp.AddChild(new ConnorNode(true, nodeName));
					}
				}
				if (GUILayout.Button("Create Selector"))
				{
					ConnorNode temp = (ConnorNode)currentNode;
					if (temp != null)
					{
						temp.AddChild(new ConnorNode(false, nodeName));
						//GetWindow<BehaviourTreeEditor>("Name");
					}
				}
				if (GUILayout.Button("Create Action"))
				{
					ConnorNode temp = (ConnorNode)currentNode;
					if (temp != null)
					{
						temp.AddChild(new ActionNode((ActionNode.ActionNodeDelegate)Delegate.CreateDelegate(typeof(ActionNode.ActionNodeDelegate), source.GetComponent<Functions>(), functionListSrc[index]), nodeName));
					}
				}
				EditorGUILayout.EndHorizontal();
			}
		}

		if (behaviourTreeSrc != null)
		{
			if (behaviourTreeSrc.GetRoot() != null)
			{
				behaviourTreeSrc.GetRoot().PrintGUIElement(this);
			}
		}
	}

	private void AddNewItem(Node node)
	{
		if (node.GetType() == typeof(ActionNode))
		{
			string code; 
			EditorGUILayout.BeginHorizontal();
			GUILayout.Label("Name:", EditorStyles.boldLabel);
			GUILayout.Box(behaviourTreeSrc.GetRoot().GetName());
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.BeginHorizontal();
			GUILayout.Label("Create Action", EditorStyles.boldLabel);
			code = EditorGUILayout.TextField("Write code here", EditorStyles.boldLabel); 
		}

	}

	private NodeState temporary()
	{
		return NodeState.FAILURE; 
	}

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; 

[CustomEditor(typeof(Test))]
public class BehaviourTreeEditor : EditorWindow
{
	public GameObject source;

	//public SerializedProperty src;

	//private GUILayoutOption test;

	//private List<string> buttons;

	public ConnorNode src; 



	[MenuItem("Window/BehaviourTree")]
	public static void ShowWindow()
	{
		GetWindow<BehaviourTreeEditor>("Editor"); 
	}

	private void OnGUI()
	{
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("AI Actor", EditorStyles.boldLabel);
		source = (GameObject)EditorGUILayout.ObjectField(source, typeof(GameObject), true);
		EditorGUILayout.EndHorizontal();

		if (GUILayout.Button("Apply"))
		{
			//get the behaviour tree we've been building up and actually set it to the source object.
			source.GetComponent<Transform>().SetPositionAndRotation(new Vector3(10, 10, 10), new Quaternion());

			//srouce.add(connor)
			
		}

		//if (source != null)
		//{
		//	if (GUILayout.Button("Create Behaviour Tree"))
		//	{
		//		Debug.Log("FUCK");
		//	}
		//	int yPos = 60;
		//	int xPos = 40;
		//	foreach(string thisValue in buttons)
		//	{
		//		GUI.Button(new Rect(xPos, yPos, 80, 30), thisValue);
		//		xPos += 5;
		//		yPos += 30;
		//	}
		//	if(GUILayout.Button("Add button..."))
		//	{
		//		buttons.Add("something");
		//	}
			
		//}

	}

}


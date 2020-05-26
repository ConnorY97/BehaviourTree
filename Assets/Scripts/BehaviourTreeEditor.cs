using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; 

public class BehaviourTreeEditor : EditorWindow
{
	public Object source; 
	

	[MenuItem("Window/BehaviourTree")]
	public static void ShowWindow()
	{
		GetWindow<BehaviourTreeEditor>("Editor"); 
	}

	private void OnGUI()
	{
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("AI Actor", EditorStyles.boldLabel);
		source = EditorGUILayout.ObjectField(source, typeof(GameObject), true);
		EditorGUILayout.EndHorizontal();

		if (source != null)
		{
			if (GUILayout.Button("Create Behaviour Tree"))
			{
				Debug.Log("FUCK");
			}
		}

	}
}


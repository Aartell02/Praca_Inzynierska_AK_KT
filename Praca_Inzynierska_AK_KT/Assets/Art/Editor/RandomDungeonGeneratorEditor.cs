using DOTS;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AbstractDungeonSimulator), true)]

public class RandomDungeonGeneratorEditor : Editor
{
    AbstractDungeonSimulator simulator;

	private void Awake()
	{
		simulator = (AbstractDungeonSimulator)target;
	}

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		if(GUILayout.Button("Create Map"))
		{
			simulator.GenerateDungeon();
		}
	}
}

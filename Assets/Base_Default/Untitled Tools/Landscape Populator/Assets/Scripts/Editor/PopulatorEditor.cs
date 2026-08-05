using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace UntitledTools {
	namespace LandscapePopulator {

		//The editor script for the landscape populator
		[CustomEditor(typeof(LandscapePopulatorMain))]
		public class PopulatorEditor : Editor {

			//Runs every Inspector GUI update
			public override void OnInspectorGUI () {

				//Finds the target script and creates default inspector
				DrawDefaultInspector ();
				LandscapePopulatorMain TargetScript = (LandscapePopulatorMain)target;

				GUILayout.Space (10f);

				EditorGUILayout.BeginHorizontal (EditorStyles.textArea);

				//Adds some essential buttons
				if (GUILayout.Button ("Generate Objects", GUILayout.Height (25f))) {
					TargetScript.GenerateObjects ();
				}

				if (GUILayout.Button ("Wipe All Objects", GUILayout.Height (25f))) {
					TargetScript.WipeAllObjects ();
				}

				EditorGUILayout.EndHorizontal ();

			}

		}

	}
}

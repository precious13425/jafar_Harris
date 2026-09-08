
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameEvent))]
public class GameEvent_Editor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUI.enabled=Application.isPlaying;
        GameEvent gameEvent=(GameEvent)target;
        if(GUILayout.Button("Raise Event"))
        {
            gameEvent.OnRaise();
        }
    }
}


[CustomEditor(typeof(Strut_GameEvent))]
public class GameEvent_FloatV3_Editor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUI.enabled=Application.isPlaying;
        Strut_GameEvent gameEvent=(Strut_GameEvent)target;
        if(GUILayout.Button("Raise Default Event"))
        {
            gameEvent.OnRaise();
        }
    }
}

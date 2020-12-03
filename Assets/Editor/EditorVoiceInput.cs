using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AudioInputManager))]
public class EditorVoiceInput : Editor
{
    private string debugInputString = "";

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        AudioInputManager aim = (AudioInputManager)target;


        debugInputString = GUILayout.TextArea(debugInputString, 200);
        if (GUILayout.Button("Input Text"))
        {
            if (!string.IsNullOrEmpty(debugInputString) && aim != null)
            {

                aim.InvokeOnSPeechResult(debugInputString);
            }
            else
            {
                Debug.Log("The InputString must not be empy or null");
            }

        }

    }


}
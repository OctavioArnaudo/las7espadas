using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(TransformPointings))]
public class TransformTagging : Editor
{
    private List<string> allTags;
    private List<bool> tagSelection;

    private void OnEnable()
    {
        allTags = new List<string>(UnityEditorInternal.InternalEditorUtility.tags);

        TransformPointings myTarget = (TransformPointings)target;
        tagSelection = new List<bool>();
        foreach (string tag in allTags)
        {
            tagSelection.Add(myTarget.selectedTags.Contains(tag));
        }
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TransformPointings myTarget = (TransformPointings)target;

        EditorGUILayout.LabelField("Select Tags to Register", EditorStyles.boldLabel);

        for (int i = 0; i < allTags.Count; i++)
        {
            bool newSelection = EditorGUILayout.Toggle(allTags[i], tagSelection[i]);
            if (newSelection != tagSelection[i])
            {
                tagSelection[i] = newSelection;
                if (newSelection)
                {
                    myTarget.selectedTags.Add(allTags[i]);
                }
                else
                {
                    myTarget.selectedTags.Remove(allTags[i]);
                }
                EditorUtility.SetDirty(myTarget);
            }
        }
    }
}
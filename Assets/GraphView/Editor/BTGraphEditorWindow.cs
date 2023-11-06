using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

public class BTGraphEditorWindow : EditorWindow
{
    [MenuItem("Window/BTGraphEditorWindow")]
    public static void Open()
    {
        GetWindow<BTGraphEditorWindow>(ObjectNames.NicifyVariableName(nameof(BTGraphEditorWindow)));
    }

    private void OnEnable()
    {
        var graphViewEditor = new BTGraphEditor(this);
        rootVisualElement.Add(graphViewEditor);

        var horizontal = new VisualElement();
        var fd = horizontal.style.flexDirection;
        fd.value = FlexDirection.Row;
        horizontal.style.flexDirection = fd;

        horizontal.Add(new Button(graphViewEditor.Load) { text = "Load" });
        horizontal.Add(new Button(graphViewEditor.Save) { text = "Save" });

        rootVisualElement.Add(horizontal);
    }
}

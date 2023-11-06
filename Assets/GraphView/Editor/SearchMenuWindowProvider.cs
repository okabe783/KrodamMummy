using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using BT;

public class SearchMenuWindowProvider : ScriptableObject, ISearchWindowProvider
{
    private BTGraphEditor graphView;
    private EditorWindow editorWindow;

    public void Initialize(BTGraphEditor graphView, EditorWindow editorWindow)
    {
        this.graphView = graphView;
        this.editorWindow = editorWindow;
    }

    List<SearchTreeEntry> ISearchWindowProvider.CreateSearchTree(SearchWindowContext context)
    {
        var entries = new List<SearchTreeEntry>();
        entries.Add(new SearchTreeGroupEntry(new GUIContent("Create Node")));
        entries.Add(new SearchTreeGroupEntry(new GUIContent("BT")) { level = 1 });

        entries.Add(new SearchTreeEntry(new GUIContent(nameof(BTStartNode))) { level = 2, userData = typeof(BTStartNode) });
        entries.Add(new SearchTreeEntry(new GUIContent(nameof(BTSelectorNode))) { level = 2, userData = typeof(BTSelectorNode) });
        entries.Add(new SearchTreeEntry(new GUIContent(nameof(BTSequenceNode))) { level = 2, userData = typeof(BTSequenceNode) });

        entries.Add(new SearchTreeGroupEntry(new GUIContent("Action")) { level = 2 });
        entries.Add(new SearchTreeEntry(new GUIContent(nameof(BTMoveNode))) { level = 3, userData = typeof(BTMoveNode) });
        entries.Add(new SearchTreeEntry(new GUIContent(nameof(BTWaitNode))) { level = 3, userData = typeof(BTWaitNode) });
        entries.Add(new SearchTreeEntry(new GUIContent(nameof(BTAttackNode))) { level = 3, userData = typeof(BTAttackNode) });

        entries.Add(new SearchTreeGroupEntry(new GUIContent("Decorator")) { level = 2 });
        entries.Add(new SearchTreeEntry(new GUIContent(nameof(BTRandomNode))) { level = 3, userData = typeof(BTRandomNode) });
        entries.Add(new SearchTreeEntry(new GUIContent(nameof(BTCheckDistanceNode))) { level = 3, userData = typeof(BTCheckDistanceNode) });

        entries.Add(new SearchTreeGroupEntry(new GUIContent("DataSet")) { level = 2 });
        entries.Add(new SearchTreeEntry(new GUIContent(nameof(BTDataSetStringNode))) { level = 3, userData = typeof(BTDataSetStringNode) });
        return entries;
    }

    bool ISearchWindowProvider.OnSelectEntry(SearchTreeEntry searchTreeEntry, SearchWindowContext context)
    {
        var type = searchTreeEntry.userData as Type;
        var node = Activator.CreateInstance(type) as Node;

        var worldMousePos = editorWindow.rootVisualElement.ChangeCoordinatesTo(editorWindow.rootVisualElement.parent, context.screenMousePosition - editorWindow.position.position);
        var localMousePos = graphView.contentViewContainer.WorldToLocal(worldMousePos);
        node.SetPosition(new Rect(localMousePos, new Vector2(100, 100)));

        graphView.AddElement(node);
        return true;
    }
}

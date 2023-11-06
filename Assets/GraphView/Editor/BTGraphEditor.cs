using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.Experimental.GraphView;

public class BTGraphEditor : GraphView
{
    public static readonly Orientation Orientation = Orientation.Horizontal;
    private string filename = "";
    private const string BT_PATH = "BTGraph";

    public BTGraphEditor(EditorWindow editorWindow)
    {
        this.StretchToParentSize();

        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        var menuWindowProvider = ScriptableObject.CreateInstance<SearchMenuWindowProvider>();
        menuWindowProvider.Initialize(this, editorWindow);
        nodeCreationRequest += context =>
        {
            SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), menuWindowProvider);

        };
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        var compatibbleParts = new List<Port>();
        compatibbleParts.AddRange(ports.ToList().Where(port =>
        {
            if (startPort.node == port.node)
            {
                return false;
            }
            if (port.direction == startPort.direction)
            {
                return false;
            }
            if (port.portType != startPort.portType)
            {
                return false;
            }
            return true;
        }));

        return compatibbleParts;
    }

    public void Load()
    {
        var bt_path = string.Format("{0}/Resources/{1}", Application.dataPath, BT_PATH);
        var path = EditorUtility.OpenFilePanel("Select BTGraph", bt_path, "asset");
        if (string.IsNullOrEmpty(path))
        {
            return;
        }
        path = path.Replace(string.Format("{0}/Resources/", Application.dataPath), "");
        filename = System.IO.Path.GetFileNameWithoutExtension(path);
        path = string.Format("{0}/{1}", BT_PATH, filename);
        BT.BTGraphSaveUtility.Load(path, this);
    }

    public void Save()
    {
        var bt_path = string.Format("{0}/Resources/{1}", Application.dataPath, BT_PATH);
        var defaultFilename = string.IsNullOrWhiteSpace(bt_path) ? "bt_graph" : filename;
        var path = EditorUtility.SaveFilePanel("Save BTGraph", bt_path, defaultFilename, "asset");
        if (string.IsNullOrEmpty(path))
        {
            return;
        }

        filename = System.IO.Path.GetFileNameWithoutExtension(path);
        path = string.Format("{0}/{1}", BT_PATH, filename);
        BT.BTGraphSaveUtility.Save(path, this);
    }
}

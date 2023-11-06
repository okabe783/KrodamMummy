using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.Experimental.GraphView;

namespace BT
{
    public abstract class BTDataSetNode : BTNode
    {
        public BTDataSetNode() : base()
        {
            title = "DataSet";

            var inputPort = Port.Create<Edge>(BTGraphEditor.Orientation, Direction.Input, Port.Capacity.Multi, typeof(float));
            inputPort.portName = "In";
            inputContainer.Add(inputPort);

            var outputPort = Port.Create<Edge>(BTGraphEditor.Orientation, Direction.Output, Port.Capacity.Single, typeof(float));
            outputPort.portName = "Out";
            outputContainer.Add(outputPort);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.Experimental.GraphView;

namespace BT
{
    public class BTSequenceNode : BTNode
    {
        public BTSequenceNode() : base()
        {
            NodeType = BTNodeType.Sequence;
            title = "Sequence";

            var inputPort = Port.Create<Edge>(BTGraphEditor.Orientation, Direction.Input, Port.Capacity.Multi, typeof(float));
            inputPort.portName = "In";
            inputContainer.Add(inputPort);

            var ouputPort = Port.Create<Edge>(BTGraphEditor.Orientation, Direction.Output, Port.Capacity.Multi, typeof(float));
            ouputPort.portName = "Out";
            outputContainer.Add(ouputPort);
        }
    }
}

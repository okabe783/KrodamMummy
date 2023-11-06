using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.Experimental.GraphView;

namespace BT
{
    public abstract class BTActionNode : BTNode
    {
        public BTActionNode() : base()
        {
            title = "Action";

            var inputPort = Port.Create<Edge>(BTGraphEditor.Orientation, Direction.Input, Port.Capacity.Multi, typeof(float));
            inputPort.portName = "In";
            inputContainer.Add(inputPort);
        }
    }
}

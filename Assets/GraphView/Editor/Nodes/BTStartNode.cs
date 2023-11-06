using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.Experimental.GraphView;

namespace BT
{
    public class BTStartNode : BTNode
    {
        public BTStartNode() : base()
        {
            NodeType = BTNodeType.Start;
            title = "Start";

            var ouputPort = Port.Create<Edge>(BTGraphEditor.Orientation, Direction.Output, Port.Capacity.Single, typeof(float));
            ouputPort.portName = "Out";
            outputContainer.Add(ouputPort);
        }
    }
}

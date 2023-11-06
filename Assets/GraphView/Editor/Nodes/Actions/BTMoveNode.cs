using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;

namespace BT
{
    public class BTMoveNode : BTActionNode
    {
        private TextField targetField;
        public BTMove data;

        public override string ToJson()
        {
            return data.ToJson();
        }

        public override void FromJson(string json)
        {
            data.FromJson(json);
            targetField.value = data.targetName;
            priorityField.value = Priority;
        }

        public BTMoveNode() : base()
        {
            data = new BTMove();

            NodeType = BTNodeType.Move;
            title = "BTMove";
            targetField = new TextField();
            targetField.RegisterValueChangedCallback(v => data.targetName = v.newValue);
            mainContainer.Add(targetField);
        }
    }
}

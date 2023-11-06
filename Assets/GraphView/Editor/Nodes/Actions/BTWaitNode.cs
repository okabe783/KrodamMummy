using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEditor.Experimental.GraphView;

namespace BT
{
    public class BTWaitNode : BTActionNode
    {
        private FloatField waitField;
        public BTWait data;

        public float WaitFloat {
            get { return data.waitTime; }
            set { data.waitTime = value; }
        }

        public override string ToJson()
        {
            return data.ToJson();
        }

        public override void FromJson(string json)
        {
            data.FromJson(json);
            waitField.value = data.waitTime;
            priorityField.value = Priority;
        }

        public BTWaitNode() : base()
        {
            data = new BTWait();

            NodeType = BTNodeType.Wait;
            title = "BTWait";
            waitField = new FloatField();
            waitField.RegisterValueChangedCallback(v => data.waitTime = v.newValue);
            mainContainer.Add(waitField);
        }
    }
}

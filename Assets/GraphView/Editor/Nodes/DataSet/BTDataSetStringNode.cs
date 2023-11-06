using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEditor.Experimental.GraphView;

namespace BT
{
    public class BTDataSetStringNode : BTDataSetNode
    {
        private TextField keyField;
        private TextField valueField;

        public BTDataSetString data;

        public BTDataSetStringNode() : base()
        {
            data = new BTDataSetString();

            NodeType = BTNodeType.DataSetString;
            title = "BTDataSet";

            keyField = new TextField();
            keyField.RegisterValueChangedCallback(v => data.key = v.newValue);
            mainContainer.Add(keyField);

            valueField = new TextField();
            valueField.RegisterValueChangedCallback(v => data.value = v.newValue);
            mainContainer.Add(valueField);
        }

        public override string ToJson()
        {
            return data.ToJson();
        }

        public override void FromJson(string json)
        {
            data.FromJson(json);
            keyField.value = data.key;
            valueField.value = data.value;
        }
    }
}

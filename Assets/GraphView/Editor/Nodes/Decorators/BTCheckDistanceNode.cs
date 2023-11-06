using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEditor.Experimental.GraphView;

namespace BT
{
    public class BTCheckDistanceNode : BTDecoratorNode
    {
        private EnumField checkTypeField;
        private TextField targetNameField;
        private FloatField distanceField;

        public BTCheckDistance data;

        public BTCheckDistanceNode() : base()
        {
            data = new BTCheckDistance();

            NodeType = BTNodeType.CheckDistance;
            title = "BTCheckDistance";

            checkTypeField = new EnumField();
            checkTypeField.Init(data.checkType);
            checkTypeField.RegisterValueChangedCallback(v => data.checkType = (CheckType)v.newValue);
            mainContainer.Add(checkTypeField);

            targetNameField = new TextField();
            targetNameField.RegisterValueChangedCallback(v => data.targetName = v.newValue);
            mainContainer.Add(targetNameField);

            distanceField = new FloatField();
            distanceField.RegisterValueChangedCallback(v => data.distance = v.newValue);
            mainContainer.Add(distanceField);
        }

        public override string ToJson()
        {
            return data.ToJson();
        }

        public override void FromJson(string json)
        {
            data.FromJson(json);
            checkTypeField.value = data.checkType;
            targetNameField.value = data.targetName;
            distanceField.value = data.distance;
            priorityField.value = Priority;
        }
    }
}

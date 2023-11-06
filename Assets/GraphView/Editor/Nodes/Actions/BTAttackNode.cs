using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;

namespace BT
{
    public class BTAttackNode : BTActionNode
    {
        private TextField targetField;
        private FloatField coolTimeField;
        public BTAttack data;

        public override string ToJson()
        {
            return data.ToJson();
        }

        public override void FromJson(string json)
        {
            data.FromJson(json);
            targetField.value = data.targetName;
            coolTimeField.value = data.coolTime;
            priorityField.value = Priority;
        }

        public BTAttackNode() : base()
        {
            data = new BTAttack();

            NodeType = BTNodeType.Attack;
            title = "BTAttack";
            targetField = new TextField();
            targetField.RegisterValueChangedCallback(v => data.targetName = v.newValue);
            mainContainer.Add(targetField);
            coolTimeField = new FloatField();
            coolTimeField.RegisterValueChangedCallback(v => data.coolTime = v.newValue);
            mainContainer.Add(coolTimeField);
        }
    }
}

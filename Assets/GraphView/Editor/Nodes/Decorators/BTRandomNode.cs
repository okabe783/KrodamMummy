using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class BTRandomNode : BTDecoratorNode
    {
        public BTRandom data;

        public BTRandomNode() : base()
        {
            data = new BTRandom();
            NodeType = BTNodeType.Random;
            title = "BTRandom";
        }

        public override string ToJson()
        {
            return data.ToJson();
        }

        public override void FromJson(string json)
        {
            data.FromJson(json);
            priorityField.value = Priority;
        }
    }
}

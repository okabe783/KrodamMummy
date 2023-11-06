using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class BTGraphDataContainer : ScriptableObject
    {
        public List<BTNodeData> Nodes = new List<BTNodeData>();
        public List<BTEdgeData> Edges = new List<BTEdgeData>();
    }
}

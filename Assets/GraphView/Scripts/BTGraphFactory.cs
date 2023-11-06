using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BT
{
    public static class BTGraphFactory
    {
        public static BTGraph Load(string filename)
        {
            var graphData = Resources.Load<BTGraphDataContainer>(filename);
            if (graphData == null)
            {
                return null;
            }

            var graph = new BTGraph();
            CreateNodes(graph, graphData);

            return graph;
        }

        private static BTBase CreateNode(BTNodeType nodeType)
        {
            BTBase node = null;
            switch (nodeType)
            {
                case BTNodeType.Start:
                    node = new BTStart();
                    break;
                case BTNodeType.Move:
                    node = new BTMove();
                    break;
                case BTNodeType.Wait:
                    node = new BTWait();
                    break;
                case BTNodeType.Attack:
                    node = new BTAttack();
                    break;
                case BTNodeType.Random:
                    node = new BTRandom();
                    break;
                case BTNodeType.CheckDistance:
                    node = new BTCheckDistance();
                    break;
                case BTNodeType.Selector:
                    node = new BTSelector();
                    break;
                case BTNodeType.Sequence:
                    node = new BTSequence();
                    break;
                case BTNodeType.DataSetString:
                    node = new BTDataSetString();
                    break;
            }
            return node;
        }

        private static BTBase CreateBTNode(BTNodeData data)
        {
            var n = CreateNode(data.NodeType);
            n.Data = data;
            n.FromJson(data.parameterJson);
            return n;
        }

        private static void CreateNodes(BTGraph graph, BTGraphDataContainer graphData)
        {
            var list = new List<BTBase>();
            foreach (var nodeData in graphData.Nodes)
            {
                list.Add(CreateBTNode(nodeData));
            }
            CalculateConnection(list, graphData);
            graph.Init(list);
        }

        private static void CalculateConnection(List<BTBase> nodes, BTGraphDataContainer graphData)
        {
            foreach (var edgeData in graphData.Edges)
            {
                var fromNode = nodes.First(x => x.Data.Guid == edgeData.fromNodeGuid);
                var toNode = nodes.First(x => x.Data.Guid == edgeData.toNodeGuid);
                if (fromNode == null || toNode == null)
                {
                    continue;
                }

                fromNode.ConnectionNodeList.Add(toNode);
            }
        }
    }
}

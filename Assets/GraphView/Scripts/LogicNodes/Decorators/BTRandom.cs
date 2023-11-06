using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class BTRandom : BTDecorator
    {
        public override BTStatus Exec(BTData data, bool traverseRunning)
        {
            Status = BTStatus.Failure;
            if (ConnectionNodeList == null || ConnectionNodeList.Count <= 0)
            {
                return Status;
            }
            if (traverseRunning)
            {
                foreach (var node in ConnectionNodeList)
                {
                    if (node.Status == BTStatus.Running)
                    {
                        Status = node.Exec(data, traverseRunning);
                    }
                }
                return Status;
            }

            int outputCount = ConnectionNodeList.Count;
            int decide = UnityEngine.Random.Range(0, outputCount);
            Debug.Log("Random : " + decide);
            Status = ConnectionNodeList[decide].Exec(data, traverseRunning);
            return Status;
        }

        public override string ToJson()
        {
            var list = new BTParameterList();
            return JsonUtility.ToJson(list);
        }

        public override void FromJson(string json)
        {
            var list = JsonUtility.FromJson<BTParameterList>(json);
            if (list != null)
            {
            }
        }
    }
}

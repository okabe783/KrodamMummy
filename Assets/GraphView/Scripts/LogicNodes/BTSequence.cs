using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class BTSequence : BTBase
    {
        private int runningIndex = 0;
        public override BTStatus Exec(BTData data, bool traverseRunning)
        {
            ConnectionNodeList.Sort((a, b) => b.Data.Priority - a.Data.Priority);
            Status = BTStatus.Success;
            int startIndex = traverseRunning ? runningIndex : 0;
            for (int i = startIndex; i < ConnectionNodeList.Count; i++)
            {
                runningIndex = i;
                var node = ConnectionNodeList[i];
                Status = node.Exec(data, traverseRunning);

                if (Status != BTStatus.Success)
                {
                    return Status;
                }
            }
            return Status;
        }
    }
}

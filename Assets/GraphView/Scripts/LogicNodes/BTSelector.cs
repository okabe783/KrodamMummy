using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class BTSelector : BTBase
    {
        public override BTStatus Exec(BTData data, bool traverseRunning)
        {
            ConnectionNodeList.Sort((a, b) => b.Data.Priority - a.Data.Priority);
            Status = BTStatus.Failure;
            foreach (var node in ConnectionNodeList)
            {
                Status = node.Exec(data, traverseRunning);

                if (Status == BTStatus.Success || Status == BTStatus.Running)
                {
                    return Status;
                }
            }
            return Status;
        }
    }

}
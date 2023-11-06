using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class BTStart : BTBase
    {
        public override BTStatus Exec(BTData data, bool traverseRunning)
        {
            Status = BTStatus.Failure;
            if (ConnectionNodeList.Count == 1)
            {
                Debug.Log("BTStart");
                Status =  ConnectionNodeList[0].Exec(data, traverseRunning);
            }
            return Status;
        }
    }
}
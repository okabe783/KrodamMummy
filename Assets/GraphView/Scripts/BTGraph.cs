using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class BTGraph
    {
        private BTStart start;
        private List<BTBase> btList;
        private BTStatus status = BTStatus.Ready;

        public void Init(List<BTBase> list)
        {
            btList = list;
            start = GetStart();
        }

        public BTStart GetStart()
        {
            BTStart s = null;
            foreach (var n in btList)
            {
                s = n as BTStart;
                if (s != null)
                {
                    return s;
                }
            }
            return null;
        }

        public void Reset(bool forceReset = false)
        {
            foreach (var n in btList)
            {
                n.Reset(forceReset);
            }
        }

        public void Exec(BTData data)
        {
            if (start != null)
            {
                data.runningAction = null;
                Reset(status != BTStatus.Running);
                status = start.Exec(data, status == BTStatus.Running);
                Debug.Log("Result : " + status);
            }
        }
    }
}

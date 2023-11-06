using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class BTDataSetString : BTDataSet
    {
        public string key = "";
        public string value = "";

        public override BTStatus Exec(BTData data, bool traverseRunning)
        {
            Status = BTStatus.Failure;
            if (ConnectionNodeList == null || ConnectionNodeList.Count <= 0)
            {
                return Status;
            }

            data.SetValue(key, value);
            Status = ConnectionNodeList[0].Exec(data, traverseRunning);
            return Status;
        }

        public override string ToJson()
        {
            var list = new BTParameterList();
            list.ParameterList.Add(new BTParamter() { Name = "Key", Value = key });
            list.ParameterList.Add(new BTParamter() { Name = "Value", Value = value });
            return JsonUtility.ToJson(list);
        }

        public override void FromJson(string json)
        {
            var list = JsonUtility.FromJson<BTParameterList>(json);
            if (list != null)
            {
                key = list.GetValue<string>("Key");
                value = list.GetValue<string>("Value");
            }
        }
    }
}

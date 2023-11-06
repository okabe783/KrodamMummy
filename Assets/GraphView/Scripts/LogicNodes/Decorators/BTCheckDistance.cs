using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public enum CheckTarget : int
    {
        Point,
        Character,
    }
    public enum CheckType : int
    {
        Less,
        LessEqual,
        Greater,
        GreaterEqual,
        Equal,
        NotEqual,
    }

    public class BTCheckDistance : BTDecorator
    {
        public CheckType checkType = CheckType.Less;
        public string targetName;
        public float distance = 0f;

        public override BTStatus Exec(BTData data, bool traverseRunning)
        {
            var prevStatus = Status;
            Status = BTStatus.Failure;
            if (ConnectionNodeList == null || ConnectionNodeList.Count <= 0)
            {
                Debug.LogError("BTCheckDistance : Failure 1");
                return Status;
            }
            var searchName = data.GetValue<string>(targetName);
            var target = data.GetTransform(searchName);
            if (target == null)
            {
                Debug.LogError("BTCheckDistance : Failure 2");
                return Status;
            }

            float dist = Vector3.Distance(target.position, data.self.position);
            // true -> 0, false -> 1
            if (CheckDistance(dist))
            {
                
                Status = ConnectionNodeList[0].Exec(data, traverseRunning);
            }
            else if (traverseRunning && prevStatus == BTStatus.Running)
            {
                Status = BTStatus.Success;
            }
            else if (ConnectionNodeList.Count == 1 || ConnectionNodeList[1] == null)
            {
                Status = BTStatus.Failure;
            }
            else
            {
                Status = ConnectionNodeList[1].Exec(data, traverseRunning);
            }

            //Debug.LogError("BTCheckDistance : " + Status);
            return Status;
        }

        private bool CheckDistance(float dist)
        {
            //Debug.LogError("BTCheckDistance " + checkType + ", " + dist + ", " + distance);
            switch (checkType)
            {
                case CheckType.Equal:
                    return dist == distance;
                case CheckType.NotEqual:
                    return dist != distance;
                case CheckType.Less:
                    return dist < distance;
                case CheckType.LessEqual:
                    return dist <= distance;
                case CheckType.Greater:
                    return dist > distance;
                case CheckType.GreaterEqual:
                    return dist >= distance;
                default:
                    return false;
            }
        }

        public override string ToJson()
        {
            var list = new BTParameterList();
            list.ParameterList.Add(new BTParamter() { Name = "CheckType", Value = ((int)checkType).ToString() });
            list.ParameterList.Add(new BTParamter() { Name = "TargetName", Value = targetName });
            list.ParameterList.Add(new BTParamter() { Name = "Distance", Value = distance.ToString() });
            return JsonUtility.ToJson(list);
        }

        public override void FromJson(string json)
        {
            var list = JsonUtility.FromJson<BTParameterList>(json);
            if (list != null)
            {
                checkType = (CheckType)list.GetValue<int>("CheckType");
                targetName = list.GetValue<string>("TargetName");
                distance = list.GetValue<float>("Distance");
            }
        }
    }
}

using UnityEngine;

namespace BT
{
    public class BTMove : BTAction
    {
        public string targetName;
        public override BTStatus Exec(BTData data, bool traverseRunning)
        {
            data.runningAction = this;
            Status = BTStatus.Running;
            return Status;
        }

        public override void OnUpdate(BTData data)
        {
            var goal = GetGoalPos(data);
            var trans = data.self;
            var dir = goal - trans.position;
            trans.LookAt(trans.position + dir);
            trans.position += 2f * Time.deltaTime * dir.normalized;
        }

        private Vector3 GetGoalPos(BTData data)
        {
            var pointName = data.GetValue<string>(targetName);
            var trans = data.GetTransform(pointName);
            return trans != null ? trans.position : Vector3.zero;
        }

        public override string ToJson()
        {
            var list = new BTParameterList();
            list.ParameterList.Add(new BTParamter() { Name = "Target", Value = targetName });
            return JsonUtility.ToJson(list);
        }

        public override void FromJson(string json)
        {
            var list = JsonUtility.FromJson<BTParameterList>(json);
            if (list != null)
            {
                targetName = list.GetValue<string>("Target");
            }
        }
    }

}
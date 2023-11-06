using UnityEngine;

namespace BT
{
    public class BTAttack : BTAction
    {
        public string targetName;
        public float coolTime;
        private float beforeTime;

        public override BTStatus Exec(BTData data, bool traverseRunning)
        {
            data.runningAction = this;
            float d = Time.realtimeSinceStartup - beforeTime;
            Status = coolTime <= d ? BTStatus.Success : BTStatus.Failure;
            if (Status == BTStatus.Success)
            {
                beforeTime = Time.realtimeSinceStartup;
                var p = GetTargetPos(data);
                var script = data.self.GetComponent<MoveByAI>();
                script?.Attack(p);
            }
            return Status;
        }

        public override void OnUpdate(BTData data)
        {
        }

        private Vector3 GetTargetPos(BTData data)
        {
            var pointName = data.GetValue<string>(targetName);
            var trans = data.GetTransform(pointName);
            return trans != null ? trans.position : Vector3.zero;
        }

        public override string ToJson()
        {
            var list = new BTParameterList();
            list.ParameterList.Add(new BTParamter() { Name = "Target", Value = targetName });
            list.ParameterList.Add(new BTParamter() { Name = "CoolTime", Value = coolTime.ToString() });
            return JsonUtility.ToJson(list);
        }

        public override void FromJson(string json)
        {
            var list = JsonUtility.FromJson<BTParameterList>(json);
            if (list != null)
            {
                targetName = list.GetValue<string>("Target");
                coolTime = list.GetValue<float>("CoolTime");
            }
        }
    }

}
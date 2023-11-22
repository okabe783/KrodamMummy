using UnityEngine;
using UnityEngine.AI;

public class MonoBehaviorTarget : MonoBehaviour
{
    [SerializeField] float _targetRange = 10;
    private NavMeshAgent _agent;
    private BasicEnemyTarget _target;

    private void Start()
    {
        _target = new BasicEnemyTarget();
        _agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        _target.TargetRange = _targetRange;
        if (EnemyActionManager.Instance.IsTarget)
        {
            _agent.SetDestination(TestPlayer.instance.transform.position);
        }
        else
        {
            _agent.SetDestination(this.transform.position);
        }
    }
}
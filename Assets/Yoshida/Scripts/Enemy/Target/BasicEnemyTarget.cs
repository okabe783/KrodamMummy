using UnityEngine;

// このクラスは非MonoBehaviorクラスなので、"MonoBehaviorTarget"というクラスから数値を受け取っている
public class BasicEnemyTarget : IEnemyTarget
{
    private float _targetRange = 10;
    private LayerMask _obstacle;
    private bool _initialize = false;

    // ここで数値をセットしている
    public float TargetRange { get => _targetRange; set => _targetRange = value; }
    public void EnemyTarget()
    {
        Initialize();
        var playerPos = TestPlayer.instance.Player.transform.position;
        var enemyPos = EnemyActionManager.Instance.Enemy.transform.position;
        var dis = Vector3.Distance(enemyPos, playerPos);
        EnemyActionManager.Instance.IsTarget = false;
        if (dis > _targetRange)
        {
            return;
        }
        Ray ray = new Ray(enemyPos, playerPos);
        Debug.DrawLine(enemyPos, playerPos, Color.red);
        if (!Physics.Linecast(enemyPos, playerPos, _obstacle))
        {
            Debug.DrawLine(enemyPos, playerPos, Color.blue);
            EnemyActionManager.Instance.IsTarget = true;
        }
    }
    private void Initialize()
    {
        if (_initialize)
            return;
        _initialize = true;
        _obstacle = 1 << LayerMask.NameToLayer("Obstacle");
        Debug.Log("Basic Target Mode");
        Debug.Log(_obstacle.value);
    }
}
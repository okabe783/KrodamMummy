using UnityEngine;

public class EnemyActionManager : MonoBehaviour
{
    [SerializeField] private bool _isSpecialMode = false;

    private BasicEnemyTarget _basicEnemyTarget;
    private SpecialEnemyTarget _specialEnemyTarget;
    private Enemy _enemyActions;

    private void Start()
    {
        EnemyInitialize();
        _enemyActions.SetTarget(_basicEnemyTarget);
    }
    private void Update()
    {
        if (_isSpecialMode)
            _enemyActions.SetTarget(_specialEnemyTarget);
    }
    /// <summary>
    /// Enemyの行動パターンの初期化処理
    /// </summary>
    private void EnemyInitialize()
    {
        _basicEnemyTarget = new BasicEnemyTarget();
        _specialEnemyTarget = new SpecialEnemyTarget();
        _enemyActions = new Enemy();
    }
}
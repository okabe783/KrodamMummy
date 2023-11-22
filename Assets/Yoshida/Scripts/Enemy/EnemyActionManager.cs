using UnityEngine;

public class EnemyActionManager : MonoBehaviour
{
    public static EnemyActionManager Instance;

    [SerializeField] private bool _isSpecialMode = false;
    [SerializeField] private bool _isTarget = false;
    [SerializeField] private int _enemySpeed = 5;
    [SerializeField] private int _enemyDamage = 100;

    private BasicEnemyHitDamage _basicEnemyHitDamage;
    private BasicEnemyMove _basicEnemyMove;
    private BasicEnemyTarget _basicEnemyTarget;
    private SpecialEnemyTarget _specialEnemyTarget;
    private Enemy _enemyActions;
    private GameObject _enemy;

    public bool IsTarget { get => _isTarget; set => _isTarget = value; }
    public GameObject Enemy => _enemy;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }
    private void Start()
    {
        EnemyInitialize();
    }
    private void Update()
    {
        _enemy = this.gameObject;
        if (_isSpecialMode)
            _enemyActions.SetTarget(_specialEnemyTarget);
        EnemyAction();
    }
    /// <summary>
    /// Enemyの行動パターンの初期化処理
    /// </summary>
    private void EnemyInitialize()
    {
        _basicEnemyHitDamage = new BasicEnemyHitDamage();
        _basicEnemyMove = new BasicEnemyMove();
        _basicEnemyTarget = new BasicEnemyTarget();
        _specialEnemyTarget = new SpecialEnemyTarget();
        _enemyActions = new Enemy();
        _enemyActions.SetHitDamage(_basicEnemyHitDamage);
        _enemyActions.SetMove(_basicEnemyMove);
        _enemyActions.SetTarget(_basicEnemyTarget);
    }
    private void EnemyAction()
    {
        //_enemyActions.EnemyHitDamage(_enemyDamage);
        //_enemyActions.EnemyMove(_enemySpeed);
        _enemyActions.EnemyTarget();

    }
}
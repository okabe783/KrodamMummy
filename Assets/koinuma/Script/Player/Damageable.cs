using UnityEngine;
using UnityEngine.Events;

public partial class Damageable : MonoBehaviour
{
    [SerializeField] int _maxHp;
    [SerializeField, Tooltip("無敵時間")] float _invulnerabiltyTime;
    [SerializeField, Tooltip("死んだとき")] UnityEvent OnDeath;
    [SerializeField, Tooltip("HPが更新されたとき")] UnityEvent OnReceiveDamage;
    [SerializeField, Tooltip("無敵中にHP更新があったとき")] UnityEvent OnHitWhileInvulnerable;
    [SerializeField, Tooltip("無敵時間が切れたとき")] UnityEvent OnBecomeVulnerable;

    /// <summary>無敵時間であるか</summary>
    public bool IsInvulnerable { get; set; }
    /// <summary>現在HP</summary>
    public int CurrentHitPoints { get; private set; }

    /// <summary>現在の無敵時間</summary>
    protected float _currentInvulnerabiltyTime;
    /// <summary>無敵時間タイマー用</summary>
    protected float m_timeSinceLastHit = 0;

    void Start()
    {
        ResetDamage(); // HP初期化
    }

    void Update()
    {
        if (IsInvulnerable) // 無敵時間タイマー
        {
            m_timeSinceLastHit += Time.deltaTime;
            if (m_timeSinceLastHit > _currentInvulnerabiltyTime)
            {
                m_timeSinceLastHit = 0;
                IsInvulnerable = false;
                OnBecomeVulnerable.Invoke();
            }
        }
    }

    public void ResetDamage()
    {
        CurrentHitPoints = _maxHp;
        IsInvulnerable = false;
        m_timeSinceLastHit = 0;
        OnReceiveDamage.Invoke();
    }

    /// <summary>ダメージを受ける回復する</summary>
    /// <param name="damage"></param>
    public void ApplyDamage(int damage)
    {
        if (CurrentHitPoints <= 0) return;

        if (IsInvulnerable)
        {
            OnHitWhileInvulnerable.Invoke();
            return;
        }

        SetInvulnerable(_invulnerabiltyTime); // ダメージを受けると無敵時間となる
        CurrentHitPoints -= damage;

        if (CurrentHitPoints <= 0) OnDeath.Invoke();
        else OnReceiveDamage.Invoke();
    }

    /// <summary>指定時間無敵時間にする</summary>
    /// <param name="sec"></param>
    public void SetInvulnerable(float sec)
    {
        _currentInvulnerabiltyTime = sec;
        IsInvulnerable = true;
    }
}
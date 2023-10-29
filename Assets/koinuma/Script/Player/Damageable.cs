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
    public bool isInvulnerable { get; set; }
    /// <summary>現在HP</summary>
    public int currentHitPoints { get; private set; }

    /// <summary>無敵時間タイマー用</summary>
    protected float m_timeSinceLastHit = 0.0f;

    System.Action schedule;

    void Start()
    {
        ResetDamage();
    }

    void Update()
    {
        if (isInvulnerable)
        {
            m_timeSinceLastHit += Time.deltaTime;
            if (m_timeSinceLastHit > _invulnerabiltyTime)
            {
                m_timeSinceLastHit = 0.0f;
                isInvulnerable = false;
                OnBecomeVulnerable.Invoke();
            }
        }
    }

    public void ResetDamage()
    {
        currentHitPoints = _maxHp;
        isInvulnerable = false;
        m_timeSinceLastHit = 0.0f;
    }

    public void ApplyDamage(int damage)
    {
        if (currentHitPoints <= 0)
        {
            return;
        }

        if (isInvulnerable)
        {
            OnHitWhileInvulnerable.Invoke();
            return;
        }

        isInvulnerable = true;
        currentHitPoints -= damage;

        if (currentHitPoints <= 0) schedule += OnDeath.Invoke;
        else OnReceiveDamage.Invoke();
    }

    void LateUpdate()
    {
        if (schedule != null)
        {
            schedule();
            schedule = null;
        }
    }
}
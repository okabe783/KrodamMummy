using UnityEngine;
using UnityEngine.Events;

public partial class Damageable : MonoBehaviour
{
    [SerializeField] int _maxHp;
    [SerializeField, Tooltip("���G����")] float _invulnerabiltyTime;
    [SerializeField, Tooltip("���񂾂Ƃ�")] UnityEvent OnDeath;
    [SerializeField, Tooltip("HP���X�V���ꂽ�Ƃ�")] UnityEvent OnReceiveDamage;
    [SerializeField, Tooltip("���G����HP�X�V���������Ƃ�")] UnityEvent OnHitWhileInvulnerable;
    [SerializeField, Tooltip("���G���Ԃ��؂ꂽ�Ƃ�")] UnityEvent OnBecomeVulnerable;

    /// <summary>���G���Ԃł��邩</summary>
    public bool isInvulnerable { get; set; }
    /// <summary>����HP</summary>
    public int currentHitPoints { get; private set; }

    /// <summary>���G���ԃ^�C�}�[�p</summary>
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
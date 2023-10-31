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
    public bool IsInvulnerable { get; set; }
    /// <summary>����HP</summary>
    public int CurrentHitPoints { get; private set; }

    /// <summary>���݂̖��G����</summary>
    protected float _currentInvulnerabiltyTime;
    /// <summary>���G���ԃ^�C�}�[�p</summary>
    protected float m_timeSinceLastHit = 0;

    void Start()
    {
        ResetDamage(); // HP������
    }

    void Update()
    {
        if (IsInvulnerable) // ���G���ԃ^�C�}�[
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

    /// <summary>�_���[�W���󂯂�񕜂���</summary>
    /// <param name="damage"></param>
    public void ApplyDamage(int damage)
    {
        if (CurrentHitPoints <= 0) return;

        if (IsInvulnerable)
        {
            OnHitWhileInvulnerable.Invoke();
            return;
        }

        SetInvulnerable(_invulnerabiltyTime); // �_���[�W���󂯂�Ɩ��G���ԂƂȂ�
        CurrentHitPoints -= damage;

        if (CurrentHitPoints <= 0) OnDeath.Invoke();
        else OnReceiveDamage.Invoke();
    }

    /// <summary>�w�莞�Ԗ��G���Ԃɂ���</summary>
    /// <param name="sec"></param>
    public void SetInvulnerable(float sec)
    {
        _currentInvulnerabiltyTime = sec;
        IsInvulnerable = true;
    }
}
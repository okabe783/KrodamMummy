using System.Collections;
using UnityEngine;

public class NinjaAttack : MonoBehaviour
{
    [SerializeField] float _cd = 0.3f;
    bool _isCD = false;
    PlayerManager _playerManager;
    Animator _animator;

    private void Start()
    {
        _playerManager = GetComponent<PlayerManager>();
        _animator = GetComponent<Animator>();
        PlayerInput.Instance.SetInput(InputType.Attack, OnAttack);
    }

    void OnAttack()
    {
        if (_isCD || _playerManager.PlayerState != PlayerState.nomal) return;
        _animator.SetTrigger("AttackTrigger");
        StartCoroutine(CountCD());
    }

    void OnHit()
    {
        var hitObjects = Physics.OverlapSphere(transform.position + Vector3.up + transform.forward * 1.5f, 1.5f);
        foreach (var hitObj in hitObjects)
        {
            if (hitObj.TryGetComponent(out Rigidbody _rb))
            {
                _rb.AddForce((hitObj.transform.position - transform.position).normalized * 5, ForceMode.Impulse);
            }
        }
    }

    IEnumerator CountCD()
    {
        _isCD = true;
        _playerManager.PlayerState = PlayerState.canneling;
        yield return new WaitForSeconds(_cd);
        _isCD = false;
        _playerManager.PlayerState = PlayerState.nomal;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + Vector3.up + transform.forward * 1.5f, 1.5f);
    }
}
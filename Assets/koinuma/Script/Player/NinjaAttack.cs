using System.Collections;
using UnityEditor;
using UnityEngine;

public class NinjaAttack : MonoBehaviour
{
    [SerializeField] float _cd = 1f;
    bool _isCD = false;
    Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        PlayerInput.Instance.SetInput(InputType.Attack, OnAttack);
    }

    void OnAttack()
    {
        if (_isCD) return;
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
        yield return new WaitForSeconds(_cd);
        _isCD = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + Vector3.up + transform.forward * 1.5f, 1.5f);
    }
}
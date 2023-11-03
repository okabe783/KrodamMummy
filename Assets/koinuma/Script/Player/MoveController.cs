using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
[RequireComponent (typeof (Collider))]
public class MoveController : MonoBehaviour
{
    [SerializeField] float _moveSpeed;
    [SerializeField] float _jumpPower;
    /// <summary>�󒆂ł̕����]���̃X�s�[�h</summary>
    [SerializeField] float _turnSpeed = 3;
    PlayerManager _playerManager;
    Rigidbody _rb;
    Animator _animator;
    bool _isGround;
    bool _jumped = false;
    Vector3 _planeNormalVector;

    void Start()
    {
        _playerManager = GetComponent<PlayerManager> ();
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
        // rb��x��z��freez rotation
        _rb.constraints = RigidbodyConstraints.FreezeRotation;
        InGameManager.Instance.OnUpdateAction += Move;
        PlayerInput.Instance.SetInput(InputType.Jump, Jump);
    }

    void Move()
    {
        IsGroundUpdate();
        // �����ړ�����
        Vector2 moveDir = PlayerInput.Instance.MoveVector;
        Vector3 dir = new Vector3(moveDir.x, 0, moveDir.y); // �ړ�����
        dir = Camera.main.transform.TransformDirection(dir); //�J������̃x�N�g���ɒ���
        dir.y = 0;
        dir = dir.normalized; // �P�ʉ����Ă��鐅�������̓��̓x�N�g��
        Vector3 velo = dir * _moveSpeed;
        velo.y = _rb.velocity.y;
        if (!_isGround) // �󒆏���
        {
            // �󒆂ł����������]�����\
            velo = _rb.velocity;
            if (dir.magnitude != 0f)
            {
                // ���x�̑傫����ێ����Ȃ���������������ς���
                Vector2 startHoriVelo = new Vector2(_rb.velocity.x, _rb.velocity.z);
                float horiMag = startHoriVelo.magnitude;
                if (horiMag < 10f)
                {
                    horiMag = 10;
                }
                Vector2 endHoriVelo = new Vector2(dir.x * horiMag, dir.z * horiMag);
                float turnSpeed = _turnSpeed * Time.deltaTime;
                Vector2 airHoriVelo = endHoriVelo * turnSpeed + startHoriVelo * (1 - turnSpeed);
                velo = new Vector3(airHoriVelo.x, _rb.velocity.y, airHoriVelo.y);
            }
            _rb.velocity = velo;
        }
        else // �ڒn������
        {
            // �ڂ��Ă���ʂɉ������x�N�g���ɕς���
            Vector3 onPlaneVelo = Vector3.ProjectOnPlane(velo, _planeNormalVector);
            _rb.velocity = onPlaneVelo; // �ڒn����velocity������������
        }

        // �L�����N�^�[���ړ������Ɍ�����(�ړ������ł͂Ȃ��Ǝ��Ɍv�Z���Ă�)
        if (PlayerInput.Instance.MoveVector.magnitude != 0) transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * _turnSpeed * 5);

        SetAnimation();
    }

    void Jump()
    {
        if (!_isGround) return; // �ڒn���ĂȂ�������return
        _isGround = false;
        Vector3 moveVelo = _rb.velocity;
        moveVelo.y = _jumpPower;
        _rb.velocity = moveVelo;
        _animator.SetTrigger("Jumped");
        StartCoroutine(Jumped());
    }

    /// <summary>�W�����v�����isGround��true�ɂ����Ȃ�����</summary>
    IEnumerator Jumped()
    {
        _jumped = true;
        yield return new WaitForSeconds(0.1f);
        _jumped = false;
    }

    void IsGroundUpdate()
    {
        if (_jumped) return; // �W�����v��������͍X�V���Ȃ�
        if (Physics.CheckSphere(transform.position + Vector3.up * 0.749f, 0.75f, 1 << 3)) _isGround = true;
        else _isGround = false;
    }

    /// <summary>animation paramater���Z�b�g</summary>
    void SetAnimation()
    {
        _animator.SetBool("Moving", PlayerInput.Instance.MoveVector != Vector2.zero);
        _animator.SetFloat("Velocity", _rb.velocity.magnitude / 10);
        _animator.SetFloat("FallSpeed", _rb.velocity.y);
    }

    private void OnCollisionStay(Collision collision)
    {
        float angle = Vector3.Angle(Vector3.up, collision.contacts[0].normal); // �ڂ��Ă���ʂ̖@���x�N�g��
        if (angle < 45)
        {
            _planeNormalVector = collision.contacts[0].normal;
            _animator.SetTrigger("Landing");
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + Vector3.up * 0.74f, 0.75f);
    }
}
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
    Rigidbody _rb;
    bool _isGround;
    /// <summary>�ڒn����Collision</summary>
    List<Collider> _onCollisions = new List<Collider>();
    Vector3 _planeNormalVector;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        // rb��x��z��freez rotation
        _rb.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
    }

    private void Update()
    {
        Move();
    }

    void Move()
    {
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
    }

    void Jump()
    {
        if (!_isGround) return; // �ڒn���ĂȂ�������return
        Vector3 moveVelo = _rb.velocity;
        moveVelo.y = _jumpPower;
        _rb.velocity = moveVelo;
        _isGround = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        float angle = Vector3.Angle(Vector3.up, collision.contacts[0].normal); // �ڂ��Ă���ʂ̖@���x�N�g��
        if (angle < 45)
        {
            _planeNormalVector = collision.contacts[0].normal;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (Vector3.Angle(Vector3.up, collision.contacts[0].normal) < 45)
        {
            _onCollisions.Add(collision.collider);
            _isGround = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (_onCollisions.Contains(collision.collider))
        {
            _onCollisions.Remove(collision.collider);
            if (_onCollisions.Count == 0) _isGround = false;
        }
    }

    private void OnEnable()
    {
        PlayerInput.Instance.SetInput(InputType.Jump, Jump); // �A�N�V�������Z�b�g
    }
}

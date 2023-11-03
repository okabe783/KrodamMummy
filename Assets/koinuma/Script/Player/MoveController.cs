using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
[RequireComponent (typeof (Collider))]
public class MoveController : MonoBehaviour
{
    [SerializeField] float _moveSpeed;
    [SerializeField] float _jumpPower;
    /// <summary>空中での方向転換のスピード</summary>
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
        // rbのxとzをfreez rotation
        _rb.constraints = RigidbodyConstraints.FreezeRotation;
        InGameManager.Instance.OnUpdateAction += Move;
        PlayerInput.Instance.SetInput(InputType.Jump, Jump);
    }

    void Move()
    {
        IsGroundUpdate();
        // 水平移動処理
        Vector2 moveDir = PlayerInput.Instance.MoveVector;
        Vector3 dir = new Vector3(moveDir.x, 0, moveDir.y); // 移動方向
        dir = Camera.main.transform.TransformDirection(dir); //カメラ基準のベクトルに直す
        dir.y = 0;
        dir = dir.normalized; // 単位化してある水平方向の入力ベクトル
        Vector3 velo = dir * _moveSpeed;
        velo.y = _rb.velocity.y;
        if (!_isGround) // 空中処理
        {
            // 空中でゆっくり方向転換が可能
            velo = _rb.velocity;
            if (dir.magnitude != 0f)
            {
                // 速度の大きさを保持しながら向きを少しずつ変える
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
        else // 接地中処理
        {
            // 接している面に沿ったベクトルに変える
            Vector3 onPlaneVelo = Vector3.ProjectOnPlane(velo, _planeNormalVector);
            _rb.velocity = onPlaneVelo; // 接地中はvelocityを書き換える
        }

        // キャラクターを移動方向に向ける(移動方向ではなく独自に計算してる)
        if (PlayerInput.Instance.MoveVector.magnitude != 0) transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * _turnSpeed * 5);

        SetAnimation();
    }

    void Jump()
    {
        if (!_isGround) return; // 接地してなかったらreturn
        _isGround = false;
        Vector3 moveVelo = _rb.velocity;
        moveVelo.y = _jumpPower;
        _rb.velocity = moveVelo;
        _animator.SetTrigger("Jumped");
        StartCoroutine(Jumped());
    }

    /// <summary>ジャンプ直後はisGroundをtrueにさせないため</summary>
    IEnumerator Jumped()
    {
        _jumped = true;
        yield return new WaitForSeconds(0.1f);
        _jumped = false;
    }

    void IsGroundUpdate()
    {
        if (_jumped) return; // ジャンプした直後は更新しない
        if (Physics.CheckSphere(transform.position + Vector3.up * 0.749f, 0.75f, 1 << 3)) _isGround = true;
        else _isGround = false;
    }

    /// <summary>animation paramaterをセット</summary>
    void SetAnimation()
    {
        _animator.SetBool("Moving", PlayerInput.Instance.MoveVector != Vector2.zero);
        _animator.SetFloat("Velocity", _rb.velocity.magnitude / 10);
        _animator.SetFloat("FallSpeed", _rb.velocity.y);
    }

    private void OnCollisionStay(Collision collision)
    {
        float angle = Vector3.Angle(Vector3.up, collision.contacts[0].normal); // 接している面の法線ベクトル
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
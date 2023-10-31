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
    Rigidbody _rb;
    bool _isGround;
    /// <summary>接地中のCollision</summary>
    List<Collider> _onCollisions = new List<Collider>();
    Vector3 _planeNormalVector;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        // rbのxとzをfreez rotation
        _rb.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
    }

    private void Update()
    {
        Move();
    }

    void Move()
    {
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
    }

    void Jump()
    {
        if (!_isGround) return; // 接地してなかったらreturn
        Vector3 moveVelo = _rb.velocity;
        moveVelo.y = _jumpPower;
        _rb.velocity = moveVelo;
        _isGround = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        float angle = Vector3.Angle(Vector3.up, collision.contacts[0].normal); // 接している面の法線ベクトル
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
        PlayerInput.Instance.SetInput(InputType.Jump, Jump); // アクションをセット
    }
}

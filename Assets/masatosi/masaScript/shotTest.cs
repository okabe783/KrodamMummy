using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class shotTest : MonoBehaviour
{
    Vector3 _target = Vector3.zero;
    float _damage;
    Rigidbody _rb;
    [SerializeField] float _speed = 1000;

    public void Date(Vector3 hitPoint,float Damage)
    {
        _target = hitPoint;
        _damage = Damage;
    }
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 3f);
        _rb = this.GetComponent<Rigidbody>();
        Vector3 direction = (_target - this.transform.position).normalized;
        _rb.AddForce(direction * _speed, ForceMode.Force);
        this.transform.forward = direction;
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<DebugEnemy>().Damage(_damage);
        }
    }
}

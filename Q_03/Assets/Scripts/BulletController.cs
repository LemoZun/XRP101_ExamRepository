using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : PooledBehaviour
{
    [SerializeField] private float _force;
    [SerializeField] private float _deactivateTime;
    [SerializeField] private int _damageValue;

    private Rigidbody _rigidbody;
    private WaitForSeconds _wait;
    
    private void Awake()
    {
        Init();
    }

    private void OnEnable()
    {
        StartCoroutine(DeactivateRoutine());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("�÷��̾ �Ѿ˿� �ǰݵ�");
            other.gameObject
                .GetComponentInParent<PlayerController>() // �浹�� body�� �θ��� Player�� ������Ʈ�� �����ϵ��� ����
                .TakeHit(_damageValue);
        }
    }

    private void Init()
    {
        _wait = new WaitForSeconds(_deactivateTime);
        //_rigidbody = GetComponent<Rigidbody>();
        _rigidbody = GetComponentInChildren<Rigidbody>(); //�ڽ��� body�� ������ٵ� �����ϵ��� ����
    }
    
    private void Fire()
    {
        _rigidbody.AddForce(transform.forward * _force, ForceMode.Impulse);
    }

    private IEnumerator DeactivateRoutine()
    {
        yield return _wait;
        _rigidbody.velocity = Vector3.zero; //���� �� �ӵ��� �ʱ�ȭ����
        ReturnPool();
    }

    public override void ReturnPool()
    {
        Pool.Push(this);
        gameObject.SetActive(false);
    }

    //�Ҹ��� OnTaken�� ������ ���
    public override void OnTaken<T>(T t)
    {
        if (!(t is Transform)) return;
        
        transform.LookAt((t as Transform));
        Fire();
    }
}

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
            Debug.Log("플레이어가 총알에 피격됨");
            other.gameObject
                .GetComponentInParent<PlayerController>() // 충돌한 body의 부모인 Player의 컴포넌트를 참조하도록 변경
                .TakeHit(_damageValue);
        }
    }

    private void Init()
    {
        _wait = new WaitForSeconds(_deactivateTime);
        //_rigidbody = GetComponent<Rigidbody>();
        _rigidbody = GetComponentInChildren<Rigidbody>(); //자식인 body의 리지드바디를 참조하도록 변경
    }
    
    private void Fire()
    {
        _rigidbody.AddForce(transform.forward * _force, ForceMode.Impulse);
    }

    private IEnumerator DeactivateRoutine()
    {
        yield return _wait;
        _rigidbody.velocity = Vector3.zero; //리턴 전 속도를 초기화해줌
        ReturnPool();
    }

    public override void ReturnPool()
    {
        Pool.Push(this);
        gameObject.SetActive(false);
    }

    //불릿의 OnTaken이 구현된 장소
    public override void OnTaken<T>(T t)
    {
        if (!(t is Transform)) return;
        
        transform.LookAt((t as Transform));
        Fire();
    }
}

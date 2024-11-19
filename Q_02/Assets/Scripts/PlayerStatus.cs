using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    private float _moveSpeed;

    public float MoveSpeed
    {   // 자기 자신을 계속 참조하고있기 때문에 문제 발생

        get => _moveSpeed; 
        private set => _moveSpeed = value;
    }

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        MoveSpeed = 5f;
    }
}

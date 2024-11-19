using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefab;

    [SerializeField] private CubeController _cubeController;
    [SerializeField] private Vector3 _cubeSetPoint;

    private void Awake()
    {
        CreateCube();
        
    }

    private void Start()
    {
        SetCubePosition(3, 0, 3);
    }

    private void SetCubePosition(float x, float y, float z)
    {
        _cubeSetPoint.x = x;
        _cubeSetPoint.y = y;
        _cubeSetPoint.z = z;
        //큐브 셋 포지션이 (3,0,3)으로 설정됨

        //갑자기 할당되지 않은 큐브컨트롤러의 setPoint를 기존에 할당된 큐브셋포인트에 덮어씌우고 있음 (0,0,0)이 됨
        //_cubeSetPoint = _cubeController.SetPoint;
        // 이렇게 해주려고해도 SetPoint가 set이 private이라 안됨
        // 프라이빗을 해제 후 설정하면 큐브가 제대로 이동 // 프라이빗이 의도된 사항이라면 다른 방법은 없을까?

        _cubeController.SetPoint = _cubeSetPoint;
        _cubeController.SetPosition();
    }

    private void CreateCube()
    {
        GameObject cube = Instantiate(_cubePrefab); //큐브 게임 오브젝트 생성
        _cubeController = cube.GetComponent<CubeController>(); // 큐브 매니저의 큐트컨트롤러에 큐브의 큐브컨트롤러 컴포넌트 할당

        //아직 setPoint가 지정되지 않은 초기 상황에서 setPoint를 해주고있음
        _cubeSetPoint = _cubeController.SetPoint;

    }
}

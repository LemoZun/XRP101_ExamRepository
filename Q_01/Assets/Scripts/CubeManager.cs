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
        //ť�� �� �������� (3,0,3)���� ������

        //���ڱ� �Ҵ���� ���� ť����Ʈ�ѷ��� setPoint�� ������ �Ҵ�� ť�������Ʈ�� ������ ���� (0,0,0)�� ��
        //_cubeSetPoint = _cubeController.SetPoint;
        // �̷��� ���ַ����ص� SetPoint�� set�� private�̶� �ȵ�
        // �����̺��� ���� �� �����ϸ� ť�갡 ����� �̵� // �����̺��� �ǵ��� �����̶�� �ٸ� ����� ������?

        _cubeController.SetPoint = _cubeSetPoint;
        _cubeController.SetPosition();
    }

    private void CreateCube()
    {
        GameObject cube = Instantiate(_cubePrefab); //ť�� ���� ������Ʈ ����
        _cubeController = cube.GetComponent<CubeController>(); // ť�� �Ŵ����� ťƮ��Ʈ�ѷ��� ť���� ť����Ʈ�ѷ� ������Ʈ �Ҵ�

        //���� setPoint�� �������� ���� �ʱ� ��Ȳ���� setPoint�� ���ְ�����
        _cubeSetPoint = _cubeController.SetPoint;

    }
}

# 1번 문제

주어진 프로젝트 내에서 CubeManager 객체는 다음의 책임을 가진다
- 해당 객체 생성 시 Cube프리팹의 인스턴스를 생성한다
- Cube 인스턴스의 컨트롤러를 참조해 위치를 변경한다.

제시된 소스코드에서는 큐브 인스턴스 생성 후 아래의 좌표로 이동시키는 것을 목표로 하였다
- x : 3
- y : 0
- z : 3

제시된 소스코드에서 문제가 발생하는 `원인을 모두 서술`하시오.

## 답안
- 에러 : NullReferenceException: Object reference not set to an instance of an object
- Awake 에서 큐브가 이동할 좌표를 설정
- SetCubePosition 에서 CubeController가 할당되지 않았는데 setPosition을 해주고 있다
- 큐브를 먼저 생성해 큐브 매니저의 _cubeController에 큐브의 _cubeController를 할당해주어야함
- 문제에서 큐브 인스턴스 생성 후 , 좌표이동이 목표이므로 큐브 먼저 생성해줌 (해당 좌표에서 생성이 아님)
- 큐브 생성인 CreateCube에서 마지막 줄 _cubeSetPoint = _cubeController.SetPoint; 를 해주고 있는데, 이 시점은 큐브생성을 먼저 해줬으므로 셋포인트가 지정되지 않은 상황
- 갑자기 할당되지 않은 큐브컨트롤러의 setPoint를 기존에 할당된 큐브셋포인트에 덮어씌우고 있음 (0,0,0)이 됨
- _cubeController.SetPoint = _cubeSetPoint; 이렇게 해주려고해도 SetPoint가 set이 private이라 안됨
- 프라이빗을 해제 후 설정하면 큐브가 제대로 이동

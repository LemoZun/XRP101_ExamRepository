# 2번 문제

주어진 프로젝트 내에서 제공된 스크립트(클래스)는 다음의 책임을 가진다
- PlayerStatus : 플레이어 캐릭터가 가지는 기본 능력치를 보관하고, 객체 생성 시 초기화한다
- PlayerMovement : 유저의 입력을 받고 플레이어 캐릭터를 이동시킨다.

프로젝트에는 현재 2가지 문제가 있다.
- 유니티 에디터에서 Run 실행 시 윈도우에서는 Stack Overflow가 발생하고, MacOS의 유니티에서는 에디터가 강제종료된다.
- 플레이어 캐릭터가 X, Z축의 입력을 동시입력 받아서 대각선으로 이동 시 하나의 축 기준으로 움직일 때 보다 약 1.414배 빠르다.

두 가지 문제가 발생한 원인과 해결 방법을 모두 서술하시오.

## 답안
- 문제 : 스택 오버플로우 + 정규화 되지 않은 대각선 이동
- 에러 코드 : 
```
StackOverflowException: The requested operation caused a stack overflow.
PlayerStatus.set_MoveSpeed (System.Single value) <0x1587392f220 + 0x00008> in <aaa3a6033c2a4fcead1e6669b484de7f>:0
```
- PlayerStatus의 set_MoveSpeed 쪽에서 문제가 생긴것으로 보임
- get => MoveSpeed; // 자기 자신을 계속 참조하고있기 때문에 문제 발생 , set도 마찬가지
```c#
private float _moveSpeed;

public float MoveSpeed
{   
    get => _moveSpeed; 
    private set => _moveSpeed = value;
}

```
- 이렇게 바꿔주면 무한참조로 오버플로우가 발생하는 현상을 해결할 수 있다.
- 플레이어에겐 PlayerStatus 와 PlayerMovement 스크립트 컴포넌트가 MonoBehaviour로 달려있다
- PlayerStatus 의 Awake에서 MoveSpeed를 5f로 설정하는 Init을 수행
- 대각선 방향을 입력시 정규화가 되지 않아 의도한 것 보다 더 빠른 이동을 하는 현상
- direction = direction.normalized; 이동 입력 방향의 정규화를 통해 이동속도를 같게 해줌

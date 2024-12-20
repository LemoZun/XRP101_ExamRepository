# 3번 문제

주어진 프로젝트 는 다음의 기능을 구현하고자 생성한 프로젝트이다.

### 1. Turret
- Trigger 범위 내로 플레이어가 들어왔을 때 1.5초에 한번씩 플레이어를 바라보면서 총알을 발사한다
- Trigger 범위 바깥으로 플레이어가 나가면 발사를 중지한다.
- 오브젝트 풀을 사용해 총알을 관리한다.

### 2. Bullet :
- 20만큼의 힘으로 전방을 향해 발사된다
- 발사 후 5초 경과 시 비활성화 처리된다
- 플레이어를 가격했을 경우 2의 데미지를 준다

### 3. Player
- 총알과 충돌했을 때, 데미지를 입는다
- 체력이 0 이하가 될 경우 효과음을 재생하며 비활성화된다.
- 플레이어의 이동은 씬 뷰를 사용해 이동하도록 한다.

위 기능들을 구현하고자 할 때
제시된 프로젝트에서 발생하는 `문제들을 모두 서술`하고 올바르게 동작하도록 `소스코드를 개선`하시오.

## 답안
1. 터렛
- fireCoolTime은 인스펙터에서 1.5로 설정되어있는 상태
- 범위는 트리거로 설정되어있고 플레이어 태그가 트리거될시 트리거 된 물체의 transform으로 Fire 메서드 실행
  - ### 플레이어가 가까이 가도 트리거가 안되는 상황
  - 터렛의 감지 범위는 Sphere Collider로 IsTrigger로 설정되어있음
  - 터렛 컨트롤러는 터렛 오브젝트의 스크립트 컴포넌트로 존재
  - 플레이어는 자식의 body 객체에 Capsule 콜라이더 존재
  - 플레이어와 그 자식객체 모두 Player 태그가 붙어있다.
  - 터렛과 플레이어 모두 리지드 바디가 없어 물리충돌을 감지할 수 없어서 발생
  - **해결1** : 플레이어 Body에 리지드 바디를 달아서 해결함
- Fire메서드는 FireRoutine 코루틴 실행
- FireRoutine : _wait 만큼 대기 후 플레이어를 바라보도록 회전(?) - 회전값 맞는지 확인 필요
- 불릿풀에서 풀을 가져와 머즐포인트의 포지션에 위치시킴
- 불릿을 타겟에 발사시키려고함 , OnTaken 메서드 실행
  - 여기서 불릿은 PooledBehaviour로 되어있는데 그 안의 OnTaken은 가상함수로 실제 취하고싶은 메서드는 불릿컨트롤러에 구현됨
- ### 트리거 범위 밖으로 나가도 플레이어를 향해 총알을 계속 발사함
  - 트리거가 해제되었을 때 코루틴을 Stop해주는것이 구현되어 있지 않음
  - **해결3** 트리거 해제시 코루틴을 해제하여 발사를 중지함
- ### 플레이어가 비활성화 되었음에도 총알을 계속 발사함
  - 총알 발사 코루틴 해제를 트리거 해제시로 해줬기 때문
  - 플레이어가 비활성화 되서 사라지면 왜 해제판정이 나지 않는다
  - **해결7** if (!target.gameObject.activeInHierarchy) 조건으로 플레이어가 비활성화되었는지 확인하고 정지시켜주었다.

1. 불릿
- ### 불릿 컨트롤러에는 객체의 rigidBody를 참조해 AddForce를 수행하지만 불릿프리팹에는 Rigidbody 컴포넌트가 없다.
  - **해결2** : 불릿 프리팹 객체의 자식 Body에 rigidBody를 달아줌 + 자식의 body의 리지드바디를 참조하도록 변경
- 불릿의 Deactive Time은 5로 인스펙터에 설정되어있음 , 5초 후 비활성화 확인됨
- ### 총알을 일정 개수 이상 발사시 총알이 터렛에서 발사되지 않고 이전 총알이 있던 자리에서 생성되서 발사됨
  - 다른문제를 해결하는 상황중 해결됨(왜? ) + 문제가 변경됨
- ### 총알이 일정 개수 이상 발사시 속도가 변경된 총알이 나감
  - 리턴된 총알이 속도가 초기화되지 않은채 리턴되어 다시 활성화될때 기존 속도를 유지하며 addForce를 가해주어 생기는 문제
  - **해결8** 풀로 리턴 전, velocity를 Vector3.zero로 초기화 해준 뒤 리턴해 해결


1. 플레이어
- ### 총알을 맞아도 플레이어의 체력이 줄지 않음
  - bullet의 body에 RigidBody를 달아줬고 bullet의 body에 콜라이더 트리거 설정으로 되어있다
  - **해결4** bullet의 body에 있는 rigidBody를 bullet에 달아주어 트리거가 되게 만듬
  - 트리거는 되지만 여전히 플레이어의 체력이 줄어들진 않는다.
  - 에러코드 :NullReferenceException: Object reference not set to an instance of an object
  - **해결5** 트리거 되었을 때 충돌한 플레이어의 Body의 부모인 Player의 컴포넌트를 참조하도록 변경
- ### HP가 없어 비활성화 될 때 소리가 출력되지 않는다
  - 소리를 재생시켜주자 마자 비활성화 시켜 사실상 소리가 재생되지 않게됨
  - isPlaying을 썼더니 비명소리가 끝나기 전에 한번 더 죽어서 계속 비명소리가 이어지며 플레이어가 죽지 않음
  - yield return new WaitForSeconds(_audio.clip.length); 를 사용했더니 재생후 비활성화되지만 비활성화 전에 한번 더 맞을경우 잠깐 다시 오디오가 재생되며 사라짐
  - **해결6** private bool _isDeadAudioPlayed; 죽음 사운드를 재생시켰는지 저장할 bool 변수를 추가해 해결
  


    

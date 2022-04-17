<br/>
<br/>
<p align="center">
    <img src="https://avatars.githubusercontent.com/u/86725144?s=400&u=47e7489102492a6b381e3cdb200039e4e2d46c67&v=4" alt="coding" width="150px" />
</p>
<br/>
<p align="center">
  <strong>Single RPG Project</strong> 
  <br/>
  팀 단위로 제작한 자작 Single RPG
</p>
<br/>
<br/>

## Project 

 `Stack` **Unity, C#**   

 `Develop` **2021.12 ~ 2022.04**   

 `Made by` **TIOGX** 이효원, 정은지
<p>
<a href="https://github.com/leehyowonzero">
  <img src="https://github.com/leehyowonzero.png" width="150">
</a>
<a href="https://github.com/JeongEunJi1127">
  <img src="https://github.com/JeongEunJi1127.png" width="150">
</a>
</p>




## Index

 [**Features**](#features)<br>
    [1. 로비씬 : 씬 이동](#1-로비씬--씬-이동)<br>
    [2. 플레이어 이동 및 카메라 이동, 미니맵 구현](#2-플레이어-이동-및-카메라-이동-미니맵-구현)<br>
    [3. npc와의 상호작용](#3-npc와의-상호작용)<br>
    [4. npc와의 대화 및 퀘스트 시스템, UI](#4-npc와의-대화-및-퀘스트-시스템-ui)<br>
    [5. 퀘스트 구분 및 현재 진행중인 퀘스트 CHECK](#5-퀘스트-구분-및-현재-진행중인-퀘스트-check)<br>
    [6. 전투 퀘스트 진행 예시 및 몬스터와 플레이어 간의 상호작용(전투)](#6-전투-퀘스트-진행-예시-및-몬스터와-플레이어-간의-상호작용전투)<br>
    [7. 액션컨트롤러를 통한 아이템 퀘스트 진행 예시 및 아이템의 인벤토리에 저장](#7-액션컨트롤러를-통한-아이템-퀘스트-진행-예시-및-아이템의-인벤토리에-저장)<br>
    [8. 퀘스트 보상 및 store ui를 이용한 골드 소비](#8-퀘스트-보상-및-store-ui를-이용한-골드-소비)<br>
    [9. 스킬 구현 및 추가 기능 설명](#9-스킬-구현-및-추가-기능-설명)<br>
    [10. 보스 전투 시스템](#10-보스-전투-시스템)<br>
  
 [**GamePlay Video**](#gameplay-video)<br>
  
  



## Features
### 1. 로비씬 : 씬 이동 
<p>
<a>
  <img width="885" alt="스크린샷 2022-04-16 오전 12 10 35" src="https://user-images.githubusercontent.com/43170505/163591752-1bdde2e2-8bfd-434d-a3e9-4347f30dd0ae.png">
</a>
</p>

* UnityEngine.SceneManagement 라이브러리의 SceneManager.LoadScene() 함수를 사용하여 씬 이동 구현
  
### 2. 플레이어 이동 및 카메라 이동, 미니맵 구현
<p>
<a>
  <img width="882" alt="스크린샷 2022-04-16 오전 12 11 47" src="https://user-images.githubusercontent.com/22341383/163705136-2071b1d1-67a1-46fd-9910-2b0f949f7929.gif">

</a>
</p>

* 플레이어 이동
  
  * Input.GetAxis()를 통해 키보드 입력을 벡터값으로 변환하고, moveInput.magnitude를 통해 해당 백터의 크기를 구하여 0 이상의 경우(움직임이 있는 경우) isMove 변수를 true로 변환하여 움직이는 상태를 구분 할 수 있다.

  ```cs
  Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxi("Vertical"));
  bool isMove = moveInput.magnitude > 0;
  ```
  * 카메라가 바라보는 방향과 키보드 입력 벡터 사이의 연산을 통해 이동하고자 하는 방향 벡터를 구한 후, 플레이어 객체의 position을 해당 방향으로 moveSpeed 계수에 맞는 속도로 이동시킨다. 

  ```cs
  Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
  Vector3 lookright = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
  Vector3 moveDir = lookForward * moveInput.y + lookright * moveInput.x;

  characterBody.forward = moveDir;
  transform.position += moveDir * Time.deltaTime * moveSpeed;
  ```
* 카메라 방향 전환 및 줌인 줌아웃
  * 마우스 입력에 따라 카메라를 돌려준다. 이때 x,y축 방향으로 제한을 두어 특정 각도 이상 카메라가 회전하는 것을 막는다.
 ```cs
  mouseX = Input.GetAxis("Mouse X") * camSpeed; // 프로젝트 세팅에 입력 값 매니저를 보면 이름이 Mouse X로 설정되어있음
  mouseY = Input.GetAxis("Mouse Y") * camSpeed;

  Vector3 camAngle = centralAxis.rotation.eulerAngles;
  float x = camAngle.x - mouseY;
  if(x < 180f)
  {
  x = Mathf.Clamp(x, 0f, 70f);
  }
  else
  {
      x = Mathf.Clamp(x, 335f, 361f);
  }
  centralAxis.rotation = Quaternion.Euler(x, camAngle.y + mouseX, camAngle.z);
  ```
  * 마우스 휠을 활용하여 줌 구현 및 최대, 최소 줌 거리 제한
  
  ```cs
  private void Zoom()
    {
        wheel += Input.GetAxis("Mouse ScrollWheel");
        if (wheel >= -3){wheel = -3;}
        else if(wheel <= -7){wheel = -7;}
        cam.localPosition = new Vector3(0, 0, wheel);
    }
  ``` 
* 미니맵
  * 미니맵용 카메라 객체 생성하여 미니맵 레이어에 해당하는 미니맵 이미지를 촬영하는 형식으로 구현 

### 3. npc와의 상호작용
<p>
<a>
  <img width="883" alt="스크린샷 2022-04-16 오전 12 12 58" src="https://user-images.githubusercontent.com/43170505/163592268-5992c1e7-2dd2-49d7-b425-0528a5cd622a.png">
</a>
</p>

* 카메라가 바라보는 방향으로 발생한 Ray가  태그가 `Npc` 이고 `MainCamera layer`에 존재하는 오브젝트와 충돌하게 되면 NPCActionController.NPCInfoAppear()가 실행된다. 
* 이를 통해 현재  어떤 NPC와 상호작용 할 수 있는지 알려주는 문자가 게임 상단에 나타난다.

### 4. npc와의 대화 및 퀘스트 시스템, UI
<p>
<a>
  <img width="877" alt="스크린샷 2022-04-16 오전 12 13 38" src="https://user-images.githubusercontent.com/43170505/163592429-8122f05c-6601-4337-b7bd-c8e1c310982e.png">
  
  <img width="877" alt="스크린샷 2022-04-16 오전 12 54 18" src="https://user-images.githubusercontent.com/43170505/163592505-ad364ed4-d8b7-4202-919c-85b05a68ea2b.png">

</a>

### 5. 퀘스트 구분 및 현재 진행중인 퀘스트 CHECK
</p>

 * (T)키를 통해 상호작용을 하게 되면, NPCManager.InitUI()와  QuestManager.checkQuest()가 실행된다.
  - NPCManager.InitUI() 를 통해 충돌체의 QuestData와 NPCData에 접근한다.
만약 해당 NPC의 퀘스트가 진행될 차례가 아니라면 Idle() 상태의 _퀘스트를 주지 않는 기본 상태_ UI가 생성되고 그 반대라면 퀘스트가 시작된다.

 - QuestManager.checkQuest()를 통해 충돌체의 Id와 퀘스트 Type를 확인한다. 해당 정보로 퀘스트의 진행도와 클리어 여부를 확인한다.
    
    >Type 1 : 사냥 퀘스트  
    >Type 2 : 말걸기  
    >Type 3 : 아이템 획득

 * 퀘스트가 시작되면 현재 진행되고 있는 퀘스트의 내용과 진행도가 게임 화면 왼쪽 UI에 표시된다. 
또한 현재 퀘스트가 진행되고 있는 대상이 누구인지 NPC위의 마크를 통해 알 수 있다.

### 6. 전투 퀘스트 진행 예시 및 몬스터와 플레이어 간의 상호작용(전투)
<p>
<a>
  <img width="884" alt="스크린샷 2022-04-16 오전 12 18 05" src="https://user-images.githubusercontent.com/22341383/163705100-1cd7e38a-57c3-44c1-ae2c-8b8ad9e348e9.gif">
</a>
</p>

* 플레이어의 피격 및 몬스터의 피격 판정 구현
  * 플레이어의 피격
    * 몬스터의 경우 일정 거리 내에 플레이어가 들어오면 trace를 실시하며, 공격 사거리 안에 플레이어가 존재 할 시 공격을 실시
  * 몬스터의 피격
    * 플레이어가 공격키를 입력시 공격중인 상태가 되며 이때 무기와 적 사이의 충돌판정을 실시하여 충돌 시 1회에 한하여 플레이어의 공격력에 해당하는 데미지를 몬스터에가 가함

### 7. 액션컨트롤러를 통한 아이템 퀘스트 진행 예시 및 아이템의 인벤토리에 저장
<p>
<a>
  <img width="877" alt="스크린샷 2022-04-16 오전 1 00 45" src="https://user-images.githubusercontent.com/22341383/163705192-25e961e4-596c-49d6-8256-da00d74c4c45.gif">
  
</a>
</p>

* NCPActionContoller와 같은 원리로 아이템을 식별하고 줍기 키 (E)를 활성화하여 인벤토리에 추가하는 식의 작업을 실시

### 8. 퀘스트 보상 및 store ui를 이용한 골드 소비
<p>
<a>

  <img width="880" alt="스크린샷 2022-04-16 오전 12 21 21" src="https://user-images.githubusercontent.com/43170505/163593285-a2230f67-b5d3-4387-804c-07dd130b2303.png">
  <img width="885" alt="스크린샷 2022-04-16 오전 12 21 32" src="https://user-images.githubusercontent.com/43170505/163593309-6c38787b-9040-47fd-ada4-6a501f5526c7.png">
  <img width="877" alt="KakaoTalk_Photo_2022-04-16-01-07-56" src="https://user-images.githubusercontent.com/22341383/163705232-99bd3db6-2fb7-47f9-b070-5b5df7b403e5.gif">


</a>
</p>

* 현재 보유 골드에 따라 포션을 구매 할 수 있으며, 골드가 부족 시 UI 출력

### 9. 스킬 구현 및 추가 기능 설명
<p>
<a>
<img width="877" alt="KakaoTalk_Photo_2022-04-16-01-07-31" src="https://user-images.githubusercontent.com/22341383/163705330-c861682f-f86e-4060-9210-aae83476f0a0.gif">
<img width="877" alt="KakaoTalk_Photo_2022-04-16-01-07-56" src="https://user-images.githubusercontent.com/22341383/163705335-dcfd4941-7af3-4614-931d-5f428c25feca.gif">


</a>
</p>

* 각 스킬 prefab ,SkillpoolManager을 통해 스킬 시스템 구현
  * SkillpoolManager
  > key : Keycode, value : skillprefab 형태의 dict 자료구조를 통해 보유 스킬을 관리
  
  > 현재 등록된 스킬의 키보드 입력이 들어올 경우 KeyDictionary[dic.Key].GetComponent<Skill>().CanUseSkill() 

  * 각 스킬 prefab
  > 각 프리팹의 경우 고유한 스킬 이름, 데미지, 쿨타임 및 UseSkill() 메소드 보유
* 추가 기능
  * 플레이어 인포 UI
  * 게임 인포 UI
  * Respawn 기능
<p>
<a>

  <img width="885" alt="KakaoTalk_Photo_2022-04-16-01-08-30" src="https://user-images.githubusercontent.com/43170505/163594010-6440a373-3e2e-4162-9698-4f2c2cd440e0.png">

  <img width="876" alt="스크린샷 2022-04-16 오전 12 12 16" src="https://user-images.githubusercontent.com/43170505/163594117-995141d9-8720-42c3-bd5f-f39c8e6cec30.png">

  <img width="885" alt="KakaoTalk_Photo_2022-04-16-01-10-46" src="https://user-images.githubusercontent.com/22341383/163705408-da6df9f0-20fa-4f6a-b813-f1fd170c34d4.gif">
</a>
</p>

### 10. 보스 전투 시스템**
<p>
<a>
<img width="885" alt="KakaoTalk_Photo_2022-04-16-01-16-09" src="https://user-images.githubusercontent.com/43170505/163594823-41a6c336-770f-4e17-9250-f75f23d1b855.png">
<img width="885" alt="KakaoTalk_Photo_2022-04-16-01-16-09" src="https://user-images.githubusercontent.com/22341383/163705698-0aa63fe3-7c40-44c5-bda2-312a209c7e19.gif">
<img width="885" alt="KakaoTalk_Photo_2022-04-16-01-16-09" src="https://user-images.githubusercontent.com/22341383/163705759-8abb2879-6d03-470d-a8bf-d7f13ab8bb0c.gif">
<img width="885" alt="KakaoTalk_Photo_2022-04-16-01-16-09" src="https://user-images.githubusercontent.com/22341383/163705836-560263af-26bd-43e7-97fe-ed1bc58cbc04.gif">
<img width="880" alt="스크린샷 2022-04-16 오전 1 17 45" src="https://user-images.githubusercontent.com/43170505/163594986-515232c9-b44d-42f9-bb77-14bbc5f15d80.png">
</a>
</p>

* BossController.cs
  * 보스는 idle, tracing, die 상태를 가짐
  * 매 업데이트마다 CheckBossState() 함수를 통해 상태를 체크하고, 움직일 수 있는 상태라면 BossMove(), 공격 할 수 있는 상태라면 BossAttack() 함수를 실행한다.
    * BossMove()
    >  nav.SetDestination() 함수를 통해 플레이어 객체의 position을 TargetPos로 지정한 후 이동한다.
    * BossAttack()
    >  IsAttack() 함수를 통해 보스의 상태 및 공격 주기, 공격 사거리 내에 플레이어가 존재하는지 확인 한 후 공격을 실시

    > 랜덤 난수를 생성하여 패턴을 선택하고 해당 패턴을 사용한다. 이후 코루틴을 통해 공격 쿨타임을 갱신한다.


<hr>

## GamePlay Video

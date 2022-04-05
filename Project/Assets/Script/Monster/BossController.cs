using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BossController : Boss
{
    public static BossController instance;
    Animator animator;
    Rigidbody rigid;
    BoxCollider boxCollider;
    NavMeshAgent nav;
    Transform monsterBody;

    public float BossTracingRange;
    public Transform LookAtTarget; // 플레이어를 바라보기 위한 위치
    public Transform Target; // 플레이어 위치
    public BossState NowBossState;
    public bool isAttackDelay;
    public string SelectedPatternName;
    public string[] patternArr;
    public float BossAttackDelay;
    public GameObject LaserSphere;
    public GameObject FlameStream;
    private Quaternion _rot;
    public Transform PatternInstantiatePos;
    public bool IsHeadHutt;

    public enum BossState
    {
        idle, // 일반 상태
        tracing, // 추격 상태
        die // 죽음 상태
    }
    private void Awake()
    {
        instance = this;
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        nav = GetComponent<NavMeshAgent>();
        monsterBody = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        

    }
    void Start() // 이 스크립트를 on 해줬을 때 작동
    {
        Target = DummyController.instance.GetPlayerTransform(); // 플레이어를 타겟으로 잡음
        NowBossState = BossState.idle;
    }


    void Update() // 보스와의 전투 구조
    {
        if(NowBossState == BossState.die)
        {
            return;
        }

        CheckBossState();
        BossMove();
        BossAttack();

    }

    void CheckBossState()
    {
        if (isAttackDelay)
        {
            NowBossState = BossState.idle;
        }
        else if(Vector3.Distance(Target.position, transform.position) > BossTracingRange)
        {
            NowBossState = BossState.tracing;
        }
        else
        {
            NowBossState = BossState.idle;
        }
    }

    void BossMove()// 보스의 움직임 구조
                   // 보스의 상태가 die가 될 때 까지 계속 진행
                   // 보스의 상태가 die 가 아니면 idle 또는 tracing 상태
                   // idle 상태인 경우 제자리에 정지 -> 플레이가 때릴 수 있는 시간 및 보스도 공격 실시
                   // tracing 상태인 경우 플레이어 추격 -> 플레이어와 거리가 가까워지면
                   // 어떤 패턴의 공격을 할 지 결정하고 패턴 진행
    {
        transform.LookAt(LookAtTarget); // 항상 플레이어를 바라보도록
        if (NowBossState == BossState.die) // 보스의 상태가 die가 될 때 까지 계속 진행
        {
            // 전투 종료 구현
            nav.SetDestination(monsterBody.position); // 보스가 제자리에 멈춤
        }
        else // 보스의 상태가 die 가 아니면 idle 또는 tracing 상태
        {
            if (NowBossState == BossState.idle)
            {
                nav.SetDestination(monsterBody.position); // 보스가 제자리에 멈춤
            }
            else if (NowBossState == BossState.tracing) // 추격 상태
            {
                
                nav.SetDestination(Target.position); // 보스가 플레이어를 향해 다가감
            }
        }
    }

    void BossAttack() // 보스의 공격 구조
                      // 공격을 위한 조건이 만족되어야 공격 실시 -> 플레이어와의 거리와 공격 범위 및 idle 상태, 공격 쿨이 아닌지
    {
        if (IsAttack()) // 보스의 플레이어 공격 조건이 만족되었는지 확인
        {
            SelectedPatternName = SelectRandomPattern();  // 공격 패턴 고르기
            UsePattern(SelectedPatternName); // 패턴 사용
            isAttackDelay = true;
            StartCoroutine(setBossAttackDelay(BossAttackDelay));// 공격 딜레이 주기 
        }
    }
    IEnumerator setBossAttackDelay(float delayTime)
    {
        Debug.Log("delay " + delayTime + " time");
        yield return new WaitForSeconds(delayTime);
        isAttackDelay = false;
    }
    bool IsAttack()
    {
        if(NowBossState != BossState.idle)
        {
            return false;
        }
        if(Vector3.Distance(Target.position, transform.position) > BossAttackRange)
        {
            return false;
        }
        if(isAttackDelay == true)
        {
            return false;
        }
        return true;
    }
    string SelectRandomPattern()
    {
        int patternArrLen = patternArr.Length;
        System.Random r = new System.Random();
        int SelectedPatternIdx = r.Next(0, patternArrLen); // 랜덤 패턴 인덱스를 고름
        return patternArr[SelectedPatternIdx];
    }

    void UsePattern(string patternName)
    {
        switch (patternName)
        {
            case "1":
                Debug.Log("1번 패턴 사용");
                animator.SetTrigger("Pattern1");
                UseDoubleShot(Target);
                break;

            case "2":
                Debug.Log("2번 패턴 사용");
                animator.SetTrigger("Pattern2");
                UseFlameStream(Target);
                break;

            case "3":
                Debug.Log("3번 패턴 사용");
                animator.SetTrigger("Pattern3");
                UseHeadButt(Target);
                break;

            case "4":
                Debug.Log("4번 패턴 사용");
                animator.SetTrigger("Pattern4");
                break;
        }
    }

    public void BossTakeDamage(float value)
    {
        BossCurHealth -= value;
        if (BossCurHealth < 0)
        {
            BossCurHealth = 0;
        }
        if (BossCurHealth == 0)
        {
            BossDie();
        }
    }

    void BossDie()
    {
        NowBossState = BossState.die;
        SignalToPlayer(MonsterId);
    }
    void SignalToPlayer(int id)
    {
        DummyController.instance.killMonster(id);
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Who");
        if ( NowBossState == BossState.die) { return; }

        if (other.tag == "Weapon")
        {
            WeaponController Weapon = other.GetComponent<WeaponController>();
            if (Weapon.attackable)
            {
                animator.SetTrigger("GetHit");
                BossTakeDamage(Weapon.damage);
                Weapon.attackable = false;
            }
        }
    }

    void UseDoubleShot(Transform TargetTransform) // 구체 레이저 2개 발사 맞으면 데미지 및 폭발 파티클
    {
        CheckQuaternion(TargetTransform, new Vector3 (0, 5f, 0));
        ShotLaserSphere();
        CheckQuaternion(TargetTransform, new Vector3(0, 5f, 0));
        Invoke("ShotLaserSphere", 1f);
    }
    void CheckQuaternion(Transform TargetTransform , Vector3 x)
    {
        Vector3 _dir = ((TargetTransform.position - (transform.position + x)).normalized);
        _rot = Quaternion.LookRotation(_dir);
    }
    void ShotLaserSphere()
    {
        Instantiate(LaserSphere, PatternInstantiatePos.position, _rot);
    }

    void UseFlameStream(Transform TargetTransform)
    {
        CheckQuaternion(TargetTransform, new Vector3(0f, 3f, 0f));
        ShotFlameStream();
    }

    void ShotFlameStream()
    {
        Instantiate(FlameStream, PatternInstantiatePos.position, _rot); 
    }
    void UseHeadButt(Transform TargetTransform)
    {
        IsHeadHutt = true;
        StartCoroutine(setIsHeadButt(1.5f));
    }
    IEnumerator setIsHeadButt(float delayTime)
    {
        Debug.Log("delay " + delayTime + " time");
        yield return new WaitForSeconds(delayTime);
        IsHeadHutt = false;
    }
}

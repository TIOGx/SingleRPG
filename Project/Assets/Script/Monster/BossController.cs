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
    public Transform LookAtTarget; // ?????????? ???????? ???? ????
    public Transform Target; // ???????? ????
    public BossState NowBossState;
    public bool isAttackDelay;
    public string SelectedPatternName;
    public string[] patternArr;
    public float BossAttackDelay;
    public GameObject FlameStream;
    public GameObject LaserSphere;
    private Quaternion _rot;
    public Transform PatternInstantiatePos;
    public bool IsHeadHutt;
    public Image HpBar;

    public enum BossState
    {
        idle, // ???? ????
        tracing, // ???? ????
        die // ???? ????
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
    void Start() // ?? ?????????? on ?????? ?? ????
    {
        HpBar.rectTransform.localScale = new Vector3(BossCurHealth / BossMaxHealth, 1f, 1f);
        Target = DummyController.instance.GetPlayerTransform(); // ?????????? ???????? ????
        NowBossState = BossState.idle;
    }


    void Update() // ???????? ???? ????
    {
        if (NowBossState == BossState.die)
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
        else if (Vector3.Distance(Target.position, transform.position) > BossTracingRange)
        {
            NowBossState = BossState.tracing;
        }
        else
        {
            NowBossState = BossState.idle;
        }
    }

    void BossMove()// ?????? ?????? ????
                   // ?????? ?????? die?? ?? ?? ???? ???? ????
                   // ?????? ?????? die ?? ?????? idle ???? tracing ????
                   // idle ?????? ???? ???????? ???? -> ???????? ???? ?? ???? ???? ?? ?????? ???? ????
                   // tracing ?????? ???? ???????? ???? -> ?????????? ?????? ??????????
                   // ???? ?????? ?????? ?? ?? ???????? ???? ????
    {
        transform.LookAt(LookAtTarget); // ???? ?????????? ??????????
        if (NowBossState == BossState.die) // ?????? ?????? die?? ?? ?? ???? ???? ????
        {
            // ???? ???? ????
            nav.SetDestination(monsterBody.position); // ?????? ???????? ????
        }
        else // ?????? ?????? die ?? ?????? idle ???? tracing ????
        {
            if (NowBossState == BossState.idle)
            {
                nav.SetDestination(monsterBody.position); // ?????? ???????? ????
            }
            else if (NowBossState == BossState.tracing) // ???? ????
            {

                nav.SetDestination(Target.position); // ?????? ?????????? ???? ??????
            }
        }
    }

    void BossAttack() // ?????? ???? ????
                      // ?????? ???? ?????? ?????????? ???? ???? -> ???????????? ?????? ???? ???? ?? idle ????, ???? ???? ??????
    {
        if (IsAttack()) // ?????? ???????? ???? ?????? ???????????? ????
        {
            SelectedPatternName = SelectRandomPattern();  // ???? ???? ??????
            UsePattern(SelectedPatternName); // ???? ????
            isAttackDelay = true;
            StartCoroutine(setBossAttackDelay(BossAttackDelay));// ???? ?????? ???? 
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
        if (NowBossState != BossState.idle)
        {
            return false;
        }
        if (Vector3.Distance(Target.position, transform.position) > BossAttackRange)
        {
            return false;
        }
        if (isAttackDelay == true)
        {
            return false;
        }
        return true;
    }
    string SelectRandomPattern()
    {
        int patternArrLen = patternArr.Length;
        System.Random r = new System.Random();
        int SelectedPatternIdx = r.Next(0, patternArrLen); // ???? ???? ???????? ????
        return patternArr[SelectedPatternIdx];
    }

    void UsePattern(string patternName)
    {
        switch (patternName)
        {
            case "1":
                Debug.Log("1?? ???? ????");
                animator.SetTrigger("Pattern1");
                UseDoubleShot(Target);
                break;

            case "2":
                Debug.Log("2?? ???? ????");
                animator.SetTrigger("Pattern2");
                UseFlameStream(Target);
                break;

            case "3":
                Debug.Log("3?? ???? ????");
                animator.SetTrigger("Pattern3");
                UseHeadButt(Target);
                break;

            case "4":
                Debug.Log("4?? ???? ????");
                animator.SetTrigger("Pattern4");
                break;
        }
    }

    public void BossTakeDamage(float value)
    {
        BossCurHealth -= value;
        HpBar.rectTransform.localScale = new Vector3(BossCurHealth / BossMaxHealth, 1f, 1f);
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
        PlayerInfo.instance.gameclearCanvas.SetActive(true);
        Pause.Instance.TimePause();
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
        if (NowBossState == BossState.die) { return; }

        if (other.tag == "Weapon")
        {
            WeaponController Weapon = other.GetComponent<WeaponController>();
            if (Weapon.attackable)
            {
                animator.SetTrigger("GetHit");
                BossTakeDamage(PlayerInfo.instance.AttackDamage);
                Weapon.attackable = false;
            }
        }
    }

    void UseDoubleShot(Transform TargetTransform) // ???? ?????? 2?? ???? ?????? ?????? ?? ???? ??????
    {
        CheckQuaternion(TargetTransform, new Vector3(0, 5f, 0));
        ShotLaserSphere();
        CheckQuaternion(TargetTransform, new Vector3(0, 5f, 0));
        Invoke("ShotLaserSphere", 1f);
    }
    void CheckQuaternion(Transform TargetTransform, Vector3 x)
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

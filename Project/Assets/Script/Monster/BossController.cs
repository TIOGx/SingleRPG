using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BossController : Boss
{
    Animator animator;
    Rigidbody rigid;
    BoxCollider boxCollider;
    NavMeshAgent nav;
    Transform monsterBody;

    public float BossTracingRange;
    public Transform LookAtTarget; // �÷��̾ �ٶ󺸱� ���� ��ġ
    public Transform Target; // �÷��̾� ��ġ
    public BossState NowBossState;
    public bool isAttackDelay;
    public string SelectedPatternName;
    public string[] patternArr;
    public float BossAttackDelay;
    public GameObject LaserSphere;
    private Quaternion _rot;

    public enum BossState
    {
        idle, // �Ϲ� ����
        tracing, // �߰� ����
        die // ���� ����
    }
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        nav = GetComponent<NavMeshAgent>();
        monsterBody = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        

    }
    void Start() // �� ��ũ��Ʈ�� on ������ �� �۵�
    {
        Target = DummyController.instance.GetPlayerTransform(); // �÷��̾ Ÿ������ ����
        NowBossState = BossState.idle;
    }


    void Update() // �������� ���� ����
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

    void BossMove()// ������ ������ ����
                   // ������ ���°� die�� �� �� ���� ��� ����
                   // ������ ���°� die �� �ƴϸ� idle �Ǵ� tracing ����
                   // idle ������ ��� ���ڸ��� ���� -> �÷��̰� ���� �� �ִ� �ð� �� ������ ���� �ǽ�
                   // tracing ������ ��� �÷��̾� �߰� -> �÷��̾�� �Ÿ��� ���������
                   // � ������ ������ �� �� �����ϰ� ���� ����
    {
        transform.LookAt(LookAtTarget); // �׻� �÷��̾ �ٶ󺸵���
        if (NowBossState == BossState.die) // ������ ���°� die�� �� �� ���� ��� ����
        {
            // ���� ���� ����
            nav.SetDestination(monsterBody.position); // ������ ���ڸ��� ����
        }
        else // ������ ���°� die �� �ƴϸ� idle �Ǵ� tracing ����
        {
            if (NowBossState == BossState.idle)
            {
                nav.SetDestination(monsterBody.position); // ������ ���ڸ��� ����
            }
            else if (NowBossState == BossState.tracing) // �߰� ����
            {
                nav.SetDestination(Target.position); // ������ �÷��̾ ���� �ٰ���
            }
        }
    }

    void BossAttack() // ������ ���� ����
                      // ������ ���� ������ �����Ǿ�� ���� �ǽ� -> �÷��̾���� �Ÿ��� ���� ���� �� idle ����, ���� ���� �ƴ���
    {
        if (IsAttack()) // ������ �÷��̾� ���� ������ �����Ǿ����� Ȯ��
        {
            SelectedPatternName = SelectRandomPattern();  // ���� ���� ������
            UsePattern(SelectedPatternName); // ���� ���
            isAttackDelay = true;
            StartCoroutine(setBossAttackDelay(BossAttackDelay));// ���� ������ �ֱ� 
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
        int SelectedPatternIdx = r.Next(0, patternArrLen); // ���� ���� �ε����� ����
        return patternArr[SelectedPatternIdx];
    }

    void UsePattern(string patternName)
    {
        switch (patternName)
        {
            case "1":
                Debug.Log("1�� ���� ���");
                animator.SetTrigger("Pattern1");
                UseDoubleShot(Target);
                break;

            case "2":
                Debug.Log("2�� ���� ���");
                animator.SetTrigger("Pattern2");
                break;

            case "3":
                Debug.Log("3�� ���� ���");
                animator.SetTrigger("Pattern3");
                break;

            case "4":
                Debug.Log("4�� ���� ���");
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

    void UseDoubleShot(Transform TargetTransform) // ��ü ������ 2�� �߻� ������ ������ �� ���� ��ƼŬ
    {
        Vector3 _dir = ((TargetTransform.position - (transform.position + new Vector3(0f, 5f, 0f))).normalized);
        _rot = Quaternion.LookRotation(_dir);

        ShotLaserSphere();
        Invoke("ShotLaserSphere", 1f);
    }

    void ShotLaserSphere()
    {
        Instantiate(LaserSphere, transform.position + new Vector3(0f, 5f, 0f), _rot);
    }


}
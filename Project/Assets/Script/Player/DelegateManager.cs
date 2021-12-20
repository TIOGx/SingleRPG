using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelegateManager : MonoBehaviour // 연습
{
    public delegate void delegateTest(int value);
    delegateTest del;

    private int power;
    private int hp;

    public void setPower(int value) { power += value; print("power = " + value); }
    public void setHp(int value) { hp += value; print("hp = " + value); }

    void Start()
    {
        del += setPower;
        del += setHp;

        del(5);

        //del -= setHp;
    }

}

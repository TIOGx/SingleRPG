using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    private string name;
    
    public Skill(string _name)
    {
        this.name = _name;
    }
    public void UseSkill()
    {
        // ����Ʈ����� ��ƼŬ ���� �׷��� ���⼭ �밡�ٷ� ¥���ϳ�
        Debug.Log(name);

    }


}

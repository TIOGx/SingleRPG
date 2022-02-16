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
        // 이펙트라던가 파티클 생성 그런걸 여기서 노가다로 짜야하냐
        Debug.Log(name);

    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForDir : MonoBehaviour
{
    public static ForDir instance;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
}

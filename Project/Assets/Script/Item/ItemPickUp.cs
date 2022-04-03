using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Item item;
    float time;
    float destroy_time;
    private void Start()
    {
        time = 0.0f;
        destroy_time = 10f;
    }
    // Update is called once per frame 
    void Update()
    {
        time += Time.deltaTime;

        if (time >= destroy_time)
        {
            Destroy(gameObject);
        }
    }
}
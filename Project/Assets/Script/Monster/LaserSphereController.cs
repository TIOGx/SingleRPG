using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSphereController : MonoBehaviour
{
    private void Update()
    {
        transform.Translate(Vector3.forward * 0.1f);
    }
}

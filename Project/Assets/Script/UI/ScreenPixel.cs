using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenPixel : MonoBehaviour
{
    private Vector3 ScreenCenter;
    private void Start()
    {
        ScreenCenter = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(ScreenCenter);
        // Debug.DrawRay(transform.position, transform.forward * 10f, Color.red);
    }

}

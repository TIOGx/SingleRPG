using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSphereController : MonoBehaviour
{
    public GameObject Particle;
    private void Update()
    {
        transform.Translate(Vector3.forward * 0.1f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Instantiate(Particle, transform.position, Quaternion.identity);
            DummyController.instance.TakeDamage(10f);
        }
        else
        {
            Instantiate(Particle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}

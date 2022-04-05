using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameStreamChecker : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnParticleCollision(GameObject other)
    {
        if(other.tag == "Player")
        {
            DummyController.instance.TakeDamage(1f);
        }
    }
}

using UnityEngine;
using System.Collections;

public class FreeShot : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            Destroy(this.gameObject);
            MissionDemolition.RemoveShotFired();
        }
    }
}

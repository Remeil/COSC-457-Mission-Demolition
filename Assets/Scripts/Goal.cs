using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour
{
    public static bool goalMet = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            Goal.goalMet = true;
            Color goalColor = GetComponent<Renderer>().material.color;
            goalColor.a = 1;
            GetComponent<Renderer>().material.color = goalColor;
        }
    }
}

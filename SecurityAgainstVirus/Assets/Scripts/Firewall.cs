using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firewall : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            IEnumerator couritine = other.GetComponent<Virus>().WaitingForDeath();
            StartCoroutine(couritine);
            //other.GetComponent<Virus>().Die();
        }
    }
}

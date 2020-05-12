using System.Collections;
using UnityEngine;

public class Firewall : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            IEnumerator couritine = other.GetComponent<Virus>().WaitingForDeath();
            StartCoroutine(couritine);
        }

        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<Player>().isInFirewall = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<Player>().isInFirewall = false;
        }
    }
}

using System.Collections;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    private void PushBack(GameObject virus)
    {
        Vector3 distance = transform.position - virus.transform.position;
        virus.GetComponent<Rigidbody>().AddForce(distance.normalized * -200f);

        IEnumerator couritine = RestoreForceAfterTime(virus);
        StartCoroutine(couritine);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (Hands.isPunching && collision.gameObject.tag == "Enemy")
        {
            PushBack(collision.gameObject);
        }
    }

    private IEnumerator RestoreForceAfterTime(GameObject virus)
    {
        yield return new WaitForSeconds(3f);
        virus.GetComponent<Rigidbody>().velocity = Vector3.zero;
        virus.GetComponent<Rigidbody>().rotation = Quaternion.Euler(Vector3.zero);
        virus.transform.LookAt(transform.position);
    }
}

using System.Collections;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    private void PushBack(GameObject virus)
    {
        Vector3 distance = transform.position - virus.transform.position;
        virus.GetComponent<Rigidbody>().AddForce(distance.normalized * -30);

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
        Rigidbody rigidbody = virus.GetComponent<Rigidbody>();
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        rigidbody.rotation = Quaternion.Euler(Vector3.zero);
        virus.transform.LookAt(transform.position);
    }
}

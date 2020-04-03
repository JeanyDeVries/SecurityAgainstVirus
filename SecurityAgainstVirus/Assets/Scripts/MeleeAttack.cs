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
        //rigidbody.rotation = Quaternion.Euler(Vector3.zero);

        Vector3 targetForward = (virus.transform.position - transform.position);
        targetForward.Normalize();
        Quaternion targetRotation = Quaternion.LookRotation(targetForward);
        Quaternion currentRotation = virus.transform.rotation;

        float t = 0.0f;
        while (t < 1.0f)
        {
            t += Time.deltaTime / 1.0f;
            virus.transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, t);

            yield return null;
        }
        yield return null;
        //virus.transform.LookAt(transform.position);
    }
}

using System.Collections;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    private void PushBack(GameObject virus)
    {
        Vector3 distance = transform.position - virus.transform.position;
        virus.GetComponent<Rigidbody>().AddForce(distance.normalized * -30);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (AnimationHands.isPunching && collision.gameObject.tag == "Enemy")
        {
            PushBack(collision.gameObject);
        }
    }
}

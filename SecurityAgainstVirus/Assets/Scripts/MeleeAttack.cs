using System.Collections;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField]
    private float knockBackAmount;

    private void PushBack(GameObject virus)
    {
        Vector3 distance = transform.position - virus.transform.position;
        virus.GetComponent<Rigidbody>().AddForce(distance.normalized * -knockBackAmount);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (AnimationHands.isPunching && collision.gameObject.tag == "Enemy")
        {
            PushBack(collision.gameObject);
        }
    }
}

using UnityEngine;
using UnityEngine.AI;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] private float knockBackAmount;

    private AudioSource audioSourceAttack;

    private void PushBack(GameObject virus)
    {
        Vector3 distance = transform.position - virus.transform.position;
        virus.GetComponent<Rigidbody>().AddForce(distance.normalized * -knockBackAmount);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (AnimationHands.isPunching && collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<NavMeshAgent>().enabled = false;
            PushBack(collision.gameObject);
        }
    }
}

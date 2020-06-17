using UnityEngine;
using UnityEngine.AI;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] private float knockBackAmount;

    /// <summary>
    /// Adds force to the virus compared to the distance and the knockbackamount
    /// </summary>
    /// <param name="virus">Parameter value to pass.</param>
    private void PushBack(GameObject virus)
    {
        Vector3 distance = transform.position - virus.transform.position;
        virus.GetComponent<Rigidbody>().AddForce(distance.normalized * -knockBackAmount);
    }

    private void OnTriggerEnter(Collider collision)
    {
        //If it collides with the enemy(virus), it will disable the navmesh
        //so the virus can be pushed back
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<NavMeshAgent>().enabled = false;
            PushBack(collision.gameObject);
        }
    }
}

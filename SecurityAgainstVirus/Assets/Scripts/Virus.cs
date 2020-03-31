using System.Collections.Generic;
using UnityEngine;

public class Virus : MonoBehaviour
{
    [SerializeField]
    private float spotRange, attackRange;

    [SerializeField]
    private float movementSpeed;

    private void Update()
    {
        checkForCollision();
    }

    private void checkForCollision()
    {
        Collider[] hitColliders = 
            Physics.OverlapSphere(transform.position, spotRange);

        if (hitColliders == null) return;
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].tag == "Player")
                Follow(hitColliders[i].transform);
        }
    }

    private void Follow(Transform target)
    {
        transform.position = 
            Vector3.MoveTowards(transform.position, target.position, (movementSpeed * Time.deltaTime));
        float distance = 
            Vector3.Distance(transform.position, target.position);
        if (distance <= attackRange)
            Attack(target);
    }

    private void Attack(Transform target)
    {
        //Attack
        Debug.Log("Attack");

        float distance = 
            Vector3.Distance(transform.position, target.position);
        if (distance > attackRange)
            Follow(target);
    }
}

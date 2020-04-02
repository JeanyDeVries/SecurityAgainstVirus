using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : MonoBehaviour
{
    [SerializeField]
    private float spotRange, attackRange, 
        movementSpeed, damage, attackCooldown;

    private float elapsedTime;

    public virtual void Update()
    {
        elapsedTime += Time.deltaTime;

        checkForCollision();
    }

    public virtual void checkForCollision()
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

    public virtual void Follow(Transform target)
    {
        float distance =
            Vector3.Distance(transform.position, target.position);
        if (distance <= attackRange && elapsedTime >= attackCooldown)
        {
            Attack(target);
            elapsedTime = elapsedTime % attackCooldown;
            return;
        }

        Vector3 targetPos = new Vector3(target.position.x, target.position.y + 0.7f, target.position.z);
        transform.position = 
            Vector3.MoveTowards(transform.position, targetPos, (movementSpeed * Time.deltaTime));
    }

    public virtual void Attack(Transform target) 
    {
        float distance = 
            Vector3.Distance(transform.position, target.position);
        if (distance > attackRange)
            Follow(target);
        Player.playerProps.health -= damage;
        target.GetComponent<Player>().healthBar.SetHealth(Player.playerProps.health);
    }
}

using System.Collections;
using UnityEngine;

public class Virus : MonoBehaviour
{
    [SerializeField]
    private float spotRange, attackRange, 
        movementSpeed, damage, attackCooldown;

    private float elapsedTime;
    private GameObject player;

    public virtual void Update()
    {
        elapsedTime += Time.deltaTime;
        player = GameObject.FindGameObjectWithTag("Player");

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

    void OnCollisionEnter(Collision collision)
    {
        IEnumerator couritine = RestoreForceAfterTime();
        StartCoroutine(couritine);
    }

    private IEnumerator RestoreForceAfterTime()
    {
        yield return new WaitForSeconds(3f);
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;

        Vector3 targetForward = (transform.position - player.transform.position);
        targetForward.Normalize();
        Quaternion targetRotation = Quaternion.LookRotation(targetForward);
        Quaternion currentRotation = transform.rotation;

        float t = 0.0f;
        while (t < 1.0f)
        {
            t += Time.deltaTime / 1.0f;
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, t);

            yield return null;
        }
        yield return null;
    }
}

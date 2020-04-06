using System.Collections;
using UnityEngine;

public class Virus : MonoBehaviour
{
    [SerializeField]
    private VirusProps properties;

    private float elapsedTime;
    private GameObject player;
    private RaycastHit hit;

    public virtual void Update()
    {
        elapsedTime += Time.deltaTime;
        player = GameObject.FindGameObjectWithTag("Player");

        checkForCollision();
        CheckCollisionObstacles();
    }

    public virtual void checkForCollision()
    {
        Collider[] hitColliders = 
            Physics.OverlapSphere(transform.position, properties.spotRange);

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
        if (distance <= properties.attackRange && elapsedTime >= properties.attackCooldown)
        {
            Attack(target);
            elapsedTime = elapsedTime % properties.attackCooldown;
            return;
        }

        Vector3 targetPos = new Vector3(target.position.x, target.position.y + 0.7f, target.position.z);
        transform.position = 
            Vector3.MoveTowards(transform.position, targetPos, (properties.movementSpeed * Time.deltaTime));
    }

    public virtual void CheckCollisionObstacles()
    {
        Vector3 dir = (player.transform.position - transform.position).normalized;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 5))
        {
            if (hit.transform.gameObject.GetComponent<Rigidbody>() == null)
            {
                transform.position += Vector3.up * Time.deltaTime;
                return;
            }
        }

        Vector3 leftR = transform.position;
        Vector3 rightR = transform.position;

        leftR.x -= 2;
        rightR.x += 2;

        if (Physics.Raycast(leftR, transform.forward, out hit, 5))
        {
            if (hit.transform.gameObject.GetComponent<Rigidbody>() == null)
            {
                transform.position += Vector3.right * Time.deltaTime;
            }
        }

        if (Physics.Raycast(rightR, transform.forward, out hit, 5))
        {
            if (hit.transform.gameObject.GetComponent<Rigidbody>() == null)
            {
                transform.position += Vector3.left * Time.deltaTime;
            }
        }
    }

    public virtual void Attack(Transform target) 
    {
        float distance = 
            Vector3.Distance(transform.position, target.position);
        if (distance > properties.attackRange)
            Follow(target);
        Player.playerProps.health -= properties.damage;
        target.GetComponent<Player>().healthBar.SetHealth(Player.playerProps.health);
    }

    void OnCollisionEnter(Collision collision)
    {
        IEnumerator couritine = RestoreForceAfterTime();
        StartCoroutine(couritine);
    }

    private IEnumerator RestoreForceAfterTime()
    {
        yield return new WaitForSeconds(properties.restorationTime);
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

using System.Collections;
using UnityEngine;

public class Virus : MonoBehaviour
{
    [SerializeField]
    private VirusProps properties;

    private float elapsedTime;
    private GameObject player;
    private RaycastHit hit;
    private Vector3 lookDirection;
    private bool avoiding = false;

    public virtual void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        ObstacleAvoidance();
        checkForCollision();
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
        else
            elapsedTime = 0f;

        Vector3 targetPos = new Vector3(target.position.x, target.position.y + 0.7f, target.position.z);

        Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
        Quaternion currentRotation = transform.rotation;
        transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, Time.deltaTime);

        transform.Translate(Vector3.forward * properties.movementSpeed * Time.deltaTime);
    }

    public virtual void ObstacleAvoidance()
    {
        lookDirection = (player.transform.position - transform.position).normalized;

        float shoulderMultiplier = 0.75f;
        float rayDistance = 20;
        Vector3 leftRayPos = transform.position - (transform.right * shoulderMultiplier);
        Vector3 rightRayPos = transform.position + (transform.right * shoulderMultiplier);

        if(Physics.Raycast(leftRayPos, transform.forward, out hit, rayDistance))
        {
            Debug.DrawLine(transform.position, hit.point, Color.red);
            if(hit.collider.tag == ("Firewall") || hit.collider.tag == ("Obstacle"))
            {
                Debug.Log("Avoiding1");
                lookDirection += hit.normal * 20.0f;
                avoiding = true;
            }
        }
        else if (Physics.Raycast(rightRayPos, transform.forward, out hit, rayDistance))
        {
            Debug.DrawLine(transform.position, hit.point, Color.red);
            if(hit.collider.tag == ("Firewall") || hit.collider.tag == ("Obstacle"))
            {
                Debug.Log("Avoiding2");
                lookDirection += hit.normal * 20.0f;
                avoiding = true;
            }
        }
        else
        {
            Debug.DrawLine(leftRayPos, transform.forward * rayDistance, Color.yellow);
            Debug.DrawLine(rightRayPos, transform.forward * rayDistance, Color.yellow);
            avoiding = false;
        }
    }

    public virtual void Attack(Transform target) 
    {
        elapsedTime += Time.deltaTime;

        float distance = 
            Vector3.Distance(transform.position, target.position);
        if (distance > properties.attackRange)
            Follow(target);
        Player.playerProps.health -= properties.damage;
        target.GetComponent<Player>().healthBar.SetHealth(Player.playerProps.health);
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
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

        Vector3 targetForward = (player.transform.position - transform.position);
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

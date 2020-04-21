using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Virus : MonoBehaviour
{
    public VirusProps properties;

    private float elapsedTime;
    private int counter = 0;
    private bool isDying;
    private GameObject player;
    private AudioSource audioSource;
    private NavMeshAgent navMeshAgent;

    public virtual void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        navMeshAgent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = properties.deathSound;
    }

    public virtual void Update()
    {
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
        if (isDying) return;
        Vector3 targetPos = new Vector3(target.position.x,
            target.position.y + 1f,
            target.position.z);
        navMeshAgent.SetDestination(targetPos);

        float distance =
            Vector3.Distance(transform.position, target.position);
        if (distance <= properties.attackRange)
        {
            elapsedTime += Time.deltaTime;
            CheckIfInAttackDistance(target);
            return;
        }
    }

    public virtual void CheckIfInAttackDistance(Transform target)
    {
        elapsedTime += Time.deltaTime;
        navMeshAgent.SetDestination(transform.position);

        float distance =
            Vector3.Distance(transform.position, target.position);
        if (distance > properties.attackRange)
        {
            Follow(target);
            elapsedTime = 0f;
        }

        if (elapsedTime >= properties.attackCooldown)
        {
            DealDamage(target);
            elapsedTime = elapsedTime % properties.attackCooldown;
        }
    }

    public virtual void DealDamage(Transform target)
    {
        //Play attack animation + sound

        Player.playerProps.health -= properties.damage;
        target.GetComponent<Player>().healthBar.SetHealth(Player.playerProps.health);
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    public IEnumerator WaitingForDeath()
    {
        isDying = true;

        ParticleSystem deathEffect = null;
        if (counter == 0)
        {
            audioSource.Play();
            deathEffect = Instantiate(properties.deathEffect, transform.position, Quaternion.identity);
            deathEffect.Play();
            counter++;
        }

        yield return new WaitForSeconds(audioSource.clip.length);
        Die();
        Destroy(deathEffect);
        counter = 0;
    }

    public virtual void OnCollisionEnter(Collision collision)
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

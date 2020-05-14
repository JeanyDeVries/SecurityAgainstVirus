using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class Virus : MonoBehaviour
{
    [Header("Properties for the virus")]
    public VirusProps properties;

    [Header("Animations")]
    public Animator animator;

    private float elapsedTime;
    private int counter = 0;
    private bool isDying;
    private GameObject player;
    private AudioSource audioSourceAttack;
    private AudioSource audioSourceDeath;
    private NavMeshAgent navMeshAgent;

    public virtual void Awake()
    {
        animator = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player");
        navMeshAgent = GetComponent<NavMeshAgent>();

        audioSourceAttack = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        audioSourceAttack.clip = properties.attackSound;

        audioSourceDeath = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        audioSourceDeath.clip = properties.deathSound;
        audioSourceDeath.volume = 0.5f;
    }

    public virtual void Update()
    {
        checkForCollision();
    }

    public virtual void checkForCollision()
    {
        Collider[] hitColliders = 
            Physics.OverlapSphere(transform.position, properties.spotRange);

        if (hitColliders == null)
        {
            animator.SetBool("Idle", true);
            animator.SetBool("Attack", false);
            animator.SetBool("Follow", false);
            return;
        }

        for (int i = 0; i < hitColliders.Length; i++)
        {
            Vector3 dirToTarget = (player.transform.position - transform.position).normalized;
            float dstToTarget = Vector3.Distance(player.transform.position, transform.position);
            if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, LayerMask.GetMask("Wall")))
            {
                if (hitColliders[i].tag == "Player")
                {
                    Follow(hitColliders[i].transform);
                }
            }
            else
            {
                if (navMeshAgent != null && navMeshAgent.enabled == true)
                    navMeshAgent.SetDestination(transform.position);
            }
        }
    }

    public virtual void Follow(Transform target)
    {
        if (isDying) return;

        Vector3 targetPos = new Vector3(target.position.x,
            target.position.y + 1f,
            target.position.z);

        if (navMeshAgent.enabled == true)
            navMeshAgent.SetDestination(targetPos);

        float distance =
            Vector3.Distance(transform.position, target.position);
        if (distance <= properties.attackRange)
        {
            elapsedTime += Time.deltaTime;
            CheckIfInAttackDistance(target);
            return;
        }
        else
        {
            animator.SetBool("Idle", false);
            animator.SetBool("Attack", false);
            animator.SetBool("Follow", true);
        }
    }

    public virtual void CheckIfInAttackDistance(Transform target)
    {
        elapsedTime += Time.deltaTime;
        if(navMeshAgent.enabled == true)
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
        Vector3 targetForward = (player.transform.position - transform.position);
        targetForward.Normalize();
        Quaternion targetRotation = Quaternion.LookRotation(targetForward);

        //Play attack animation + sound
        animator.SetBool("Idle", false);
        animator.SetBool("Attack", true);
        animator.SetBool("Follow", false);
        audioSourceAttack.Play();

        Player.playerProps.health -= properties.damage;
        Player.playerProps.healthBar.SetHealth(Player.playerProps.health);
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
            audioSourceDeath.Play();
            deathEffect = Instantiate(properties.deathEffect, transform.position, Quaternion.identity);
            deathEffect.Play();
            counter++;
        }

        yield return new WaitForSeconds(audioSourceDeath.clip.length);
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

        this.gameObject.GetComponent<NavMeshAgent>().enabled = true;

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

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
        if(animator == null)
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

    /// <summary>
    /// Checks for collision, if there is none then the virus will be idle.
    /// If there is collision with the player, the navmesh will follow the player
    /// </summary>
    /// <param name="myBool">Parameter value to pass.</param>
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

    /// <summary>
    /// the navmeshagent follows the player, but when it is in attackrange
    /// it will attack the player.
    /// </summary>
    /// <param name="target">Parameter value to pass.</param>
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

    /// <summary>
    /// Checks if you can attack, if so deal damage. Also checks if you're
    /// out of attackrange, if so go back to follow
    /// </summary>
    /// <param name="target">Parameter value to pass.</param>
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

    /// <summary>
    /// It keeps looking at the player + the attackanimation will play.
    /// </summary>
    /// <param name="target">Parameter value to pass.</param>
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

        DealDamageToPlayer();
    }

    /// <summary>
    /// Deals damage to the player and drops the health
    /// </summary>
    public virtual void DealDamageToPlayer()
    {
        Player.playerProps.health -= properties.damage;
        Player.playerProps.healthBar.SetHealth(Player.playerProps.health);
    }

    private void Die()
    {
        if(gameObject)
            Destroy(gameObject);
    }

    /// <summary>
    /// Virus dies when the audio clip is finished. Before that it plays
    /// the audio and spawn a particle effect
    /// </summary>
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

    /// <summary>
    /// When the virus is pushed back it will, after the restorationtime,
    /// rotate back to the player and follow it again.
    /// </summary>
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

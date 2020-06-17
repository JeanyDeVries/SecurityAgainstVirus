using UnityEngine;

public class RansomwareVirus : Virus
{
    [Header("Attributes for ransomware only")]
    [SerializeField] private float drainAmount;
    [SerializeField] private ParticleSystem drainEffect;
    [SerializeField] private AudioClip drainSound;

    private float timer;
    private AudioSource drainAudioSource;
    private ParticleSystem drainParticles;
    private bool canSpawnParticles = true;

    public override void Awake()
    {
        base.Awake();

        drainAudioSource = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        drainAudioSource.clip = drainSound;
        drainAudioSource.loop = true;
        drainAudioSource.volume = 0.5f;
    }

    /// <summary>
    /// Checks if it is in distance and if it is in the firewall. 
    /// If the player is in the firewall it will not drain money
    /// </summary>
    /// <param name="target">Parameter value to pass.</param>
    public override void CheckIfInAttackDistance(Transform target) 
    {
        base.CheckIfInAttackDistance(target);

        bool isPlayerInFirewall = target.GetComponent<Player>().isInFirewall;
        if (!isPlayerInFirewall && timer >= properties.attackCooldown)
            DrainMoney();
        else if (isPlayerInFirewall)
            Reset();
    }

    /// <summary>
    /// Drains the money of the player, spawn particles and plays the correct animation
    /// </summary>
    private void DrainMoney()
    {
        if(canSpawnParticles)
        {
            drainParticles = Instantiate(drainEffect, transform.position, Quaternion.identity, this.transform);
            drainAudioSource.Play();
        }
        canSpawnParticles = false;
        Player.playerProps.money -= (int)drainAmount;

        animator.SetBool("Idle", false);
        animator.SetBool("Attack", true);
        animator.SetBool("Follow", false);
    }

    public override void Update()
    {
        base.Update();

        timer += Time.deltaTime;
    }

    public override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        if (collision.gameObject.tag == "Player")
            Reset();
    }

    public override void Follow(Transform target)
    {
        base.Follow(target);

        float distance =
            Vector3.Distance(transform.position, target.position);
        if(distance > properties.attackRange)
            Reset();
    }

    private void Reset()
    {
        timer = 0.0f;
        canSpawnParticles = true;
        drainAudioSource.Stop();
        Destroy(drainParticles);
    }

    /// <summary>
    /// It overrides the method so it won't deal close damage. 
    /// It will just be in idle state
    /// </summary>
    /// <param name="target">Parameter value to pass.</param>
    public override void DealDamage(Transform target)
    {
        animator.SetBool("Idle", true);
        animator.SetBool("Attack", false);
        animator.SetBool("Follow", false);
    }
}

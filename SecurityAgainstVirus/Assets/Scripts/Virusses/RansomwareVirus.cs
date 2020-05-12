using UnityEngine;

public class RansomwareVirus : Virus
{
    //Drains points/money from the player when not killed or attacked in time
    [Header("Attributes for ransomware only")]
    [SerializeField]
    private float drainAmount;

    [SerializeField]
    private ParticleSystem drainEffect;

    private float timer;
    private ParticleSystem drainParticles;
    private bool canSpawnParticles = true;

    public override void CheckIfInAttackDistance(Transform target) 
    {
        base.CheckIfInAttackDistance(target);

        bool isPlayerInFirewall = target.GetComponent<Player>().isInFirewall;
        if (!isPlayerInFirewall && timer >= properties.attackCooldown)
            DrainMoney();
        else if (isPlayerInFirewall)
            Reset();
    }

    private void DrainMoney()
    {
        if(canSpawnParticles)
            drainParticles = Instantiate(drainEffect, transform.position, Quaternion.identity, this.transform);
        drainParticles.Play();
        canSpawnParticles = false;
        Player.playerProps.money -= (int)drainAmount;
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

    private void Reset()
    {
        timer = 0.0f;
        drainParticles.Stop();
        canSpawnParticles = true;
        Destroy(drainParticles); 
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RansomwareVirus : Virus
{
    //Drains points/money from the player when not killed or attacked in time
    [SerializeField]
    private float drainAmount;

    private float timer;

    public override void CheckIfInAttackDistance(Transform target) 
    {
        base.CheckIfInAttackDistance(target);

        if(timer >= properties.attackCooldown)
            DrainMoney();

        Debug.Log(timer);
    }

    private void DrainMoney()
    {
        Player.playerProps.money -= (int)(drainAmount * (timer/100));
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
            timer = 0f;
    }
}

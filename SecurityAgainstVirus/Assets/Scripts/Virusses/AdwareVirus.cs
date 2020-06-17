using System.Collections.Generic;
using UnityEngine;

public class AdwareVirus : Virus
{
    [SerializeField] private GameObject ad;
    [SerializeField] private int adsAmount;
    [SerializeField] private Transform adsPukePoint;

    private List<GameObject> ads = new List<GameObject>();

    public override void DealDamage(Transform target)
    {
        base.DealDamage(target);

        //When it is in the attack state, it will spawn ads
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Sting") 
            || animator.GetCurrentAnimatorStateInfo(0).IsName("ReverseSting 0 0"))
        {
            for (int i = 0; i < adsAmount; i++)
            {
                GameObject newAd = Instantiate(ad, adsPukePoint.transform.position, Quaternion.identity, this.transform);
                ads.Add(newAd);
            }
        }
    }

    //This is overridden so it won't do the base method
    public override void DealDamageToPlayer()
    {
        
    }
}

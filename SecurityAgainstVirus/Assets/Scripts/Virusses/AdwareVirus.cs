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

    public override void DealDamageToPlayer()
    {
        
    }
}

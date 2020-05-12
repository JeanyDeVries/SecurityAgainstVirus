using System.Collections.Generic;
using UnityEngine;

public class AdwareVirus : Virus
{
    [SerializeField] private GameObject ad;
    [SerializeField] private int adsAmount;

    private List<GameObject> ads = new List<GameObject>();

    public override void DealDamage(Transform target)
    {
        for (int i = 0; i < adsAmount; i++)
        {
            GameObject newAd = Instantiate(ad, transform.position, Quaternion.identity, this.transform);
            ads.Add(newAd);
        }
    }
}

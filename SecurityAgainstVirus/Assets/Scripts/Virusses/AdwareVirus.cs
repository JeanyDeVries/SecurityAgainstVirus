using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdwareVirus : Virus
{
    [SerializeField]
    private GameObject ad;

    [SerializeField]
    private int adsAmount;

    private List<GameObject> ads = new List<GameObject>();

    public override void DealDamage(Transform target)
    {
        //Puke ads that deal damage
        for (int i = 0; i < adsAmount; i++)
        {
            Debug.Log("Spawning ads");
            GameObject newAd = Instantiate(ad, transform.position, Quaternion.identity);
            ads.Add(newAd);
        }
    }

    public override void Update()
    {
        base.Update();

       
    }
}

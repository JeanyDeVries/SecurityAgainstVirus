using System.Collections;
using UnityEngine;

public class TrojanVirus : Virus
{
    //Disguise as a human but when approached closed changes to malware virus
    [Header("Models")]
    [SerializeField]
    private GameObject humanModel;

    [SerializeField]
    private GameObject malwareModel;

    private GameObject currentModel;
    private int counter = 0;

    public override void Awake()
    {
        currentModel = Instantiate(humanModel, transform.position, transform.rotation) as GameObject;
        currentModel.transform.parent = transform;

        base.Awake();
    }

    public override void Follow(Transform target)
    {
        IEnumerator couritine = base.WaitingForDeath();
        StartCoroutine(couritine);

        Destroy(currentModel);

        //Reveal itself
        if (counter == 0f)
        {
            Instantiate(malwareModel, transform.position, transform.rotation);
            counter++;
        }
    }
}

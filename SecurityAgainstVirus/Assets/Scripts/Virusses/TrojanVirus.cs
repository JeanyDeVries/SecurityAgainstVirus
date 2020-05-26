using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrojanVirus : Virus
{
    [Header("Models")]
    [SerializeField] private List <GameObject> humanModels;
    [SerializeField] private GameObject malwareModel;

    private GameObject currentModel;
    private int counter = 0;

    public override void Awake()
    {
        int randomHumanIndex = Random.Range(0, humanModels.Count - 1);

        currentModel = Instantiate(humanModels[randomHumanIndex], transform.position, transform.rotation) as GameObject;
        currentModel.transform.parent = transform;

        base.Awake();
    }

    public override void Follow(Transform target)
    {
        IEnumerator couritine = base.WaitingForDeath();
        StartCoroutine(couritine);

        if(currentModel)
            Destroy(currentModel);

        if (counter == 0f)
        {
            Instantiate(malwareModel, transform.position, transform.rotation);
            counter++;
        }
    }
}

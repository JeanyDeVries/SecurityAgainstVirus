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
        //Selects a random model from the array in the inspector
        int randomHumanIndex = Random.Range(0, humanModels.Count );

        //Sets the current model to the human model, it is a child of the object
        currentModel = Instantiate(humanModels[randomHumanIndex], transform.position, transform.rotation) as GameObject;
        currentModel.transform.parent = transform;

        base.Awake();
    }

    /// <summary>
    /// It overrides follow so when the player is in range it will change the model.
    /// When it is in range, it will destroy the human model and instatiate a malware virus as child
    /// </summary>
    /// <param name="target">Parameter value to pass.</param>
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

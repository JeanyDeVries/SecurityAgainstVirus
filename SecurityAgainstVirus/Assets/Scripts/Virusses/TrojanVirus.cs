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

    public override void Awake()
    {
        currentModel = Instantiate(humanModel, transform.position, transform.rotation) as GameObject;
        currentModel.transform.parent = transform;
    }

    public override void Follow(Transform target)
    {
        //Reveal itself
        GameObject thisModel = Instantiate(malwareModel, transform.position, transform.rotation) as GameObject;
        Destroy(currentModel);
        thisModel.transform.parent = transform;
        currentModel = thisModel;
    }
}

using UnityEngine;

public class CameraTransition : MonoBehaviour
{
    [SerializeField]
    private float range, zoomInSpeed;

    [SerializeField]
    private Transform targetPoint;

    void Update()
    {
        Collider[] hitColliders =
            Physics.OverlapSphere(transform.position, range);

        if (hitColliders == null) return;

        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].tag == "Player")
            {
                Camera.main.transform.position = 
                    Vector3.Slerp(Camera.main.transform.position, targetPoint.position, zoomInSpeed * Time.deltaTime);

            }
        }
    }
}

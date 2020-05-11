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

                Cursor.lockState = CursorLockMode.None;
            }

            float distance = Vector3.Distance(Camera.main.transform.position, transform.position);
            if(distance >= range && distance < range + 5f)
                Reset();
        }
    }

    void Reset()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}

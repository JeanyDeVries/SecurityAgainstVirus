using UnityEngine;

public class CameraTransition : MonoBehaviour
{
    [Header("Properties that can be changed and balanced")]
    [SerializeField] private float range, zoomInSpeed;

    [Header("Additional properties")]
    [SerializeField] private Transform targetPoint;

    private void Update()
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
                if(Camera.main.transform.position == targetPoint.position)
                    hitColliders[i].gameObject.transform.position = targetPoint.position;

                Cursor.lockState = CursorLockMode.None;
            }

            float distance = Vector3.Distance(Camera.main.transform.position, transform.position);
            if(distance >= range && distance < range + 5f)
                Reset();
        }
    }

    private void Reset()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}

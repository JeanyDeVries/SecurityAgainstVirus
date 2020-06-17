using UnityEngine;

public class CameraTransition : MonoBehaviour
{
    [Header("Properties that can be changed and balanced")]
    [SerializeField] private float range;

    private void Update()
    {
        //Checks if the player or any other object is in range of the computer
        Collider[] hitColliders =
            Physics.OverlapSphere(transform.position, range);

        if (hitColliders == null) return;

        for (int i = 0; i < hitColliders.Length; i++)
        {
            //if the player is in range, show the cursor
            if (hitColliders[i].tag == "Player")
            {
                Cursor.lockState = CursorLockMode.None;
            }

            //Reset when player is out of range
            float distance = Vector3.Distance(Camera.main.transform.position, transform.position);
            if(distance >= range && distance < range + 5f)
                Reset();
        }
    }

    /// <summary>
    /// Hides the cursor again
    /// </summary>
    private void Reset()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}

using UnityEngine;

public class EndPoint : MonoBehaviour
{
    [SerializeField] private GameObject victoryUI;

    private void OnTriggerEnter(Collider other)
    {
        Time.timeScale = 0.0f;
        victoryUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }
}

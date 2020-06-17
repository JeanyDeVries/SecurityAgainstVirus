using System.Collections;
using UnityEngine;

public class ShieldPickup : MonoBehaviour
{
    [SerializeField] private int protectionPickupAmount;
    [SerializeField] private float rotationSpeed;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            AddHealth();
    }

    /// <summary>
    /// Adds health to the player and plays the audio
    /// </summary>
    private void AddHealth()
    {
        Player.playerProps.health += protectionPickupAmount;
        Player.playerProps.healthBar.SetHealth(Player.playerProps.health);

        audioSource.Play();
        IEnumerator couritine = WaitingForDeath();
        StartCoroutine(couritine);
    }

    private void Update()
    {
        transform.rotation =
            Quaternion.Euler(0, 180 * Mathf.Sin(Time.time * rotationSpeed), 0f);
    }

    /// <summary>
    /// Destroys the object when the audioclip has been finished
    /// </summary>
    private IEnumerator WaitingForDeath()
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        Destroy(gameObject);
    }
}

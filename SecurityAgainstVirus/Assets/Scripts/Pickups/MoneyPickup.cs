using System.Collections;
using UnityEngine;

public class MoneyPickup : MonoBehaviour
{
    [SerializeField] private int moneyPickupAmount;
    [SerializeField] private float rotationSpeed;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            AddMoney();
    }

    private void AddMoney()
    {
        Player.playerProps.money += moneyPickupAmount;

        audioSource.Play();
        IEnumerator couritine = WaitingForDeath();
        StartCoroutine(couritine);
    }

    private void Update()
    {
        transform.rotation = 
            Quaternion.Euler(-90f, 0f,180 * Mathf.Sin(Time.time * rotationSpeed));
    }

    private IEnumerator WaitingForDeath()
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        Destroy(gameObject);
    }
}

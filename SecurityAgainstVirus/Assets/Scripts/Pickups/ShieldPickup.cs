using UnityEngine;

public class ShieldPickup : MonoBehaviour
{
    [SerializeField] private int protectionPickupAmount;
    [SerializeField] private float rotationSpeed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            AddMoney();
    }

    private void AddMoney()
    {
        Player.playerProps.health += protectionPickupAmount;
        Player.playerProps.healthBar.SetHealth(Player.playerProps.health);
        Destroy(gameObject);
    }

    private void Update()
    {
        transform.rotation =
            Quaternion.Euler(0, 180 * Mathf.Sin(Time.time * rotationSpeed), 0f);
    }
}

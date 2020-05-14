using UnityEngine;

public class MoneyPickup : MonoBehaviour
{
    [SerializeField] private int moneyPickupAmount;
    [SerializeField] private float rotationSpeed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            AddMoney();
    }

    private void AddMoney()
    {
        Player.playerProps.money += moneyPickupAmount;
        Destroy(gameObject);
    }

    private void Update()
    {
        transform.rotation = 
            Quaternion.Euler(-90f, 0f,180 * Mathf.Sin(Time.time * rotationSpeed));
    }
}

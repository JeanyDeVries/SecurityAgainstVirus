using UnityEngine;

public class adsScript : MonoBehaviour
{
    [SerializeField]
    private float damage;
    
    private void OnTriggerEnter(Collider other)
    {
        //Play hit animation

        Player.playerProps.health -= damage;
        if(other.GetComponent<Player>() != null)
            other.GetComponent<Player>().healthBar.SetHealth(Player.playerProps.health);

        Debug.Log("Deal damage");
    }
}

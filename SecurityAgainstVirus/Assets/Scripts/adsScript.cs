using System.Collections;
using UnityEditor.UIElements;
using UnityEngine;

public class adsScript : MonoBehaviour
{
    [SerializeField]
    private float damage;

    private float frames = 1;
   
    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime);
        float posY = (frames/60) / 10;
        transform.position = Vector3.Lerp(transform.position,
            new Vector3(Random.Range(0, 0.2f), posY,
            Random.Range(0.2f, 0.4f)), Time.deltaTime);
        frames++;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Play hit animation

        if (other.GetComponent<Player>() != null)
        {
            Player.playerProps.health -= damage;
            other.GetComponent<Player>().healthBar.SetHealth(Player.playerProps.health);
        }
        IEnumerator couritine = WaitingToDestroy();
        StartCoroutine(couritine);
    }

    private IEnumerator WaitingToDestroy()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }

}

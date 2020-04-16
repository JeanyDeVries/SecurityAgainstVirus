using System.Collections;
using UnityEditor.UIElements;
using UnityEngine;

public class adsScript : MonoBehaviour
{
    [SerializeField]
    private float damage, duration;

    [SerializeField]
    private AnimationCurve curve;

    private float time;
    private Vector3 start, end;
    private GameObject target;

    private void Start()
    {
        start = transform.position;
        target = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(Curve());
        Vector3 targetPos = new Vector3(Random.Range(target.transform.position.x - 0.5f, target.transform.position.x + 0.5f),
            Random.Range(target.transform.position.y - 0.5f, target.transform.position.y + 0.5f),
            Random.Range(target.transform.position.z - 0.5f, target.transform.position.z + 0.5f));
        end = targetPos;
    }

    IEnumerator Curve()
    {
        while (time < duration)
        {
            time += Time.deltaTime;

            float linearT = time / duration;
            float heightT = curve.Evaluate(linearT);

            float height = Mathf.Lerp(0f, transform.localScale.y, heightT);

            transform.position = Vector3.Lerp(start, end, linearT) + new Vector3(0f, height, 0f);

            if (transform.position == end)
            {
                Destroy(this.gameObject);
            }

            yield return null;
        }
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
        yield return new WaitForSeconds(duration);
        Destroy(this.gameObject);
    }

}

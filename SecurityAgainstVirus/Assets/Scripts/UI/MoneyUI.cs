using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour
{
    private Text text;

    private void Start()
    {
        text = GetComponent<Text>();
    }

    private void Update()
    {
        text.text = "€" + Player.playerProps.money.ToString();
    }
}

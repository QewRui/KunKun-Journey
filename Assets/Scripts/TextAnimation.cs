using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TextAnimation : MonoBehaviour
{
    public Text text;

    private void Start()
    {
        if (text == null)
        {
            text = GetComponent<Text>();
        }

        StartBlinking();
    }

    private void OnEnable()
    {
        StartBlinking();
    }

    private void OnDisable()
    {
        StopBlinking();
    }

    IEnumerator Blink()
    {
        while (true)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
            yield return new WaitForSeconds(1.5f);

            text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
            yield return new WaitForSeconds(0.5f);
        }
    }

    void StartBlinking()
    {
        StopBlinking();
        StartCoroutine(Blink());
    }

    void StopBlinking()
    {
        StopCoroutine(Blink());
    }
}
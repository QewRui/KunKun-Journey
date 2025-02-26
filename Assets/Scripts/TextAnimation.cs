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
<<<<<<< HEAD
            text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
            yield return new WaitForSeconds(1.0f);

            text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
            yield return new WaitForSeconds(1.0f);
=======
            text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
            yield return new WaitForSeconds(1.5f);

            text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
            yield return new WaitForSeconds(0.5f);
>>>>>>> 5d8d32b9ddc80184a0a87df05555cecbbbc5fd4b
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
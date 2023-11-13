using System.Collections;
using UnityEngine;
using TMPro;

public sealed class ColorLerp : MonoBehaviour
{
    [SerializeField] Color startColor = Color.red;
    [SerializeField] Color endColor = Color.blue;
    [SerializeField] float duration = 2f;
    TextMeshProUGUI target;

    private void Start()
    {
        target = GetComponent<TextMeshProUGUI>();

        StartCoroutine(LerpColor());
    }

    IEnumerator LerpColor()
    {
        float timeElapsed = 0f;

        Color start = startColor;
        Color end = endColor;

        while (true)
        {
            target.color = Color.Lerp(start, end, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null; 

            if(timeElapsed >= duration)
            {
                timeElapsed = 0.0f;

                Color temp = start;
                start = end;
                end = temp;
            }
        }
    }
}

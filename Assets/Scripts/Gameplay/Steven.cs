using System.Collections;
using UnityEngine;

public sealed class Steven : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(ColorRoutine());
    }

    IEnumerator ColorRoutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.1f);
            GetComponent<Renderer>().material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1.0f);
        }
    }
}

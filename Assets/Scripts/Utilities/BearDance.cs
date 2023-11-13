using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BearDance : MonoBehaviour
{
    RawImage bear;

    private void Awake()
    {
        bear = GetComponent<RawImage>();   
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ScrollRoutine());
    }


    IEnumerator ScrollRoutine()
    {
        float x = 0.0f;

        while (true)
        {
            bear.uvRect = new Rect
            {
                x = -x,
                y = x,
                width = 10,
                height = 2
            };

            x += Time.deltaTime;

            yield return null;
        }
    }

}

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BearDance : MonoBehaviour
{
    RawImage bear;
    [SerializeField] bool inverse;

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

        float initX = bear.uvRect.x;
        float initY = bear.uvRect.y;

        while (true)
        {
            bear.uvRect = new Rect
            {
                x = initX + -x,
                y = initY + x,
                width = 10,
                height = 2
            };

            if(!inverse)
                x += Time.deltaTime;
            else
                x -= Time.deltaTime;

            yield return null;
        }
    }

}

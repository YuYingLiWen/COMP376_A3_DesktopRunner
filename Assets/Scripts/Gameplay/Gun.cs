
using UnityEditor.Animations;
using UnityEngine;

public sealed class Gun : MonoBehaviour
{
    Animator control;

    private void Awake()
    {
        control = GetComponent<Animator>();
    }
    
    void Start()
    {

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            control.SetTrigger("Fire");
        }
    }
}

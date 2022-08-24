using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ButtonTrigger : MonoBehaviour
{
    [SerializeField] private GameObject ButtonTop;
    [SerializeField] private Color Color1;
    [SerializeField] private Color Color2;
    private Renderer btnRender;
    private bool colorswap = false;

    public UnityEvent Pressed;
    // Setup References and listener for changing button color
    void Start()
    {
        btnRender = ButtonTop.GetComponent<Renderer>();
        Pressed.AddListener(OnButtonPressed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Button")   //We only want to fire if we were triggered by our button, so we check the tag
        {
            Pressed.Invoke();
        }
    }

    // Functions to run when the button is pressed / triggered above
    void OnButtonPressed()
    {
        Debug.Log("OnButtonPressed is working");
        ChangeColorV2();
    }

    void ChangeColorV2()
    {
        //Ternary Operator can be read as (TrueOrFalseStatement ? ValueIfTrue : ValueIfFalse)
        btnRender.material.color = (colorswap ? Color1 : Color2);
        colorswap = !colorswap; //Invert the color swap boolean
    }

    // Old Way to switch colors
    //void ChangeColorV1()
    //{
    //     This could be replaced with a Ternary Operator
    //    if(colorswap)
    //    {
    //        btnRender.material.color = Color1;
    //    }
    //    else
    //    {
    //        btnRender.material.color = Color2;
    //    }
    //    colorswap = !colorswap;
    //}
}

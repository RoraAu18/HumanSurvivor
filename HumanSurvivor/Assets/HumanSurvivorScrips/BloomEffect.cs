using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloomEffect : MonoBehaviour
{
    public Renderer myRender;
    private Color targetColor=Color.white*8;
    private Color inicialColor;
    public float transitionDuration;
   

    void Update()
    {



    }

    void Start()
    {
        inicialColor = myRender.material.color;
        StartCoroutine(ColorLoopCoroutine());
    }

    private IEnumerator ColorLoopCoroutine()
    {
        while (true)
        {
            yield return ColorTransition(targetColor);
            yield return ColorTransition(inicialColor);
            yield return new WaitForSeconds(3);

        }
    }

    private IEnumerator ColorTransition(Color targetColor)
    {
        float elapsedTime = 0f;
        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            myRender.material.color = Color.Lerp(myRender.material.color, targetColor, elapsedTime / transitionDuration);
            yield return null;
        }
    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloomEffect : MonoBehaviour
{
    public Renderer myRender;
    private Color targetColor=Color.white*6;
    private Color inicialColor;
    public float transitionDuration;
    public ParticleSystem particle;
    public float waitTime;



    void Update()
    {



    }

    private void OnEnable()
    {
        inicialColor = myRender.material.color;
        StartCoroutine(ColorLoopCoroutine());
    }
    //void Start()
    //{
    //    inicialColor = myRender.material.color;
    //    StartCoroutine(ColorLoopCoroutine());
    //}

    private IEnumerator ColorLoopCoroutine()
    {
        while (true)
        {
            particle.Play();
            yield return ColorTransition(targetColor);
            particle.Stop();
            yield return ColorTransition(inicialColor);
            yield return new WaitForSeconds(waitTime);

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



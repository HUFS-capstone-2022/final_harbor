using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalFading : MonoBehaviour
{
    public bool fadeOnStart = true;
    public float fadeDuration = 2.0f;
    public Color fadeColor;
    private Renderer render;

    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<Renderer>();
        if (fadeOnStart)
        {
            StartCoroutine(FadeIn());
        }
    }

    void Update()
    {
        if (NormalManager.Instance.makeFadeOut)
        {
            StartCoroutine(FadeOut());
            Debug.Log("Fade In Coroutine called!!");
            NormalManager.Instance.makeFadeOut = false;
        }
    }

    public IEnumerator FadeIn()
    {
        float timer = 0.0f;
        while (timer < fadeDuration)
        {
            Color fiColor = fadeColor;
            fiColor.a = Mathf.Lerp(1, 0, timer / fadeDuration);
            render.material.SetColor("_Color", fiColor);

            timer += Time.deltaTime;
            yield return null;
        }
        Color fiColor2 = fadeColor;
        fiColor2.a = Mathf.Lerp(1, 0, timer / fadeDuration);
        render.material.SetColor("_Color", fiColor2);

        NormalManager.Instance.makeStart = true;
        NormalManager.Instance.carMoves = true;
        NormalManager.Instance.carMoves2 = true;
    }

    public IEnumerator FadeOut()
    {
        float timer = 0.0f;
        while (timer < fadeDuration)
        {
            Color foColor = fadeColor;
            foColor.a = Mathf.Lerp(0, 1, timer / fadeDuration);
            render.material.SetColor("_Color", foColor);

            timer += Time.deltaTime;
            yield return null;
        }
        Color foColor2 = fadeColor;
        foColor2.a = Mathf.Lerp(0, 1, timer / fadeDuration);
        render.material.SetColor("_Color", foColor2);
    }
}

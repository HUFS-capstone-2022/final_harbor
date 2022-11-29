using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadFading : MonoBehaviour
{
    public float fadeDuration = 2.0f;   // Fading 시간
    public Color fadeColor; // Fade 색 지정(검정)
    private Renderer render;    // 렌더 컴포넌트 캐시 변수

    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<Renderer>();      // 렌더링 컴포넌트 저장
        // 씬 시작 시 FadeIn 코루틴 호출
        StartCoroutine(FadeIn());
    }

    void Update()
    {
        if (BadManager.Instance.makeFadeOut)
        {
            StartCoroutine(FadeOut());
            Debug.Log("Fade In Coroutine called!!");
            BadManager.Instance.makeFadeOut = false;
        }
    }

    // FadeIn 코루틴
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

        BadManager.Instance.playerMove = true;
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

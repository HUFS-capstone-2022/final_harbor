using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScript : MonoBehaviour
{
    public bool fadeOnStart = true;
    public float fadeDuration = 2.0f;
    public Color fadeColor;
    private Renderer render;

    // Start is called before the first frame update
    void Start()
    {
        // 이거 빼
        PlayerPrefs.SetInt("ending", 0);
        Debug.Log("now value of PlayerPrefs_ending = " + PlayerPrefs.GetInt("ending"));

        render = GetComponent<Renderer>();
        if (fadeOnStart)
        {
            StartCoroutine(FadeIn());
        }
    }

    // 냅둬
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
    }

    // 냅둬
    public IEnumerator FadeOut()
    {
        float timer = 0.0f;
        fadeDuration = 1.0f;
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

    // 트리거 이거 써
    private void OnTriggerEnter(Collider other)
    {
        // 각각 다른 씬(Scene) 호출

        // 상대 게임 오브젝트 태그가 TrueEnd 라면
        if (other.gameObject.name == "True")
        {
            StartCoroutine(FadeOut());
            SceneManager.LoadScene("True_End_Scene");
        }
        // 상대 게임 오브젝트 태그가 NormalEnd 라면
        else if (other.gameObject.name == "Normal")
        {
            StartCoroutine(FadeOut());
            SceneManager.LoadScene("Normal_End_Scene");
        }
        // 상대 게임 오브젝트 태그가 BadEnd 라면
        else if (other.gameObject.name == "Bad1" || other.gameObject.name == "Bad2")
        {
            StartCoroutine(FadeOut());
            SceneManager.LoadScene("Bad_End_Scene");
        }
    }
}

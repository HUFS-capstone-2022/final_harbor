using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{

    public Button GameOver;
    public GameObject OverUI;
    public CanvasGroup cg;
    // Start is called before the first frame update
    void Start()
    {
        GameOver.onClick.AddListener(() => OnButtonClick(GameOver));
    }

    public void OnButtonClick(Button button) 
    {
        if (button == GameOver)
        {
            this.StartCoroutine(this.FadeOut());
        }
    }

    private IEnumerator FadeOut()
    {   
        // Debug.LogFormat("cg.alpha {0}", cg.alpha);
        float fadeCount = 1;
        while (fadeCount > 0.0f)
        {
            // Debug.LogFormat("cg.alpha in {0}", cg.alpha);
            fadeCount -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            cg.alpha = fadeCount;
            // Debug.LogFormat("cg.alpha minus {0}", cg.alpha);
        }
        OverUI.SetActive(false);
    }

}

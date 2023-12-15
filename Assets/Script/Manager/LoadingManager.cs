using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement; 
using DG.Tweening;

public class LoadingManager : MonoBehaviour
{

    public Image loadingBar1;
    public Image loadingBar1_back;
    public Image logo;

    public Image loadingBar2;
    public Image loadingBar2_back;

    private void Awake()
    {
        loadingBar1.fillAmount = 0f;
        loadingBar1_back.DOFade(0, 0);
        logo.rectTransform.anchoredPosition = Vector2.zero;
        loadingBar1_back.gameObject.SetActive(false);

        loadingBar2.fillAmount = 0f;
        loadingBar2_back.DOFade(0, 0);
        loadingBar2_back.gameObject.SetActive(false);

        SetInitialResolution();

        // 씬 로딩 시작
        StartCoroutine(LoadScene());
    }

    void SetInitialResolution()
    {
        // 게임 시작 시 해상도 설정 (1080x1920)
        Screen.SetResolution(1080, 1920, true);
    }

    private IEnumerator LoadScene()
    {
       SceneName nextScene = GameManager.Instance.NextScene;

        // TitleScene에서 MainScene으로 전환될 때
        if (nextScene == SceneName.MainScene)
        {
            loadingBar1_back.gameObject.SetActive(true);

            yield return logo.rectTransform.DOAnchorPos(new Vector2(-560, -44), 1.5f).WaitForCompletion();

            yield return loadingBar1_back.DOFade(1, 0.5f).WaitForCompletion();

            yield return loadingBar1.DOFade(1, 0.5f).WaitForCompletion();
            yield return YieldInstructionCache.WaitForSeconds(1f);

            AsyncOperation async = SceneManager.LoadSceneAsync(GameManager.Instance.NextScene.ToString());
            async.allowSceneActivation = false;

            float timeC = 0f;

            while (!async.isDone)
            {
                yield return null;
                timeC += Time.deltaTime;

                if (async.progress >= 0.9f)
                {
                    loadingBar1.fillAmount = Mathf.Lerp(loadingBar1.fillAmount, 1f, timeC / 10f); 
                    if (loadingBar1.fillAmount >= 0.99f)
                        async.allowSceneActivation = true;
                }
                else
                {
                    loadingBar1.fillAmount = Mathf.Lerp(loadingBar1.fillAmount, async.progress, timeC);
                    if (loadingBar1.fillAmount >= async.progress)
                        timeC = 0f;
                }
            }
        }
        // MainScene에서 Game1Scene으로 전환될 때
        else if (nextScene == SceneName.GameScene1)    
        {
            // 원하는 해상도로 변경 (1920x1080)
            Screen.SetResolution(1920, 1080, true);

            loadingBar2_back.gameObject.SetActive(true);

            yield return loadingBar2_back.DOFade(1, 0.5f).WaitForCompletion();

            yield return loadingBar2.DOFade(1, 0.5f).WaitForCompletion();
            yield return YieldInstructionCache.WaitForSeconds(1f);

            AsyncOperation async = SceneManager.LoadSceneAsync(GameManager.Instance.NextScene.ToString());
            async.allowSceneActivation = false;

            float timeC = 0f;

            while (!async.isDone)
            {
                yield return null;
                timeC += Time.deltaTime;

                if (async.progress >= 0.9f)
                {
                    loadingBar2.fillAmount = Mathf.Lerp(loadingBar2.fillAmount, 1f, timeC / 10f); 
                    if (loadingBar2.fillAmount >= 0.99f)
                        async.allowSceneActivation = true;
                }
                else
                {
                    loadingBar2.fillAmount = Mathf.Lerp(loadingBar2.fillAmount, async.progress, timeC);
                    if (loadingBar2.fillAmount >= async.progress)
                        timeC = 0f;
                }
            }
        }
    }
}


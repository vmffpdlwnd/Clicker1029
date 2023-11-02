using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Image ����
using UnityEngine.SceneManagement; // �� �ε�
using DG.Tweening;
using TMPro;

public class LoadingManager : MonoBehaviour
{
    public Image loadingBar;
    public Image loadingBar_back;
    public Image logo;

    private void Awake()
    {
        loadingBar.fillAmount = 0f;
        loadingBar_back.DOFade(0, 0); // �ε��ٸ� ���� ����ϴ�.
        logo.rectTransform.anchoredPosition = Vector2.zero; // �ΰ��� ȭ�� �߾ӿ� ��ġ��ŵ�ϴ�.
        StartCoroutine("LoadAsyncScene"); // �񵿱� �ε� ó���ϴ� �ڷ�ƾ ����
    }

    IEnumerator LoadAsyncScene()
    {
        yield return logo.rectTransform.DOAnchorPos(new Vector2(-360, 41), 1.5f).WaitForCompletion(); // �ΰ��� ������ ��ġ�� 1.5�� ���� �̵���ŵ�ϴ�.

        yield return loadingBar_back.DOFade(1, 0.5f).WaitForCompletion(); // loadingBar_back�� 0.5�� ���� ������ ��Ÿ���� �մϴ�.
        yield return YieldInstructionCache.WaitForSeconds(0.5f);

        yield return loadingBar.DOFade(1, 0.5f).WaitForCompletion(); // loadingBar�� 0.5�� ���� ������ ��Ÿ���� �մϴ�.
        yield return YieldInstructionCache.WaitForSeconds(1f); // ����ũ �ε� 1��

        AsyncOperation async = SceneManager.LoadSceneAsync(GameManager.Instance.NextScene.ToString());
        async.allowSceneActivation = false;

        float timeC = 0f;

        while (!async.isDone)
        {
            yield return null;
            timeC += Time.deltaTime;

            if (async.progress >= 0.9f)
            {
                loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount, 1f, timeC / 10f); // �ε��ٰ� ������ ä�������� ����
                if (loadingBar.fillAmount >= 0.99f)
                    async.allowSceneActivation = true;
            }
            else
            {
                loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount, async.progress, timeC);
                if (loadingBar.fillAmount >= async.progress)
                    timeC = 0f;
            }
        }
    }
}


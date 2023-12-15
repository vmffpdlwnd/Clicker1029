using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class TitleManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI welcomeText;

    [SerializeField]
    private GameObject nickNamePopup; // ���� ������ �˾�

    [SerializeField]
    private TextMeshProUGUI warningText;

    private bool havePlayerInfo; // ������ �����Ǿ��ִ� ��������

    private void Awake()
    {
        Screen.SetResolution(1080, 1920, true);
        nickNamePopup.SetActive(false); // �˾��� ��Ȱ��ȭ
        warningText.gameObject.SetActive(false); // warningText�� ��Ȱ��ȭ
        InitTitleScene();
    }

    private void InitTitleScene()
    {
        if (GameManager.Instance.CheckData()) // ���������� Ȯ���ϰ�, �ε�
        {
            havePlayerInfo = true;
            welcomeText.text = GameManager.Instance.PlayerName + " �� ȯ���մϴ�.\n��ġ�� �����մϴ�.";
        }
        else
        {
            welcomeText.text = "������ �����ҷ��� ��ġ�ϼ���.";
            havePlayerInfo = false;
        }
    }

    // UI ��ư �̺�Ʈ
    public void EnterBtn()
    {
        if (havePlayerInfo) //����
        {
            GameManager.Instance.AsyncLoadNextScene(SceneName.MainScene);
        }
        else // ���� ���� ����
        {
            nickNamePopup.SetActive(true);
            nickNamePopup.transform.localScale = Vector3.zero;
            nickNamePopup.transform.DOScale(1.3f, 0.7f)
                .SetEase(Ease.InOutElastic)
                .OnComplete(() => nickNamePopup.transform.DOScale(1f, 0.3f));

        }
    }

    public void DeletBtn()
    {
        GameManager.Instance.DeleteData();
        GameManager.Instance.PlayerGold = 0; // ���� ������ �ʱ�ȭ
        InitTitleScene();
    }

    private string newNickName;

    public void InputField_Nick(string input)
    {
        newNickName = input;
    }

    public void CreateUserInfo()
    {
        if (newNickName.Length >= 2) // �α��� �̻�, ������ ó��, �ߺ��г��� üũ
        {
            nickNamePopup.transform.DOScale(0.7f, 0.3f)
            .OnComplete(() =>
             {
                 nickNamePopup.transform.DOScale(0f, 0.7f)
                 .SetEase(Ease.InOutElastic)
                 .OnComplete(() => nickNamePopup.SetActive(false));
             }); // �˾�â ����

            welcomeText.enabled = true;

            GameManager.Instance.CreateUserData(newNickName);
            GameManager.Instance.SaveData();

            InitTitleScene();
        }
        else
        {
            WarningTextActive(); // �ٽ��Է��϶�� ��� �޼��� ���
        }
    }


    Color fromColor = Color.red;
    Color toColor = Color.red;

    private void WarningTextActive()
    {
        fromColor.a = 0f;
        toColor.a = 1f;

        warningText.gameObject.SetActive(true); // warningText�� Ȱ��ȭ
        DOTween.To(() => fromColor, x => UpdateValue(x), toColor, 1f).SetEase(Ease.InOutQuad).OnStart(() => warningText.gameObject.SetActive(true));
        DOTween.To(() => toColor, x => UpdateValue(x), fromColor, 1f).SetDelay(1.5f).SetEase(Ease.InOutQuad).OnComplete(() => warningText.gameObject.SetActive(false));
    }

    private void UpdateValue(Color val)
    {
        warningText.color = val;
        warningText.gameObject.SetActive(val.a > 0); // ������ 0���� ũ�� ��� �޽��� ǥ��, �׷��� ������ ����
    }
}
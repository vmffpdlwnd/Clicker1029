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
            nickNamePopup.transform.DOScale(1f, 0.7f).SetEase(Ease.OutElastic);
            welcomeText.enabled = false;
        }
    }

    public void DeletBtn()
    {
        GameManager.Instance.DeleteData();
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
            nickNamePopup.transform.DOScale(Vector3.zero, 0.7f).SetEase(Ease.OutElastic); // �˾�â ����
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

        DOTween.To(() => fromColor, x => UpdateValue(x), toColor, 1f).SetEase(Ease.InOutQuad);
        DOTween.To(() => toColor, x => UpdateValue(x), fromColor, 1f).SetDelay(1.5f).SetEase(Ease.InOutQuad);
    }
    private void UpdateValue(Color val)
    {
        warningText.color = val;
        warningText.gameObject.SetActive(val.a > 0); // ������ 0���� ũ�� ��� �޽��� ǥ��, �׷��� ������ ����
    }
}
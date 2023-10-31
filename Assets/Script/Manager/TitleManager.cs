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
            //LeanTween.scale(nickNamePopup, Vector3.one, 0.7f).setEase(LeanTweenType.easeOutElastic);
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
            //LeanTween.scale(nickNamePopup, Vector3.zero, 0.7f).setEase(LeanTweenType.easeOutElastic); // �˾�â ����
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

        //LeanTween.value(warningText.gameObject, UpdateValue, fromColor, toColor, 1f).setEase(LeanTweenType.easeInOutQuad);
        //LeanTween.value(warningText.gameObject, UpdateValue, toColor, fromColor, 1f).setDelay(1.5f).setEase(LeanTweenType.easeInOutQuad);
    }
    private void UpdateValue(Color val)
    {
        warningText.color = val;
    }
}
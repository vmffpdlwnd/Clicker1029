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
    private GameObject nickNamePopup; // 계정 생성용 팝업

    [SerializeField]
    private TextMeshProUGUI warningText;

    private bool havePlayerInfo; // 계정이 생성되어있는 상태인지

    private void Awake()
    {
        InitTitleScene();
    }

    private void InitTitleScene()
    {
        if (GameManager.Instance.CheckData()) // 데이터파일 확인하고, 로딩
        {
            havePlayerInfo = true;
            welcomeText.text = GameManager.Instance.PlayerName + " 님 환영합니다.\n터치시 시작합니다.";
        }
        else
        {
            welcomeText.text = "계정을 생성할려면 터치하세요.";
            havePlayerInfo = false;
        }
    }

    // UI 버튼 이벤트
    public void EnterBtn()
    {
        if (havePlayerInfo) //접속
        {
            GameManager.Instance.AsyncLoadNextScene(SceneName.MainScene);
        }
        else // 계정 생성 로직
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
        if (newNickName.Length >= 2) // 두글자 이상, 금지어 처리, 중복닉네임 체크
        {
            //LeanTween.scale(nickNamePopup, Vector3.zero, 0.7f).setEase(LeanTweenType.easeOutElastic); // 팝업창 제거
            welcomeText.enabled = true;
            GameManager.Instance.CreateUserData(newNickName);
            GameManager.Instance.SaveData();
            InitTitleScene();

        }
        else
        {
            WarningTextActive(); // 다시입력하라는 경고 메세지 출력
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
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
        Screen.SetResolution(1080, 1920, true);
        nickNamePopup.SetActive(false); // 팝업을 비활성화
        warningText.gameObject.SetActive(false); // warningText를 비활성화
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
        GameManager.Instance.PlayerGold = 0; // 코인 데이터 초기화
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
            nickNamePopup.transform.DOScale(0.7f, 0.3f)
            .OnComplete(() =>
             {
                 nickNamePopup.transform.DOScale(0f, 0.7f)
                 .SetEase(Ease.InOutElastic)
                 .OnComplete(() => nickNamePopup.SetActive(false));
             }); // 팝업창 제거

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

        warningText.gameObject.SetActive(true); // warningText를 활성화
        DOTween.To(() => fromColor, x => UpdateValue(x), toColor, 1f).SetEase(Ease.InOutQuad).OnStart(() => warningText.gameObject.SetActive(true));
        DOTween.To(() => toColor, x => UpdateValue(x), fromColor, 1f).SetDelay(1.5f).SetEase(Ease.InOutQuad).OnComplete(() => warningText.gameObject.SetActive(false));
    }

    private void UpdateValue(Color val)
    {
        warningText.color = val;
        warningText.gameObject.SetActive(val.a > 0); // 투명도가 0보다 크면 경고 메시지 표시, 그렇지 않으면 숨김
    }
}
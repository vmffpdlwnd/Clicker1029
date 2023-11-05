using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Image expBar;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private Button state;
    [SerializeField] private Button shop;
    [SerializeField] private Button game;
    [SerializeField] private Button set;

    [SerializeField] private GameObject shopPopup;

    private Vector3 shopPopupOriginalPosition; // StatePopup의 원래 위치
    private bool isshopPopupActive = false; // StatePopup이 활성화되어 있는지 확인하는 변수

    [SerializeField] private GameObject statePopup; // StatePopup 객체

    private Vector3 statePopupOriginalPosition; // StatePopup의 원래 위치
    private bool isStatePopupActive = false; // StatePopup이 활성화되어 있는지 확인하는 변수


    private void Start()
    {
        UpdateUI();

        state.onClick.AddListener(OnButtonState);
        shopPopupOriginalPosition = shopPopup.transform.localPosition;

        shop.onClick.AddListener(OnButtonShop);
        statePopupOriginalPosition = statePopup.transform.localPosition;

        game.onClick.AddListener(OnButtonGame);
        set.onClick.AddListener(OnButtonSetting);


    }

    public void UpdateUI()
    {
        // EXP, Level, Gold 정보를 가져와 UI 업데이트
        int currentExp = GameManager.Instance.PlayerCurrentExp;
        int maxExp = CalculateMaxExpForLevel(GameManager.Instance.PlayerLevel);
        float fillAmount = (float)currentExp / maxExp;
        expBar.fillAmount = fillAmount;

        if (currentExp >= maxExp)
        {
            GameManager.Instance.PlayerLevel++;
            GameManager.Instance.PlayerCurrentExp -= maxExp;
            // 경험치가 넘치면 즉시 경험치 바를 리셋
            expBar.fillAmount = 0;
        }

        DOTween.To(() => expBar.fillAmount, x => expBar.fillAmount = x, expBar.fillAmount, 0.5f);

        levelText.text =  GameManager.Instance.PlayerLevel.ToString();
        goldText.text = "X " + GameManager.Instance.PlayerGold.ToString();
    }

    private int CalculateMaxExpForLevel(int level)
    {
        // 예시로 간단하게 레벨 * 100으로 계산
        return level * 100;
    }

    // 버튼 클릭 이벤트 처리
    public void OnButtonState()
    {
        Debug.Log("클릭됨");

        if (!isStatePopupActive)
        {
            // StatePopup UI를 원하는 위치로 이동
            statePopup.transform.DOLocalMove(new Vector3(0, 78, 0), 1f).SetEase(Ease.InOutElastic);
            isStatePopupActive = true;
        }
        else
        {
            // StatePopup을 원래 위치로 이동
            statePopup.transform.DOLocalMove(statePopupOriginalPosition, 1f).SetEase(Ease.InOutElastic).OnComplete(() => isStatePopupActive = false);
        }
    }

    private bool isPopupActive = false; // ShopPopup이 활성화되어 있는지 확인하는 변수
    private Vector3 popupOriginalPosition = new Vector3(0f, -1200f, 0f); // ShopPopup의 원래 위치

    public void OnButtonShop()
    {
        Debug.Log("클릭됨");

        if (!isPopupActive)
        {
            // ShopPopup UI를 원하는 위치로 이동
            shopPopup.transform.DOLocalMove(new Vector3(0, 0, shopPopup.transform.localPosition.z), 0.5f).SetEase(Ease.InOutElastic);
            isPopupActive = true;
        }
        else
        {
            // ShopPopup을 원래 위치로 이동
            shopPopup.transform.DOLocalMove(popupOriginalPosition, 0.5f).SetEase(Ease.InOutElastic).OnComplete(() => isPopupActive = false);
        }
    }

    public void OnButtonGame()
    {
        Debug.Log("클릭됨");
        // 버튼 3의 동작 처리
    }

    public void OnButtonSetting()
    {
        Debug.Log("클릭됨");
        // 버튼 4의 동작 처리
    }
}

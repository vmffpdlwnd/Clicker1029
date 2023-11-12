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

    [SerializeField] private GameObject statePopup; // StatePopup 객체

    private Vector3 statePopupOriginalPosition; // StatePopup의 원래 위치
    private bool isStatePopupActive = false; // StatePopup이 활성화되어 있는지 확인하는 변수

    [SerializeField] private GameObject shopPopup;

    private Vector3 shopPopupOriginalPosition; // StatePopup의 원래 위치
    private bool isShopPopupActive = false; // StatePopup이 활성화되어 있는지 확인하는 변수

    [SerializeField] private GameObject gamePopup;

    private Vector3 gamePopupOriginalPosition; // StatePopup의 원래 위치
    private bool isGamePopupActive = false; // StatePopup이 활성화되어 있는지 확인하는 변수

    [SerializeField] private GameObject settingPopup;

    private Vector3 settingPopupOriginalPosition; // StatePopup의 원래 위치
    private bool isSettingPopupActive = false; // StatePopup이 활성화되어 있는지 확인하는 변수


    private void Start()
    {
        UpdateUI();

        state.onClick.AddListener(OnButtonState);
        shopPopupOriginalPosition = shopPopup.transform.localPosition;

        shop.onClick.AddListener(OnButtonShop);
        statePopupOriginalPosition = statePopup.transform.localPosition;

        game.onClick.AddListener(OnButtonGame);
        gamePopupOriginalPosition = gamePopup.transform.localPosition;

        set.onClick.AddListener(OnButtonSetting);
        settingPopupOriginalPosition = settingPopup.transform.localPosition;

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
        CloseAllOtherPopups("state");

        if (!isStatePopupActive)
        {
            // StatePopup UI를 원하는 위치로 이동
            statePopup.transform.DOLocalMove(new Vector3(0, 0, 0), 1f).SetEase(Ease.InOutElastic).SetUpdate(UpdateType.Normal);
            isStatePopupActive = true;
            GameManager.Instance.isPaused = true; // 팝업을 열면 게임을 일시 정지 상태로 변경
        }
        else
        {
            // StatePopup을 원래 위치로 이동
            statePopup.transform.DOLocalMove(statePopupOriginalPosition, 1f).SetEase(Ease.InOutElastic).SetUpdate(UpdateType.Normal).OnComplete(() =>
            {
                isStatePopupActive = false;
                GameManager.Instance.isPaused = false;
            });
        }
    }

    public void OnButtonShop()
    {
        CloseAllOtherPopups("shop");
        if (!isShopPopupActive)
        {
            // ShopPopup UI를 원하는 위치로 이동
            shopPopup.transform.DOLocalMove(new Vector3(0, -260, 0), 1f).SetEase(Ease.InOutElastic).SetUpdate(UpdateType.Normal);
            isShopPopupActive = true;
            GameManager.Instance.isPaused = true;
        }
        else
        {
            // ShopPopup을 원래 위치로 이동
            shopPopup.transform.DOLocalMove(shopPopupOriginalPosition, 1f).SetEase(Ease.InOutElastic).SetUpdate(UpdateType.Normal).OnComplete(() =>
            {
                isShopPopupActive = false;
                GameManager.Instance.isPaused = false;
            });
        }
    }

    public void OnButtonGame()
    {
        CloseAllOtherPopups("game");
        if (!isGamePopupActive)
        {
            // GamePopup UI를 원하는 위치로 이동
            gamePopup.transform.DOLocalMove(new Vector3(0, 0, 0), 1f).SetEase(Ease.InOutElastic).SetUpdate(UpdateType.Normal);
            isGamePopupActive = true;
            GameManager.Instance.isPaused = true;
        }
        else
        {
            // GamePopup을 원래 위치로 이동
            gamePopup.transform.DOLocalMove(gamePopupOriginalPosition, 1f).SetEase(Ease.InOutElastic).SetUpdate(UpdateType.Normal).OnComplete(() =>
            {     
                isGamePopupActive = false;
                GameManager.Instance.isPaused = false;
            });
        }
    }

    public void OnButtonSetting()
    {
        CloseAllOtherPopups("setting");
        if (!isSettingPopupActive)
        {
            // SettingPopup UI를 원하는 위치로 이동
            settingPopup.transform.DOLocalMove(new Vector3(0, -260, 0), 1f).SetEase(Ease.InOutElastic).SetUpdate(UpdateType.Normal);
            isSettingPopupActive = true;
            GameManager.Instance.isPaused = true;
        }
        else
        {
            // SettingPopup을 원래 위치로 이동
            settingPopup.transform.DOLocalMove(settingPopupOriginalPosition, 1f).SetEase(Ease.InOutElastic).SetUpdate(UpdateType.Normal).OnComplete(() =>
            { 
                isSettingPopupActive = false;
                GameManager.Instance.isPaused = false;
            });
    }
    }
    private void CloseAllOtherPopups(string currentPopup)
    {
        if (currentPopup != "state" && isStatePopupActive)
        {
            statePopup.transform.DOLocalMove(statePopupOriginalPosition, 1f).SetEase(Ease.InOutElastic).SetUpdate(UpdateType.Normal).OnComplete(() => isStatePopupActive = false);
            GameManager.Instance.isPaused = false;
        }
        if (currentPopup != "shop" && isShopPopupActive)
        {
            shopPopup.transform.DOLocalMove(shopPopupOriginalPosition, 1f).SetEase(Ease.InOutElastic).SetUpdate(UpdateType.Normal).OnComplete(() => isShopPopupActive = false);
            GameManager.Instance.isPaused = false;
        }
        if (currentPopup != "game" && isGamePopupActive)
        {
            gamePopup.transform.DOLocalMove(gamePopupOriginalPosition, 1f).SetEase(Ease.InOutElastic).SetUpdate(UpdateType.Normal).OnComplete(() => isGamePopupActive = false);
            GameManager.Instance.isPaused = false;
        }
        if (currentPopup != "setting" && isSettingPopupActive)
        {
            settingPopup.transform.DOLocalMove(settingPopupOriginalPosition, 1f).SetEase(Ease.InOutElastic).SetUpdate(UpdateType.Normal).OnComplete(() => isSettingPopupActive = false);
            GameManager.Instance.isPaused = false;
        }
    }
}

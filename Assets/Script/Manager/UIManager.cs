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

    [SerializeField] private GameObject statePopup; // StatePopup ��ü

    private Vector3 statePopupOriginalPosition; // StatePopup�� ���� ��ġ
    private bool isStatePopupActive = false; // StatePopup�� Ȱ��ȭ�Ǿ� �ִ��� Ȯ���ϴ� ����

    [SerializeField] private GameObject shopPopup;

    private Vector3 shopPopupOriginalPosition; // StatePopup�� ���� ��ġ
    private bool isShopPopupActive = false; // StatePopup�� Ȱ��ȭ�Ǿ� �ִ��� Ȯ���ϴ� ����

    [SerializeField] private GameObject gamePopup;

    private Vector3 gamePopupOriginalPosition; // StatePopup�� ���� ��ġ
    private bool isGamePopupActive = false; // StatePopup�� Ȱ��ȭ�Ǿ� �ִ��� Ȯ���ϴ� ����

    [SerializeField] private GameObject settingPopup;

    private Vector3 settingPopupOriginalPosition; // StatePopup�� ���� ��ġ
    private bool isSettingPopupActive = false; // StatePopup�� Ȱ��ȭ�Ǿ� �ִ��� Ȯ���ϴ� ����


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
        // EXP, Level, Gold ������ ������ UI ������Ʈ
        int currentExp = GameManager.Instance.PlayerCurrentExp;
        int maxExp = CalculateMaxExpForLevel(GameManager.Instance.PlayerLevel);
        float fillAmount = (float)currentExp / maxExp;
        expBar.fillAmount = fillAmount;

        if (currentExp >= maxExp)
        {
            GameManager.Instance.PlayerLevel++;
            GameManager.Instance.PlayerCurrentExp -= maxExp;
            // ����ġ�� ��ġ�� ��� ����ġ �ٸ� ����
            expBar.fillAmount = 0;
        }

        DOTween.To(() => expBar.fillAmount, x => expBar.fillAmount = x, expBar.fillAmount, 0.5f);

        levelText.text =  GameManager.Instance.PlayerLevel.ToString();
        goldText.text = "X " + GameManager.Instance.PlayerGold.ToString();
    }

    private int CalculateMaxExpForLevel(int level)
    {
        // ���÷� �����ϰ� ���� * 100���� ���
        return level * 100;
    }

    // ��ư Ŭ�� �̺�Ʈ ó��
    public void OnButtonState()
    {
        CloseAllOtherPopups("state");

        if (!isStatePopupActive)
        {
            // StatePopup UI�� ���ϴ� ��ġ�� �̵�
            statePopup.transform.DOLocalMove(new Vector3(0, 0, 0), 1f).SetEase(Ease.InOutElastic).SetUpdate(UpdateType.Normal);
            isStatePopupActive = true;
            GameManager.Instance.isPaused = true; // �˾��� ���� ������ �Ͻ� ���� ���·� ����
        }
        else
        {
            // StatePopup�� ���� ��ġ�� �̵�
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
            // ShopPopup UI�� ���ϴ� ��ġ�� �̵�
            shopPopup.transform.DOLocalMove(new Vector3(0, -260, 0), 1f).SetEase(Ease.InOutElastic).SetUpdate(UpdateType.Normal);
            isShopPopupActive = true;
            GameManager.Instance.isPaused = true;
        }
        else
        {
            // ShopPopup�� ���� ��ġ�� �̵�
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
            // GamePopup UI�� ���ϴ� ��ġ�� �̵�
            gamePopup.transform.DOLocalMove(new Vector3(0, 0, 0), 1f).SetEase(Ease.InOutElastic).SetUpdate(UpdateType.Normal);
            isGamePopupActive = true;
            GameManager.Instance.isPaused = true;
        }
        else
        {
            // GamePopup�� ���� ��ġ�� �̵�
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
            // SettingPopup UI�� ���ϴ� ��ġ�� �̵�
            settingPopup.transform.DOLocalMove(new Vector3(0, -260, 0), 1f).SetEase(Ease.InOutElastic).SetUpdate(UpdateType.Normal);
            isSettingPopupActive = true;
            GameManager.Instance.isPaused = true;
        }
        else
        {
            // SettingPopup�� ���� ��ġ�� �̵�
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

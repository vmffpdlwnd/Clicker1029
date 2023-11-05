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

    private Vector3 shopPopupOriginalPosition; // StatePopup�� ���� ��ġ
    private bool isshopPopupActive = false; // StatePopup�� Ȱ��ȭ�Ǿ� �ִ��� Ȯ���ϴ� ����

    [SerializeField] private GameObject statePopup; // StatePopup ��ü

    private Vector3 statePopupOriginalPosition; // StatePopup�� ���� ��ġ
    private bool isStatePopupActive = false; // StatePopup�� Ȱ��ȭ�Ǿ� �ִ��� Ȯ���ϴ� ����


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
        Debug.Log("Ŭ����");

        if (!isStatePopupActive)
        {
            // StatePopup UI�� ���ϴ� ��ġ�� �̵�
            statePopup.transform.DOLocalMove(new Vector3(0, 78, 0), 1f).SetEase(Ease.InOutElastic);
            isStatePopupActive = true;
        }
        else
        {
            // StatePopup�� ���� ��ġ�� �̵�
            statePopup.transform.DOLocalMove(statePopupOriginalPosition, 1f).SetEase(Ease.InOutElastic).OnComplete(() => isStatePopupActive = false);
        }
    }

    private bool isPopupActive = false; // ShopPopup�� Ȱ��ȭ�Ǿ� �ִ��� Ȯ���ϴ� ����
    private Vector3 popupOriginalPosition = new Vector3(0f, -1200f, 0f); // ShopPopup�� ���� ��ġ

    public void OnButtonShop()
    {
        Debug.Log("Ŭ����");

        if (!isPopupActive)
        {
            // ShopPopup UI�� ���ϴ� ��ġ�� �̵�
            shopPopup.transform.DOLocalMove(new Vector3(0, 0, shopPopup.transform.localPosition.z), 0.5f).SetEase(Ease.InOutElastic);
            isPopupActive = true;
        }
        else
        {
            // ShopPopup�� ���� ��ġ�� �̵�
            shopPopup.transform.DOLocalMove(popupOriginalPosition, 0.5f).SetEase(Ease.InOutElastic).OnComplete(() => isPopupActive = false);
        }
    }

    public void OnButtonGame()
    {
        Debug.Log("Ŭ����");
        // ��ư 3�� ���� ó��
    }

    public void OnButtonSetting()
    {
        Debug.Log("Ŭ����");
        // ��ư 4�� ���� ó��
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Image expBar;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField]
    private Button home;
    [SerializeField]
    private Button Shop;
    [SerializeField]
    private Button Gaming;
    [SerializeField]
    private Button Setting;
    [SerializeField] private GameObject shopPopup;

    private void Awake()
    {
        home.onClick.AddListener(OnButtonHome);
        Shop.onClick.AddListener(OnButtonShop);
        Gaming.onClick.AddListener(OnButtonGame);
        Setting.onClick.AddListener(OnButtonSetting);
    }

    private void Start()
    {
        UpdateUI();
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
    public void OnButtonHome()
    {
        Debug.Log("��ư�� Ŭ���Ǿ����ϴ�.");
        GameManager.Instance.AsyncLoadNextScene(SceneName.MainScene);
    }

    public void OnButtonShop()
    {
        Debug.Log("��ư�� Ŭ���Ǿ����ϴ�.");
        // MidPopup�� ShopPopup Ȱ��ȭ
        shopPopup.SetActive(true);

        // MidPopup �ִϸ��̼� ����
        RectTransform shopPopupRect = shopPopup.GetComponent<RectTransform>();
        shopPopupRect.DOAnchorPosY(200f, 1f).SetEase(Ease.OutElastic);
    }


    public void OnButtonGame()
    {
        Debug.Log("��ư�� Ŭ���Ǿ����ϴ�.");
        // ��ư 3�� ���� ó��
    }

    public void OnButtonSetting()
    {
        Debug.Log("��ư�� Ŭ���Ǿ����ϴ�.");
        // ��ư 4�� ���� ó��
    }
}

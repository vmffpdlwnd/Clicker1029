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
    public void OnButtonHome()
    {
        Debug.Log("버튼이 클릭되었습니다.");
        GameManager.Instance.AsyncLoadNextScene(SceneName.MainScene);
    }

    public void OnButtonShop()
    {
        Debug.Log("버튼이 클릭되었습니다.");
        // MidPopup과 ShopPopup 활성화
        shopPopup.SetActive(true);

        // MidPopup 애니메이션 적용
        RectTransform shopPopupRect = shopPopup.GetComponent<RectTransform>();
        shopPopupRect.DOAnchorPosY(200f, 1f).SetEase(Ease.OutElastic);
    }


    public void OnButtonGame()
    {
        Debug.Log("버튼이 클릭되었습니다.");
        // 버튼 3의 동작 처리
    }

    public void OnButtonSetting()
    {
        Debug.Log("버튼이 클릭되었습니다.");
        // 버튼 4의 동작 처리
    }
}

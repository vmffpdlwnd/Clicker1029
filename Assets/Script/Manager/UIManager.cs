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
    [SerializeField] private Button[] buttons;

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
    public void IncreaseGoldOnClick()
    {
        Debug.Log("클릭됨");
        // 버튼을 클릭할 때마다 골드를 10 증가시킵니다.
        GameManager.Instance.PlayerGold += 10;
        UpdateUI();
    }
    // 버튼 클릭 이벤트 처리
    public void OnButton1Click()
    {
        // 버튼 1의 동작 처리
    }

    public void OnButton2Click()
    {
        // 버튼 2의 동작 처리
    }

    public void OnButton3Click()
    {
        // 버튼 3의 동작 처리
    }

    public void OnButton4Click()
    {
        // 버튼 4의 동작 처리
    }
}

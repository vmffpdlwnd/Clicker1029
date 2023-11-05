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
    public void IncreaseGoldOnClick()
    {
        Debug.Log("Ŭ����");
        // ��ư�� Ŭ���� ������ ��带 10 ������ŵ�ϴ�.
        GameManager.Instance.PlayerGold += 10;
        UpdateUI();
    }
    // ��ư Ŭ�� �̺�Ʈ ó��
    public void OnButton1Click()
    {
        // ��ư 1�� ���� ó��
    }

    public void OnButton2Click()
    {
        // ��ư 2�� ���� ó��
    }

    public void OnButton3Click()
    {
        // ��ư 3�� ���� ó��
    }

    public void OnButton4Click()
    {
        // ��ư 4�� ���� ó��
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// ��ü���� ���α׷��� - �پ缺(���������Լ� Ȥ�� �������̽�)�� ���� �ڵ�
public interface IBaseTownPopup
{
    public void PopupOpen();
    public void PopupClose();
}


public class Popup_Base : MonoBehaviour
{
    public void PopupOpen()
    {
        transform.DOScale(Vector3.one, 0.7f).SetEase(Ease.OutElastic);
    }
    public void PopupClose()
    {
        transform.DOScale(Vector3.zero, 0.7f).SetEase(Ease.OutElastic);
    }
}

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


    private void Awake()
    {
        gameObject.transform.DOScale(Vector3.zero, 0.01f); // ���� �����ϸ� �⺻������ �˾��� �������·� ����
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePopup : MonoBehaviour
{
    public void Game1Btn()
    {
        Screen.SetResolution(1920, 1080, true);
        Canvas[] allCanvases = FindObjectsOfType<Canvas>();
        foreach (Canvas canvas in allCanvases)
        {
            canvas.enabled = false;
        }
        GameManager.Instance.AsyncLoadNextScene(SceneName.GameScene1);
    }
}
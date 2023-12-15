using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePopup : MonoBehaviour
{
    public void Game1Btn()
    {
        GameManager.Instance.AsyncLoadNextScene(SceneName.GameScene1);
    }
}


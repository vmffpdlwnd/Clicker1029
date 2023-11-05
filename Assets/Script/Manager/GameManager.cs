using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public enum SceneName
{
    TitleScene,
    MainScene,
    GameScene1
}

public enum Save_Date
{
    save_NickName,
    save_SceneName,
    save_Coin_BGM, // 코인의 볼륨 크기를 저장.(옵션)
    save_BGM, // 배경음의 볼륨 크기를 저장.
    save_Level, // 플레이어 레벨
    save_CPU_level,
    save_Exp, // 경험치
    save_Gold
    
}

public class PlayerData
{
    public string userNickName; 
    public int level;
    public int curExp;
    public int gold;
    public int CPU_level;
}

public class GameManager : Singleton<GameManager>
{
    private PlayerData pData = new PlayerData();
    public PlayerData PlayerInfo
    {
        get => pData;
    }

    private SceneName nextScene;
    public SceneName NextScene
    {
        get => nextScene;
    }

    private new void Awake()
    {
        base.Awake(); // 부모의 Awake 호출
        dataPath = Application.persistentDataPath + "/save";
        CheckData();
        SceneManager.sceneLoaded += OnSceneLoaded; // 이벤트에 메서드 등록
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // 이벤트에서 메서드 제거
    }

    public void AsyncLoadNextScene(SceneName newScene)
    {
        SaveData();
        nextScene = newScene;
        SceneManager.LoadScene("LoadingScene");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) // 새로운 씬이 로딩이 완료가 되면 호출되는 이벤트
    {
        if (scene.buildIndex > 2)
        {
            Debug.Log("캐릭터 세이브 데이터 로딩");
            LoadData();
        }
    }

    #region _Save&Load_

    private string dataPath; //= Application.persistentDataPath + "/save"; // 디바이스 정해진 폴더에 save폴더 추가

    public void SaveData()
    {
        string data = JsonUtility.ToJson(pData); // 객체정보를 string타입으로 변경 ( Json 구조를 활용 )
        File.WriteAllText(dataPath, data);
    }

    public bool LoadData()
    {
        if (File.Exists(dataPath))
        {
            string data = File.ReadAllText(dataPath);
            pData = JsonUtility.FromJson<PlayerData>(data);
            return true;
        }
        return false;
    }

    public void DeleteData()
    {
        File.Delete(dataPath);
    }
    public bool CheckData()
    {
        if (File.Exists(dataPath))
        {
            return LoadData();
        }
        return false;
    }

    #endregion

    #region _ UserData_

    public void CreateUserData(string newNickName) // 처음 계정 생성할때 초기 데이터 세팅
    {
        pData.userNickName = newNickName;
        pData.curExp = 0;
        pData.level = 1;
        pData.CPU_level = 1;

    }

    public string PlayerName
    {
        get => pData.userNickName;
    }
    public int PlayerLevel
    {
        get => pData.level;
        set => pData.level = value;
    }
    public int PlayerCurrentExp
    {
        get => pData.curExp;
        set => pData.curExp = value;
    }
    public int PlayerCurrentCPU_level
    {
        get => pData.CPU_level;
    }

    public Text expText;

    public void AddExp(int addEXP)
    {
        pData.curExp += addEXP;
        UIManager.Instance.UpdateUI();
    }
    public int PlayerGold
    {
        get => pData.gold;
        set => pData.gold = value;
    }

    public Text goldText;

    public void AddGold(int addGold)
    {
        pData.gold += addGold * pData.level;
        UIManager.Instance.UpdateUI();
    }

    #endregion
}

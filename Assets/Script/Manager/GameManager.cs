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

[System.Serializable]
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
    private ClickerGame table;

    private PlayerData pData = new PlayerData();

    public bool isPaused = false;
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

        #region _TableData_
        table = Resources.Load<ClickerGame>("ClickerGame"); // 런타임중에 에셋폴더(Resources)에 접근해서 에셋을 동적으로 로딩.
        // 아이템 테이블 딕셔너리로 정리
        for (int i=0; i<)
      
        #endregion
        CheckData();
        SceneManager.sceneLoaded += OnSceneLoaded; // 이벤트에 메서드 등록
    }

    private void Update()
    {
        if (Time.time - lastSaveTime >= autoSaveInterval)
        {
            Debug.Log("Save Data!");
            SaveData();
            lastSaveTime = Time.time;
        }
        if (GameManager.Instance.isPaused)
        {
            return; // 게임이 일시정지 상태라면 Update를 빠져나감
        }

        // 게임이 일시정지 상태가 아니라면 정상적으로 Update 실행
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // 이벤트에서 메서드 제거
    }

    public void AsyncLoadNextScene(SceneName newScene)
    {
        SaveData();
        nextScene = newScene;
        isPaused = true; // 새로운 씬을 로드하는 동안 게임을 일시 정지 상태로 변경
        SceneManager.LoadScene("LoadingScene");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) // 새로운 씬이 로딩이 완료가 되면 호출되는 이벤트
    {
        if (scene.buildIndex > 2)
        {
            Debug.Log("캐릭터 세이브 데이터 로딩");
            LoadData();
        }
        isPaused = false; // 씬 로딩이 완료되면 게임을 일시 정지 상태에서 해제
    }

    #region _Save&Load_

    private string dataPath; //= Application.persistentDataPath + "/save"; // 디바이스 정해진 폴더에 save폴더 추가
    private float autoSaveInterval = 60f; // 자동 저장 간격 (초 단위)
    private float lastSaveTime = 0f; // 마지막 저장 시간

  

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
        pData.gold = 0; // 코인 데이터 초기화
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
        set => pData.CPU_level = value;
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

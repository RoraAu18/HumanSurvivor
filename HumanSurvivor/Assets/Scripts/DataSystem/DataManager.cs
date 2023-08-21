using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DataManager : MonoBehaviour
{
    public List<IGameDataStorer> gameDataStorers = new List<IGameDataStorer>();
    public List<Guid> uniqueDataUsers = new List<Guid>();
    //save the end game data scores best scores and display them

    void Start()
    {

    }

    public void AddUser(IGameDataStorer gameDataStorer, DataRef dataRef)
    {
        gameDataStorers.Add(gameDataStorer);
        if (dataRef.inUse) return;
        
        dataRef.uniqueId = UniqueIDGenerator();
        dataRef.inUse = true;
    }
    public void RemoveUser(IGameDataStorer gameDataStorer)
    {
        gameDataStorers.Remove(gameDataStorer);
    }
    
    public void SaveAllData()
    {
        for (int i = 0; i < gameDataStorers.Count; i++)
        {
             SaveData(gameDataStorers[i].StoreData());
        }
    }    
    public void GetAllData()
    {
        for (int i = 0; i < gameDataStorers.Count; i++)
        {
            gameDataStorers[i].RestoreData(GetData(gameDataStorers[i].id));
        }
    }

    public void SaveData(GameData data)
    {
        var jsonData = JsonUtility.ToJson(data);

        PlayerPrefs.SetString(data.id.uniqueId, jsonData);
    }
    public GameData GetData(GameData data)
    {
        var jsonData = PlayerPrefs.GetString(data.id.uniqueId);
        GameData fileData = JsonUtility.FromJson<GameData>(jsonData);
        Debug.Log("restoring" + jsonData + " AND " + data.id.uniqueId);
        if(data is BestMatchsGameData bestmatch)
        {
            fileData = bestmatch;
            Debug.Log(fileData + " " + bestmatch);
        }
        return fileData;
    }

    public string UniqueIDGenerator()
    {
        return Guid.NewGuid().ToString();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            SaveAllData();
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            GetAllData();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            for (int i = 0; i < gameDataStorers.Count; i++)
            {
                Debug.Log(gameDataStorers[i].StoreData());
            }
        }
    }
}

[Serializable]
public class GameData
{
    public DataRef id;
    public string description;
    //constructor 
    public GameData()
    {
        //id = Guid.NewGuid();
    }
    public GameData(Guid oldId)
    {
        //id = oldId;
    }
}

[Serializable]
public class BestMatchsGameData : GameData
{
    public bool allItemsCollected;
    public bool onTime;
    public bool wasNotDetected;
    public float bestTime;
}
public interface IGameDataStorer
{
    public GameData id { get; }
    public DataManager dataMan { get; }
    GameData StoreData();
    void RestoreData(GameData restoreData);
}


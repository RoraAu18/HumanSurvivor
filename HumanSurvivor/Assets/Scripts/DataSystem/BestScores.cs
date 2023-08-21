using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestScores : MonoBehaviour, IGameDataStorer
{
    [SerializeField]
    BestMatchsGameData scores;
    public DataManager dataMan => GameManager.OnlyInstance.dataManager;

    public GameData id => scores;

    public void RestoreData(GameData restoreData)
    {
        //in the conversion, the data is getting modified, the restored data should be applied, otherwise it womr work
        if(restoreData is BestMatchsGameData bestData)
        {
            Debug.Log("getting new best data + " + bestData.id+ " " + scores.allItemsCollected);
            scores = bestData;
        }
    }

    public GameData StoreData()
    {
        var currScore = new BestMatchsGameData();
        currScore = scores;
        //getbestscores isnt working for any of the store datas so far
        if (GameManager.OnlyInstance.GetBestScore(scores, currScore, out BestMatchsGameData best))
        {
            scores = best;
        }
        return scores;
    }



    // Start is called before the first frame update
    void Start()
    {
        dataMan.AddUser(this, id.id);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    
}

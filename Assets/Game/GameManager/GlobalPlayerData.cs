using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalPlayerData : MonoBehaviour
{
    static private GlobalPlayerData instance;

    static public GlobalPlayerData Instance
    {
        get 
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<GlobalPlayerData>();
            }

            return instance;
        }
    }

    public string Name;
    public int Stage;
    public int Score;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);    
    }

    public void AddScore(int score)
    {
        this.Score += score;
    }

    public void ResetData()
    {
        this.Name = string.Empty;
        this.Stage = 0;
        this.Score = 0;
    }
}

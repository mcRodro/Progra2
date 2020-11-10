using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnteringName : MonoBehaviour
{
    public InputField inputField;
    private Database db;

    void Start()
    {
        db = new Database();
    }

    public void ActionConfirmName()
    {
        GlobalPlayerData.Instance.Name = inputField.text;

        var ranking = new RankingModel(inputField.text, GlobalPlayerData.Instance.Stage, GlobalPlayerData.Instance.Score);
        db.AddRankingRecord(ranking);
        SceneManager.LoadScene("Ranking");
    }

    public void ActionBack()
    {
        SceneManager.LoadScene("Menu");
    }
}

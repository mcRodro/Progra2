using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RankingManager : MonoBehaviour
{
    public GameObject prifab;
    public Transform Grid;
    public List<RankingModel> rankingData;
    private Database db;

    private void Start()
    {
        db = new Database();
        var quickSort = new QuicksortTDA();

        //rankingData = new List<RankingModel>();
        //for (int i = 0; i < 50; i++)
        //{
        //    var model = new RankingModel("Aurora", Random.Range(0, 10), Random.Range(0, 1000000));
        //    rankingData.Add(model);
        //    db.AddRankingRecord(model);
        //}

        Debug.Log("Inicio Programa: Quick Sort");
        var rankingRecords = db.GetAllRankingRecords();
        quickSort.quickSort(rankingRecords, 0, rankingRecords.Count - 1);

        Debug.Log("\nLista Ordenada: ");
        quickSort.imprimirVector(rankingRecords);

        for (int i = 0; i < rankingRecords.Count; i++)
        {
            var slot = Instantiate(prifab, Grid);
            var model = rankingRecords[i];
            slot.GetComponent<RankingModel>().SetTexts(i, model.NameValue, model.StageValue, model.ScoreValue);
        }
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}

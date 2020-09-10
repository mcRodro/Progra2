using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RankingManager : MonoBehaviour
{
    public GameObject prifab;
    public Transform Grid;
    public List<RankingModel> rankingData;

    private void Start()
    {
        var quickSort = new QuicksortTDA();

        rankingData = new List<RankingModel>();
        for (int i = 0; i < 50; i++)
        {
            var model = new RankingModel("Aurora", Random.Range(0, 10), Random.Range(0, 1000000));
            rankingData.Add(model);
        }

        Debug.Log("Inicio Programa: Quick Sort");
        quickSort.quickSort(rankingData, 0, rankingData.Count - 1);

        Debug.Log("\nLista Ordenada: ");
        quickSort.imprimirVector(rankingData);

        for (int i = 0; i < rankingData.Count; i++)
        {
            var slot = Instantiate(prifab, Grid);
            var model = rankingData[i];
            slot.GetComponent<RankingModel>().SetTexts(i, model.NameValue, model.StageValue, model.ScoreValue);
        }
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}

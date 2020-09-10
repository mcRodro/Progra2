using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingModel : MonoBehaviour
{
    public Text Name;
    public Text Stage;
    public Text Score;
    public Text Position;

    public string NameValue { get; set; }
    public int StageValue { get; set; }
    public int ScoreValue { get; set; }
    public int PositionValue { get; set; }

    public RankingModel(string name, int stage, int score)
    {
        this.NameValue = name;
        this.StageValue = stage;
        this.ScoreValue = score;
    }

    public void SetTexts(int position, string name, int stage, int score)
    {
        this.Name.text = name;
        this.Stage.text = stage.ToString();
        this.Score.text = score.ToString();
        this.Position.text = (position+1).ToString();
    }
}

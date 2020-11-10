using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIiData : MonoBehaviour
{
    public Text StageInfo;
    public Text WinLooseInfo;
    public Text CurrentKllCount;
    public Text MaxKillCount;
    public Image KillProgressBar;
    public Image GamePause;

    private void Awake()
    {
        this.StageInfo.gameObject.SetActive(false);
        this.WinLooseInfo.gameObject.SetActive(false);
        this.GamePause.gameObject.SetActive(false);
    }

    public void UpdateMaxKillCount(int maxKillCount)
    {
        this.MaxKillCount.text = maxKillCount.ToString();
    }

    public void UpdateKillProgress(int currentKillCount, int maxKill)
    {
        float max = maxKill * .1f;
        this.CurrentKllCount.text = string.Format("{0} de {1}", currentKillCount, maxKill);
        this.KillProgressBar.fillAmount = currentKillCount / max;
    }

    public void UpdateStageInfo(int stage)
    {
        this.StageInfo.text = string.Format("!STAGE {0}!", stage);
    }

    public void UpdateWinLooseInfo(bool isWinner)
    {
        this.WinLooseInfo.text = isWinner ? "VICTORIA" : "DERROTA";
    }
}

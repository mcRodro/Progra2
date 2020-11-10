using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    readonly int[] EnemiesObjectivePerStage = { 50, 100, 150, 300, 400, 500 };

    static public GameManager instance;

    public int CurrentStage { get; set; }
    public int EnemiesKillCount { get; set; }
    
    public bool IsGameOver;

    public bool GamePause;
    private GameObject MainBase { get; set; }
    
    public int GameState;

    void Awake()
    {
        instance = this;

        this.MainBase = GameObject.FindGameObjectWithTag("MainBase");

        this.CurrentStage = 1;
        this.EnemiesKillCount = 0;
        this.IsGameOver = false;
        this.GamePause = true;
        this.GameState = 0;

        this.GetComponent<GameUIiData>().UpdateKillProgress(EnemiesKillCount, EnemiesObjectivePerStage[CurrentStage]);
    }

    void Start()
    {
        
    }

    void Update()
    { 
        GameStateMachine();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            this.GamePause = !this.GamePause;
            this.GetComponent<GameUIiData>().GamePause.gameObject.SetActive(this.GamePause);
        }
    }

    public bool IsLooser()
    {
        if (!this.MainBase)
        {
            this.IsGameOver = true;
            this.GameState = 3;
            return true;
        }

        return false;
    }

    public bool IsWinner()
    {
        if (this.EnemiesKillCount >= this.EnemiesObjectivePerStage[CurrentStage] && AllEnemyIsKilled())
        {
            this.IsGameOver = true;
            this.GameState = 3;
            return true;
        }

        return false;
    }

    public bool AllEnemyIsKilled()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            return true;
        }

        return false;
    }

    private void GameStateMachine()
    {
        switch (GameState)
        {
            /* preGame - Inform Stage */
            case 0:
                Invoke("ShowStage", 1);
                Invoke("HideStage", 6);
                break;
            /* preGame - Inform countdown to first enemy spown */
            case 1:
                this.GamePause = false;
                this.GameState = 2;
                break;
            /* inGame - Inform kill progress */
            case 2:
                this.IsLooser();
                this.IsWinner();
                this.GetComponent<GameUIiData>().UpdateKillProgress(EnemiesKillCount, EnemiesObjectivePerStage[CurrentStage]);
                break;
            /* postGame - When gameover inform result */
            case 3:
                Invoke("ShowWinLoose", 1);
                Invoke("HideWinLoose", 10);
                break;
            /* postGame - Load ranking scene */
            case 4:
                GlobalPlayerData.Instance.Stage = this.CurrentStage;
                SceneManager.LoadScene("CargaNombre");
                break;
            /* Default */
            default: break;
        }
    }

    public void AddEnemyKillToCount()
    {
        this.EnemiesKillCount++;
    }

    private void ChangeCurrentStage(int nextStage)
    {
        this.CurrentStage = nextStage;
    }

    private void ShowStage()
    {
        this.GetComponent<GameUIiData>().UpdateStageInfo(this.CurrentStage);
        this.GetComponent<GameUIiData>().StageInfo.gameObject.SetActive(true);
    }
    
    private void HideStage()
    {
        this.GetComponent<GameUIiData>().StageInfo.gameObject.SetActive(false);
        this.GameState = 1;
    }

    private void ShowWinLoose()
    {
        this.GetComponent<GameUIiData>().WinLooseInfo.gameObject.SetActive(true);
    }

    private void HideWinLoose()
    {
        this.GetComponent<GameUIiData>().WinLooseInfo.gameObject.SetActive(false);
        this.GameState = 4;
    }
}

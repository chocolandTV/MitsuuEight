using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class LapManager : MonoBehaviour
{
    [SerializeField] private int Lap_Rounds = 3;
    private const int Lap_Checkpoints = 7;
    private List<bool> checkPointsDone = new List<bool>();
    private List<Vector3> GhostData = new List<Vector3>();
    [SerializeField] private int Lap_Current = 1;
    public bool stageStarts = false;
    int _ChecksMissing = 0;
    public static LapManager Instance;
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        stageStarts = false;
        for (int i = 0; i < Lap_Checkpoints; i++)
        {
            checkPointsDone.Add(false);
        }
        //HUD_Manager.Instance.UpdateLapRound(Lap_Current);

    }
    void Start()
    {
        // START ANIMATION TO SELECTED CAR
        // START TIMER 3,2,1
        StartStage();
    }
    public int GetLapRounds()
    {
        return Lap_Rounds;
    }

    public void StartStage()
    {

        stageStarts = true;
        HUD_Manager.StageStartTime = Time.timeSinceLevelLoad;
        HUD_Manager.Instance.SetTimerText(true); 
        HUD_Manager.Instance.ResetTimeLapRoundsText();
        HUD_Manager.Instance.UpdateCollectables(CollectionManager.Instance.GetCoins(GameManager.Game_stageIndex));
    }
    public void CheckNextRound(Vector3 pos)
    {
        if (Lap_Current >= Lap_Rounds)
        {
            HUD_Manager.Instance.UpdateTimeLapRoundText(Lap_Current, Time.timeSinceLevelLoad - HUD_Manager.StageStartTime);
            StageWin();
            return;
        }
        if (CheckLapProgress())
        {
            GhostData.Add(pos);
            NextRound();
        }

    }
    public void CheckpointSet(int _checkpoint_id, Vector3 pos)
    {

        if (checkPointsDone[_checkpoint_id] == false)
        {
            checkPointsDone[_checkpoint_id] = true;
            Debug.Log("Checkpoint: New Checkpoint ID:" + _checkpoint_id);
            GhostData.Add(pos);
        }
        else
        {
            Debug.Log("Checkpoint: Allready Checked");

        }

    }
    private void NextRound()
    {
        HUD_Manager.Instance.UpdateTimeLapRoundText(Lap_Current, Time.timeSinceLevelLoad - HUD_Manager.StageStartTime);
        Debug.Log("Next Round");
        Lap_Current++;
        HUD_Manager.Instance.UpdateLapRound(Lap_Current);
        ResetCheckPoints();
    }
    private void StageWin()
    {
        Debug.Log("Win Stage");
        HUD_Manager.Instance.SetTimerText(false);

        // CAR CONTROLL OFF
        // SAFE GHOST POSITIONS
        // END SCREEN

    }
    private void ResetCheckPoints()
    {
        for (int i = 0; i < checkPointsDone.Count; i++)
        {
            checkPointsDone[i] = false;
        }
        Debug.Log("CheckPoints Reset next lap");
    }
    private bool CheckLapProgress()
    {
        _ChecksMissing = 0;
        for (int i = 0; i < checkPointsDone.Count; i++)
        {
            if (!checkPointsDone[i])
            {
                _ChecksMissing++;
            }
        }
        if (_ChecksMissing > 0)
        {

            return false;
        }
        else
        {

            return true;
        }
    }
}

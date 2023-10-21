using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class LapManager : MonoBehaviour
{
    private const int Lap_Rounds = 8;
    private const int Lap_Checkpoints = 7;
    private List<bool> checkPointsDone = new List<bool>();
    private List<Vector3> GhostData = new List<Vector3>();
    public static int Lap_Current =1;
    private float StageStartTime, stageTime;
    private bool stageStarts = false;
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
        
    }
    public int GetLapRounds()
    {
        return Lap_Rounds;
    }
    
    public void StartStage()
    {
        
        stageStarts = true;
        HUD_Manager.StageStartTime  = Time.timeSinceLevelLoad;
    }
    public void CheckNextRound(Vector3 pos)
    {
        if(Lap_Current >= Lap_Rounds)
            {
                HUD_Manager.Instance.UpdateTimeLapRoundText(Lap_Current,Time.timeSinceLevelLoad-HUD_Manager.StageStartTime);
                StageWin();
                return;
            }
        if(CheckLapProgress())
        {    
            GhostData.Add(pos);
            NextRound();
        }    
        
    }
    public void CheckpointSet(int _checkpoint_id, Vector3 pos)
    {
        
        if(checkPointsDone[_checkpoint_id] == false)
        {
            checkPointsDone[_checkpoint_id] = true;
            Debug.Log("Checkpoint: New Checkpoint ID:" + _checkpoint_id);
            GhostData.Add(pos);
        }
        else{
            Debug.Log("Checkpoint: Allready Checked");
            
        }
        
    }
    private void NextRound()
    {
            HUD_Manager.Instance.UpdateTimeLapRoundText(Lap_Current,Time.timeSinceLevelLoad-HUD_Manager.StageStartTime);
            Debug.Log("Next Round");
            Lap_Current ++;
            HUD_Manager.Instance.UpdateLapRound(Lap_Current);
            ResetCheckPoints();
    }
    private void StageWin()
    {
        Debug.Log("Win Stage");
        HUD_Manager.Instance.SetTimer(false);
        
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
            if(!checkPointsDone[i])
            {
                _ChecksMissing++;
            }
        }
        if(_ChecksMissing > 0)
        {
            Debug.Log(_ChecksMissing);
            Debug.Log(checkPointsDone.Count);
            return false;
        }
        else{
            Debug.Log(_ChecksMissing);
            Debug.Log(checkPointsDone.Count);
            return true;
        }
    }
}

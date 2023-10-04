using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapManager : MonoBehaviour
{
    [SerializeField] private int Lap_Rounds;
    [SerializeField] private int Lap_Checkpoints;
    private List<bool> checkPointsDone = new List<bool>();
    private List<Vector3> GhostData = new List<Vector3>();
    public int Lap_Current =1;
    int _ChecksMissing = 0;
    
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
        if(CheckLapProgress())
        {
            Lap_Current ++;
            if(Lap_Current >= Lap_Rounds)
            {
                StageWin();
            }
            ResetCheckPoints();
        }
    }
    private void StageWin()
    {
        Debug.Log("Win Stage");
        // STOP TIMER
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
        
        for (int i = 0; i < checkPointsDone.Count; i++)
        {
            if(!checkPointsDone[i])
            {
                _ChecksMissing++;
            }
        }
        if(_ChecksMissing > 0)
        {
            return false;
        }
        else{
            return true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionManager : MonoBehaviour
{
    public static CollectionManager Instance;
    public int[][] CoinWallet = new int[8][];
    public int[] CarWallet = new int[8];
    [SerializeField] private Color collectIconColor_Default, collectIconColor_Clear;
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        DontDestroyOnLoad(gameObject);
        ResetStats();

    }
    void Start()
    {
        LoadSaveGameData();
        /*****************************************************************************

        GET DATA FROM PLAYERPREF: 
        SAVE TO COINWALLET
        SAVE DATA TO PLAYERPREF AFTER COLLECT 
        *********************************************************************************/

    }

    public void LoadSaveGameData()
    {
        GameData data = SaveGameManager.LoadData();
        CoinWallet = data.CollectedCoins;
        CarWallet = data.CollectedCars;
    }
    public void ResetStats()
    {
        for (int i = 0; i < 8; i++)
        {

            CoinWallet[i] = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
            CarWallet[i] = 0;
        }
    }
    public void ResetSaveGame()
    {
        ResetStats();
        SaveGameManager.SaveData(this);
    }
    public void SetCoin(int id, int stage)
    {
        CoinWallet[stage][id] = 1;
        // Debug.LogFormat("Coin Collected Stage{0}: [ {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}", stage, CoinWallet[stage][0],
        // CoinWallet[stage][1], CoinWallet[stage][2], CoinWallet[stage][3], CoinWallet[stage][4], CoinWallet[stage][5], CoinWallet[stage][6], CoinWallet[stage][7]);
        HUD_Manager.Instance.UpdateCollectables(GetCoins(stage));

        SaveGameManager.SaveData(this);

        if (GetCoins(stage) >= 8)
        {
            HUD_Manager.Instance.UpdateCollectableIcon(collectIconColor_Clear);
        }
    }
    public void LoadLevelCoins(int stage)
    {
        // LOAD FROM PLAYERPREFS

        HUD_Manager.Instance.UpdateCollectableIcon(collectIconColor_Default);
        if (GetCoins(stage) >= 8)
        {
            HUD_Manager.Instance.UpdateCollectableIcon(collectIconColor_Clear);
        }
    }
    public int GetCoins(int stage)
    {
        int count = 0;
        for (int i = 0; i < CoinWallet[stage].Length; i++)
        {
            if (CoinWallet[stage][i] == 1)
                count++;

        }
        Debug.Log("Coin Count on this stage:" + count);
        return count;
    }

}

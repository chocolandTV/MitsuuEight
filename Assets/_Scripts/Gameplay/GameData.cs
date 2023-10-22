using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GameData
{
    public int[][] CollectedCoins;
    public int[] CollectedCars;

    public GameData(CollectionManager collectionManager)
    {
        CollectedCoins = collectionManager.CoinWallet;
        CollectedCars = collectionManager.CarWallet;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinIndex : MonoBehaviour
{
    [SerializeField]private int Coin_Index;

    public int GetCoinIndex()
    {
        return Coin_Index;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SQL_Test : MonoBehaviour
{
    [SerializeField]private List<TMP_InputField> InputList; 
    // Start is called before the first frame update
    public async void OnButtonPress()
    {
        if(string.IsNullOrWhiteSpace(InputList[0].text)|| InputList[0].text.Length<0)// STAGE
        {
            Debug.LogError("Stage is not Valid, Error_01");
            return;
        }
        if(string.IsNullOrWhiteSpace(InputList[1].text) ||InputList[1].text.Length<1)// STAGETIME
        {
            Debug.LogError("Time is not Valid, Error_02");
        }
        if(string.IsNullOrWhiteSpace(InputList[2].text) ||InputList[2].text.Length<1)// STAGETIME
        {
            Debug.LogError("Name is not Valid, Error_03");
            InputList[2].text  = "anonymous";
        }
        
        if(await SQL_Manager.AddHighscore(InputList[0].text,
        InputList[1].text,
        InputList[2].text,
        InputList[3].text,
        InputList[4].text,
        System.DateTime.Now.ToString(),
        InputList[6].text,
        InputList[7].text))
        {
            Debug.Log("Success Saved");
        }else{
            Debug.Log("Failed to Saved, Error_04");
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public static class SQL_Manager
{
    readonly static string SERVER_URL = "mitsueight.mucic-tb.de:80";
    public static async Task<bool> AddHighscore(string stage, string stagetime, string username, string fastestlap, string coins, string date, string car, string totaltime)
    {
        string REGISTER_USER_URL = $"{SERVER_URL}/addHighscore.php";

        return await SendPostRequest(REGISTER_USER_URL, new Dictionary<string, string>()
        {
            {"stage", "Stage0" + stage},
            {"stagetime", stagetime},
            {"username", username},
            {"fastestlap", fastestlap},
            {"coins",coins},
            {"date", date},
            {"car", car},
            {"totaltime", totaltime}
        });
    }
    static async Task<bool> SendPostRequest(string url, Dictionary<string, string> data)
    {
        using (UnityWebRequest req = UnityWebRequest.Post(url, data))
        {
            req.SendWebRequest();

            while (!req.isDone) await Task.Delay(100);
            // when the Task is done
            if (req.error != null || string.IsNullOrWhiteSpace(req.error)
            || HasErrorMessage(req.downloadHandler.text))
                return false;

            // On Success
            return true;

        }
    }
    static bool HasErrorMessage(string msg) => int.TryParse(msg, out var res);

}
public class DataHighscore
{
    public int Stage;
    public float Stagetime;
    public string Username;
    public float Fastestlap;
    public int Coins;
    public DateTime Date;
    public int Car;
    public float Totaltime;
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName ="New Stage", menuName ="Scriptable Objects/Stage")]
public class Stage : ScriptableObject
{
    public enum Difficult
    {
        easy,
        normal,
        hard,
        insane
    }
    public int mapIndex;
    public string mapName;
    public string mapDescription;
    public Difficult difficult;
    public Color nameColor1, nameColor2;
    public Sprite mapImage;
    public Object sceneToLoad;
}

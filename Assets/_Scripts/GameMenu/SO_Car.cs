using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu (fileName ="New Car", menuName ="Scriptable Objects/Car")]
public class SO_Car : ScriptableObject
{
    public int CarIndex,MaxSpeed, Accelerate, Handling;
    public string CarName, CarDescription;
    public Color nameColor1, nameColor2;
    public Texture CarImage;
    public Object Prefab;

}

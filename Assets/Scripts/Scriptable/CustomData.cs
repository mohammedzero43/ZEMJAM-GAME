using UnityEngine;

[CreateAssetMenu(fileName = "CustomData", menuName = "MyGame/CustomData", order = 1)]
public class CustomData : ScriptableObject
{
    // Define properties and variables for your custom data
    public int myInt;
    public string myString;
    public Color myColor;
}

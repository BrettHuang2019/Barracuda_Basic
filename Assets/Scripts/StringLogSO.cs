using UnityEngine;

[CreateAssetMenu(fileName = "StringLog", menuName = "ScriptableObject/StringLog")]
public class StringLogSO : ScriptableObject
{
    [TextArea(50,200)]
    public string logStr = "This is a log string.";

}

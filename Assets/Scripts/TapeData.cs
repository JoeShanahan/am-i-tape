using UnityEngine;

[CreateAssetMenu(fileName = "TapeData", menuName = "Scriptable Objects/TapeData")]
public class TapeData : ScriptableObject
{
    public string TapeName;
    public Sprite PreviewSprite;
    public string Description;
    public GameObject Prefab;
    public Texture2D Texture;
    public bool IsLocked;

}

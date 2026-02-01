using UnityEngine;

[CreateAssetMenu(fileName = "TapeData", menuName = "Scriptable Objects/TapeData")]
public class TapeData : ScriptableObject
{
    public string TapeName;
    public Sprite PreviewSprite;
    [TextArea]
    public string Description;
    public GameObject Prefab;
    public GameObject PrefabMultiplayer;
    public Material material;
    public bool IsLocked;

}

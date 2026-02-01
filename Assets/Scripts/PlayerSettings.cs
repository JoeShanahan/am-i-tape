using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "Scriptable Objects/PlayerSettings")]
public class PlayerSettings : ScriptableObject
{
    public enum GameMode
    {
        Single,
        MultiHost,
        MultiClient
    }

    public GameMode SelectedMode;
    public string RemoteIP;
    public int RemotePort;
    public string MultiplayerName;
    public TapeData SelectedTape;

    public bool InvertX;
    public bool InvertY;

    public TapeData[] AllTapes;
}

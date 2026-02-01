using Unity.Netcode;
using UnityEngine;

public class NetworkStarter : MonoBehaviour
{

    [SerializeField]
    private PlayerSettings _settings;

    [SerializeField]
    private NetworkManager _netMan;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (_settings.SelectedMode == PlayerSettings.GameMode.MultiHost)
        {
            _netMan.OnClientConnectedCallback += ClientConnected;
            _netMan.OnClientDisconnectCallback += ClientDisconnected;
            _netMan.StartHost();
        }
        else if (_settings.SelectedMode == PlayerSettings.GameMode.MultiClient)
        {
            _netMan.StartClient();
        }
    }

    private void ClientConnected(ulong id)
    {
        Debug.Log($"Player with id {id} joined!");
    }

    private void ClientDisconnected(ulong id)
    {
        Debug.Log($"Player with id {id} left!");
    }
}

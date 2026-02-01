using System.Net;
using System.Net.Sockets;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
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


            var transport = (UnityTransport)_netMan.NetworkConfig.NetworkTransport;
            transport.ConnectionData.Address = GetLocalIPv4();
            transport.ConnectionData.Port = 7777;

            Debug.Log($"Starting server on: {transport.ConnectionData.Address}");

            _netMan.StartHost();
        }
        else if (_settings.SelectedMode == PlayerSettings.GameMode.MultiClient)
        {
            var transport = (UnityTransport)_netMan.NetworkConfig.NetworkTransport;
            transport.ConnectionData.Address = _settings.RemoteIP;
            transport.ConnectionData.Port = 7777;
            Debug.Log($"Connecting to: {transport.ConnectionData.Address}");
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

    
    public static string GetLocalIPv4()
    {
        foreach (var ni in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
        {
            if (ni.AddressFamily == AddressFamily.InterNetwork)
                return ni.ToString();
        }

        return "0.0.0.0";
    }
}

using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

public class NetworkedPlayerSpawner : NetworkBehaviour
{
    [SerializeField]
    private PlayerSettings _settings;

    [SerializeField]
    private Transform _spawnPoint;

    [SerializeField]
    private GameObject _extras;

    public override void OnNetworkSpawn()
    {
        if (IsOwner && IsClient)
        {
            RequestSpawnRpc(_settings.SelectedTape.name, _settings.MultiplayerName);
        }
    }

    [Rpc(SendTo.Server)]
    private void RequestSpawnRpc(string tapeId, string playerId, RpcParams rpcParams = default)
    {
        ulong requestingClientId = rpcParams.Receive.SenderClientId;

        Debug.Log($"Server received spawn request from client {requestingClientId} " +
                  $"for tape {tapeId} and player {playerId}");

        SpawnPlayerObject(requestingClientId, tapeId, playerId);
    }

    private void SpawnPlayerObject(ulong ownerClientId, string tapeId, string playerId)
    {
        TapeData d = _settings.AllTapes.FirstOrDefault(t => t.name == tapeId);

        if (d == null)
        {
            Debug.LogError($"No idea what tape '{tapeId}' is???");
            return;
        }

        GameObject go = Instantiate(d.PrefabMultiplayer);
        var netObj = go.GetComponent<NetworkObject>();

        go.GetComponent<NetworkTapeBehaviour>().PlayerId.Value = playerId;
                netObj.SpawnWithOwnership(ownerClientId);
    }
}
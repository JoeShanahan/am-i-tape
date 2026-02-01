using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class NetworkTapeBehaviour : NetworkBehaviour
{
    [SerializeField]
    private GameObject _extras;

    [SerializeField]
    private GameObject _nameTagPrefab;

    public NetworkVariable<FixedString64Bytes> PlayerId = new();


    public override void OnNetworkSpawn()
    {
        Debug.Log($"I have just spawned: IsOwner: {IsOwner}, IsClient: {IsClient}");
        if (IsOwner)
        {
            Instantiate(_extras);
            var camFollow = FindFirstObjectByType<CameraFollow>();
            camFollow.Init(transform);
            GetComponent<TapePlayerInput>().Init(camFollow.transform, true);
                GameObject respawnPos = GameObject.FindWithTag("Respawn");
            Rigidbody _rb = GetComponent<Rigidbody>();

        if (_rb != null && respawnPos != null)
            _rb.position = respawnPos.transform.position;
        }
        else
        {
            GetComponent<TapePlayerInput>().Init(null, false);
            W2C.InstantiateAs<PlayerNameTag>(_nameTagPrefab).Init(transform, PlayerId.Value.ToString());
            
        }   
    }
}

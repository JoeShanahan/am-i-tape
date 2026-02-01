using Unity.Netcode;
using UnityEngine;

public class NetworkTapeBehaviour : NetworkBehaviour
{
    [SerializeField]
    private GameObject _extras;

    public override void OnNetworkSpawn()
    {
        Debug.Log($"I have just spawned: IsOwner: {IsOwner}, IsClient: {IsClient}");
        if (IsOwner)
        {
            Instantiate(_extras);
            var camFollow = FindFirstObjectByType<CameraFollow>();
            camFollow.Init(transform);
            GetComponent<TapePlayerInput>().Init(camFollow.transform, true);       
        }
        else
        {
            GetComponent<TapePlayerInput>().Init(null, false);
            
        }   
    }
}

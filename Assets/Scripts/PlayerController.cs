using Unity.Cinemachine;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private TapeData _selectedTape;

    [SerializeField]
    private CameraFollow _cameraFollow;

    [SerializeField]
    private CinemachineCamera _virtualCam;

    private GameObject _spawnedTape;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bool isLocalPlayer = true;

        _virtualCam.gameObject.SetActive(isLocalPlayer);
        _spawnedTape = Instantiate(_selectedTape.Prefab);
        _spawnedTape.GetComponent<TapePlayerInput>().Init(_cameraFollow.transform, isLocalPlayer);
        _cameraFollow.Init(_spawnedTape.transform);
        FindFirstObjectByType<PlayerDebugUI>()?.Init(_spawnedTape);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

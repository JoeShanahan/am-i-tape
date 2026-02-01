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

    [SerializeField] GameObject respawnPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bool isLocalPlayer = true;

        _virtualCam.gameObject.SetActive(isLocalPlayer);
        _spawnedTape = Instantiate(_selectedTape.Prefab);
        _spawnedTape.GetComponent<TapePlayerInput>().Init(_cameraFollow.transform, isLocalPlayer);
        _cameraFollow.Init(_spawnedTape.transform);
        FindFirstObjectByType<PlayerDebugUI>()?.Init(_spawnedTape);


        Vector3 respawnPos = respawnPoint.transform.position;
        Rigidbody _rb = _spawnedTape.GetComponent<Rigidbody>();
        _rb.position = respawnPos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

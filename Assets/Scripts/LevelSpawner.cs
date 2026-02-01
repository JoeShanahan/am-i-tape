using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-500)]
public class LevelSpawner : MonoBehaviour
{
    [SerializeField] private string _levelToLoad;
    void Awake()
    {
        SceneManager.LoadSceneAsync(_levelToLoad, LoadSceneMode.Additive);
    }
}

using Unity.VisualScripting;
using UnityEngine;

public class TitleScreenController : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup _rootScreen;

    [SerializeField]
    private CanvasGroup _singlePlayerScreen;

    [SerializeField]
    private CanvasGroup _mutliplayerScreen;

    [SerializeField]
    private PlayerSettings _settings;

    public void ButtonPressSingle()
    {
        _rootScreen.gameObject.SetActive(false);
        _singlePlayerScreen.gameObject.SetActive(true);
        _settings.SelectedMode = PlayerSettings.GameMode.Single;
    }

    public void ButtonPressMutli()
    {
        _rootScreen.gameObject.SetActive(false);
        _mutliplayerScreen.gameObject.SetActive(true);
    }

    public void ButtonPressBack()
    {
        _rootScreen.gameObject.SetActive(true);
        _singlePlayerScreen.gameObject.SetActive(false);
        _mutliplayerScreen.gameObject.SetActive(false);
    }

    public void ButtonPressPlaySingle()
    {
        _settings.SelectedMode = PlayerSettings.GameMode.Single;
        PersistentUI.DoTransition("JoeScene");
    }

    public void ButtonPressPlayMultiHost()
    {
        _settings.SelectedMode = PlayerSettings.GameMode.MultiHost;
        PersistentUI.DoTransition("MultiScene");
    }

    public void ButtonPressPlayMultiClient()
    {
        _settings.SelectedMode = PlayerSettings.GameMode.MultiClient;
        PersistentUI.DoTransition("MultiScene");
    }

    public void ButtonPressQuit()
    {
        Application.Quit();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

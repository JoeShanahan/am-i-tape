using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TitleScreenController : MonoBehaviour
{
    private GameObject _lastSelectedButton;

    [SerializeField]
    private CanvasGroup _rootScreen;

    [SerializeField]
    private CanvasGroup _singlePlayerScreen;

    [SerializeField]
    private CanvasGroup _multiplayerScreen;

    [SerializeField]
    private CanvasGroup _settingsScreen;
    
    [SerializeField]
    private PlayerSettings _settings;
    
    [SerializeField] private GameObject _rootDefaultButton;
    [SerializeField] private GameObject _singleDefaultButton;
    [SerializeField] private GameObject _multiDefaultButton;

    public void ButtonPressSingle()
    {
        _rootScreen.gameObject.SetActive(false);
        _singlePlayerScreen.gameObject.SetActive(true);
        _multiplayerScreen.gameObject.SetActive(false);
        _settingsScreen.gameObject.SetActive(false);

    }
    public void ButtonPressMulti()
    {
        _rootScreen.gameObject.SetActive(false);
        _multiplayerScreen.gameObject.SetActive(true);
        _singlePlayerScreen.gameObject.SetActive(false);
        _settingsScreen.gameObject.SetActive(false);

    }
    
    public void ButtonPressSettings()
    {
        _rootScreen.gameObject.SetActive(false);
        _settingsScreen.gameObject.SetActive(true);
        _singlePlayerScreen.gameObject.SetActive(false);
        _multiplayerScreen.gameObject.SetActive(false);

    }
    
    public void ButtonPressBack()
    {
        _rootScreen.gameObject.SetActive(true);
        _singlePlayerScreen.gameObject.SetActive(false);
        _multiplayerScreen.gameObject.SetActive(false);
        _settingsScreen.gameObject.SetActive(false);

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

    private void SelectButton(GameObject button)
    {
        _lastSelectedButton = button;
    }

    void Update()
    {
        var selected = EventSystem.current.currentSelectedGameObject;
        if (selected)
        {
            _lastSelectedButton = selected;
        }
    }


}

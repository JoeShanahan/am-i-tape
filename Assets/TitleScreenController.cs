using UnityEngine;

public class TitleScreenController : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup _rootScreen;

    [SerializeField]
    private CanvasGroup _singlePlayerScreen;

    [SerializeField]
    private CanvasGroup _mutliplayerScreen;

    public void ButtonPressSingle()
    {
        _rootScreen.gameObject.SetActive(false);
        _singlePlayerScreen.gameObject.SetActive(true);
    }

    public void ButtonPressMutli()
    {
        
    }

    public void ButtonPressBack()
    {
        _rootScreen.gameObject.SetActive(true);
        _singlePlayerScreen.gameObject.SetActive(false);
    }

    public void ButtonPressPlaySingle()
    {
        PersistentUI.DoTransition("JoeScene");
    }

    public void ButtonPressPlayMultiHost()
    {
        
    }

    public void ButtonPressPlayMultiClient()
    {
        
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

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PauseController : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen;

    private InputSystem_Actions _input;
    private bool _isPaused;

    void Awake()
    {
        _input = new InputSystem_Actions();
        _input.Enable();
        _input.UI.Menu.performed += PausePressed;
    }

    private void OnDestroy()
    {
        _input.UI.Menu.performed -= PausePressed;
        _input.Disable();
    }

    private void PausePressed(InputAction.CallbackContext ctx)
    {
        TogglePause();
    }

    public void PauseUiButtonPressed()
    {
        TogglePause();
    }

    private void TogglePause()
    {
        _isPaused = !_isPaused;

        pauseScreen.SetActive(_isPaused);

        // Time.timeScale = _isPaused ? 0f : 1f;
    }
    
    public void ButtonPressQuit()
    {
        Application.Quit();
    }
    
    public void ButtonPressMenu()
    {;
        PersistentUI.DoTransition("TitleScene");

    }
}
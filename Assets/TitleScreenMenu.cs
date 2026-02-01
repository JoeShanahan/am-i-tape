using UnityEngine;
using UnityEngine.EventSystems;

public class TitleScreenMenu : MonoBehaviour
{
    [SerializeField] private GameObject _defaultButton;
    
    void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(_defaultButton);;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

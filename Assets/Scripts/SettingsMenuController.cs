using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuController : MonoBehaviour
{
    [SerializeField] private PlayerSettings settings;
    [SerializeField] private Toggle invertXToggle;
    [SerializeField] private Toggle invertYToggle;

    void Start()
    {
        // Load saved values into UI
        invertXToggle.isOn = settings.InvertX;
        invertYToggle.isOn = settings.InvertY;

        // Hook up events
        invertXToggle.onValueChanged.AddListener(OnInvertXChanged);
        invertYToggle.onValueChanged.AddListener(OnInvertYChanged);
    }

    private void OnInvertXChanged(bool value)
    {
        settings.InvertX = value;
    }

    private void OnInvertYChanged(bool value)
    {
        settings.InvertY = value;
    }
}
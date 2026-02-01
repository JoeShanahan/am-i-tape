using System.Net;
using System.Net.Sockets;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MultiplayerTitleMenu : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField _ipEntry;

    [SerializeField]
    private TMP_InputField _nameEntry;

    [SerializeField]
    private TMP_Text _myIpText;

    [SerializeField]
    private PlayerSettings _settings;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _myIpText.text = $"My IP: {GetLocalIPv4()}";
        _ipEntry.onSubmit.AddListener(_ => _settings.RemoteIP = _ipEntry.text);
        _nameEntry.onSubmit.AddListener(_ => _settings.MultiplayerName = _nameEntry.text);
    }

    void OnEnable()
    {
        _myIpText.text = _settings.RemoteIP;
        _nameEntry.text = _settings.MultiplayerName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static string GetLocalIPv4()
    {
        foreach (var ni in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
        {
            if (ni.AddressFamily == AddressFamily.InterNetwork)
                return ni.ToString();
        }

        return "0.0.0.0";
    }

}

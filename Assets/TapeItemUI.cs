using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TapeItemUI : MonoBehaviour
{

    public Image sprite;
    public TMP_Text nametext;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTapeData(TapeData data)
    {
        nametext.text = data.TapeName;
    }
}

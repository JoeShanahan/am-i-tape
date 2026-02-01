using TMPro;
using UnityEngine;

public class TapeSelectUI : MonoBehaviour
{

    public GameObject Prefab;
    public PlayerSettings Settings;
    
    // reference the TMP for the text
    public TMP_Text DescriptionText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DescriptionText.text = "";
        
        foreach (TapeData tape in Settings.AllTapes)
        {
            GameObject NewObject = Instantiate(Prefab, transform);
            NewObject.GetComponent<TapeItemUI>().SetTapeData(tape, DescriptionText); // pass in the TMP text
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

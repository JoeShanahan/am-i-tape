using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TapeItemUI : MonoBehaviour
{

    public Image sprite;
    public TMP_Text nametext;
    public TMP_Text descriptiontext;
    public Transform LockedObject;
    public TapeData tapedata;
    public PlayerSettings settings;
    public Image HighlightImage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HighlightImage.enabled = tapedata == settings.SelectedTape;

        if (EventSystem.current.currentSelectedGameObject == gameObject)
        {
            descriptiontext.text = tapedata.Description;        
        }
    }

    public void SetTapeData(TapeData data, TMP_Text DT) // add parameter for "description text"
    {
        // save reference to description text
        descriptiontext = DT;
        nametext.text = data.TapeName;
        tapedata = data;
       // descriptiontext.text = data.Description;
        LockedObject.gameObject.SetActive(data.IsLocked);
        sprite.sprite = data.PreviewSprite;
        if (data.IsLocked)
        {
            nametext.text = "Locked";
            
        }
    }

    public void ButtonPressed()
    {
        settings.SelectedTape = tapedata;
    }

    public void onSelected()
    {
    }
}
   

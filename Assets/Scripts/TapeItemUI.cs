using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TapeItemUI : MonoBehaviour
{

    public Image sprite;
    public TMP_Text nametext;
    public TMP_Text descriptiontext;
    public Transform LockedObject;
    public TapeData tapedata;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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

    public void onSelected()
    {
        // set description text.text as Description
        descriptiontext.text = tapedata.Description;
    }
}
   

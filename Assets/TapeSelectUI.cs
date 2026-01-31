using UnityEngine;

public class TapeSelectUI : MonoBehaviour
{

    public GameObject Prefab;
    public TapeData [] AllTapes;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (TapeData tape in AllTapes)
        {
            GameObject NewObject = Instantiate(Prefab, transform);
            NewObject.GetComponent<TapeItemUI>().SetTapeData(tape);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

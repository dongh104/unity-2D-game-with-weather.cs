using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class itemcount : MonoBehaviour
{
    public TextMeshPro ic;

    GameManager gm;

    private void Awake()
    {
        gm = FindAnyObjectByType<GameManager>();

    }
    void Start()
    {
        ic = GetComponent<TextMeshPro>();
        //if (gm.getItem.Count > 0)
       // {
        //    //ic.text = gm.getItem.Count.ToString();
        //    Debug.Log(gm.getItem.Count);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        ic.text = gm.getItem.Count.ToString();
    }
    public void aaa()
    {
        ic.text = gm.getItem.Count.ToString();
    }
}

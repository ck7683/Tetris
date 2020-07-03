using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    GameObject bottom;
    GameObject left;
    GameObject right;

    private void Awake()
    {
        bottom = transform.Find("Bottom").gameObject;
        left = transform.Find("Left").gameObject;
        right = transform.Find("Right").gameObject;
    }

    void Start()
    {
    }


    void Update()
    {
        
    }

    public void InitSetting(int Xsize, int Ysize)
    {
        bottom.transform.position = new Vector3(0.5f, -Ysize / 2 + 1, 0);
        left.transform.position = new Vector3(-Xsize / 2, 3, 0);
        right.transform.position = new Vector3(-Xsize / 2 + Xsize + 1, 3, 0);

        bottom.transform.localScale = new Vector3(Xsize+2, 1, 1);
        left.transform.localScale = new Vector3(1, Ysize+3, 1);
        right.transform.localScale = new Vector3(1, Ysize+3, 1);
    }
}

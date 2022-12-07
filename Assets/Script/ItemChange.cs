using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChange : MonoBehaviour
{
    public GameObject item1;
    public GameObject item2;
    public GameObject item3;


    private void Start()
    {
        item1.SetActive(false);
        item2.SetActive(false);
        item3.SetActive(false);

    }

    void Update()
    {
        trocarItem();
    }

    void trocarItem()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (item1.activeSelf == false)
            {
                item1.SetActive(true);
                item2.SetActive(false);
                item3.SetActive(false);
            }
            else
            {
                item1.SetActive(false);
                item2.SetActive(false);
                item3.SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (item2.activeSelf == false)
            {
                item1.SetActive(false);
                item2.SetActive(true);
                item3.SetActive(false);
            }
            else
            {
                item1.SetActive(false);
                item2.SetActive(false);
                item3.SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (item3.activeSelf == false)
            {
                item1.SetActive(false);
                item2.SetActive(false);
                item3.SetActive(true);
            }
            else
            {
                item1.SetActive(false);
                item2.SetActive(false);
                item3.SetActive(false);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightController : MonoBehaviour
{
    [SerializeField] GameObject lightOn;
    [SerializeField] GameObject lightOff;
    [SerializeField] GameObject[] targetObjects;
    [SerializeField] Image[] images;

    private void Update()
    {
        Check();
    }

    void Check()
    {
        for (int i = 0; i < targetObjects.Length; i++)
        {
            if (targetObjects[i].activeSelf)
            {
                LightOff();
                return;
            }

            LightOn();
        }
    }

    void LightOn()
    {
        lightOn.SetActive(true);
        lightOff.SetActive(false);
    }

    void LightOff()
    {
        lightOn.SetActive(false);
        lightOff.SetActive(true);
    }
}

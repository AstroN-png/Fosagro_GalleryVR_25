using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultPanelController : MonoBehaviour
{
    public static ResultPanelController instance;   

    [SerializeField] GameObject panel1;
    [SerializeField] GameObject panel2;
    [SerializeField] GameObject btnLeft;
    [SerializeField] GameObject btnRight;
    [SerializeField] GameObject btnSelect;
    [SerializeField] GameObject btnRestart;
    [SerializeField] TextMeshProUGUI headText;
    [SerializeField] TextMeshProUGUI mainText;
    [SerializeField] string headChoose;
    [SerializeField] string headResult;

    [SerializeField] string[] punishmentText;
    [SerializeField] string[] descriptionText;
    [SerializeField] int indexOfChoose;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        ToDefault();
    }

    public void ToDefault()
    {
        indexOfChoose = 0;
        headText.text = headChoose;
        btnRight.SetActive(true);
        btnLeft.SetActive(false);
        mainText.text = punishmentText[indexOfChoose];
        panel1.SetActive(true);
        panel2.SetActive(true);
        btnSelect.SetActive(true);
        btnRestart.SetActive(false);

    }

    public void LeftChoose()
    {
        indexOfChoose--;

        if (indexOfChoose <= 0)
        {
            indexOfChoose = 0;

            btnRight.SetActive(true);
            btnLeft.SetActive(false);
        }
        else
        {
            btnRight.SetActive(true);
            btnLeft.SetActive(true);
        }
        mainText.text = punishmentText[indexOfChoose];
    }

    public void RightChoose()
    {
        indexOfChoose++;

        if (indexOfChoose >= 2)
        {
            indexOfChoose = 2;

            btnRight.SetActive(false);
            btnLeft.SetActive(true);
        }
        else
        {
            btnRight.SetActive(true);
            btnLeft.SetActive(true);
        }
        mainText.text = punishmentText[indexOfChoose];
    }

     public void ToChoose()
    {
        panel1.SetActive(true);
        panel2.SetActive(false);
        btnRight.SetActive(false);
        btnLeft.SetActive(false);
        btnSelect.SetActive(false);
        btnRestart.SetActive(true);
        mainText.text = descriptionText[indexOfChoose];
        headText.text = headResult;
    }

}

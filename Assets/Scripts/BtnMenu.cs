using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnMenu : MonoBehaviour
{
    public swipe _swipe;
    [SerializeField] bool loading;
    [SerializeField] Image loadingFrameImg;
    [SerializeField] GameObject loadingFrameObj;
    public bool continiueBtn;
    public bool restartBtn;
    public bool leftBtn;
    public bool rightBtn;
    public bool confirmBtn;
    public bool closeBtn;


    [SerializeField] bool startLoading;

    private void Update()
    {
        Loading();
    }

    private void OnEnable()
    {
        loadingFrameImg.fillAmount = 0;
    }

    void Loading()
    {
        if (loading)
        {
            loadingFrameImg.fillAmount += Time.deltaTime / 2;

            if (loadingFrameImg.fillAmount >= 1)
            {
                loading = false;

                //loadingFrameObj.SetActive(false);
                if (continiueBtn)
                {
                    Continiue();
                }
                if (restartBtn)
                {
                    Restart();
                }
                if (leftBtn)
                {
                    LeftBtn();
                }
                if (rightBtn)
                {
                    RightBtn();
                }
                if(confirmBtn)
                {
                    ConfirmBtn();
                }
                if (closeBtn)
                {
                   ObjData.instance.OpenGamePanel();
                }
            }
        }
    }

    public void Continiue()
    {
        GameController.instance.Continiue();
    }
    public void Restart()
    {
        ObjData.instance.OpenGamePanel();

    }

    public void SelectBtn()
    {
        if (startLoading) return;

        loadingFrameImg.fillAmount = 0;
        loading = true;
        Loading();
        loadingFrameObj.SetActive(true);

        startLoading = true;
    }


    public void UnselectBtn()
    {
        loadingFrameObj.SetActive(false);
        startLoading = false;
        loading = false;
    }

    public void LeftBtn()
    {
        _swipe.ScrollLeft();
    }
    public void RightBtn()
    {
       _swipe.ScrollRight();
    }
    public void ConfirmBtn()
    {
        ResultPanelController.instance.ToChoose();

    }
}

using UnityEngine;
using UnityEngine.UI;

public class Filial : MonoBehaviour
{
    [SerializeField] bool loading;
    [SerializeField] Image loadingFrameImg;
    [SerializeField] GameObject loadingFrameObj;
    [SerializeField] bool startLoading;
    [SerializeField] int idOfFilial;

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
            loadingFrameImg.fillAmount += Time.deltaTime / 3;

            if (loadingFrameImg.fillAmount >= 1)
            {
                loading = false;

                loadingFrameObj.SetActive(false);
                SceneLoader.instance.LoadFilial(idOfFilial);
                //PhotoPanel.instance.InitCurrentGroup(idOfFilial);

            }
        }
    }

    

    public void UnselectBtn()
    {
        loadingFrameObj.SetActive(false);
        startLoading = false;
        loading = false;
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
}

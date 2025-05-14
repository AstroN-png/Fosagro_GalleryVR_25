using UnityEngine;
using UnityEngine.UI;

public class PositionPoint : MonoBehaviour
{
    [SerializeField] Transform targetPosition;

    [SerializeField] Image loadingFrameImg;
    [SerializeField] Image mainImg;
    [SerializeField] bool startLoading; // загрузка запущена
    [SerializeField] bool loading;
    [SerializeField] GameObject[] objtsToDisactivate;
    Color pointColorDefault;
    Color pointColorSelected;

    private void Update()
    {
        Loading();
    }

    public void ObjectsToDisactivate(bool condition)
    {
        for (int i = 0; i < objtsToDisactivate.Length; i++)
        {
            objtsToDisactivate[i].SetActive(condition);
        }
    }
    private void Start()
    {
        loadingFrameImg.fillAmount = 0;

        
        pointColorDefault = mainImg.color;
        pointColorSelected = mainImg.color;
        pointColorDefault.a = 1f;
        pointColorSelected.a = 1;

        mainImg.color = pointColorDefault;


    }

    void Loading()
    {
        if (loading)
        {
            loadingFrameImg.fillAmount += Time.deltaTime / 1.5f;

            if (loadingFrameImg.fillAmount >= 1)
            {
                loading = false;
                startLoading = false;
                ChangePosition();
            }
        }
    }

    public void SelectPosition()
    {
        if (startLoading) return;
        loadingFrameImg.fillAmount = 0;
        loading = true;
        Loading();
        startLoading = true;
        mainImg.color = pointColorSelected;
    }
    public void UnselectPosition()
    {
        startLoading = false;
        loading = false;
        loadingFrameImg.fillAmount = 0;
        mainImg.color = pointColorDefault;
    }

    void ChangePosition()
    {
        loadingFrameImg.fillAmount = 0;
        Transform camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        camera.position = targetPosition.position;
        camera.transform.SetParent(targetPosition);
        GameController.instance.SetActualPosition(this);
       
    }
}

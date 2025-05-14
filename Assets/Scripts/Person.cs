using UnityEngine;
using UnityEngine.UI;

public class Person : MonoBehaviour
{
    public int indexOfPerson;
    public string nameOfPerson;
    [SerializeField] GameObject activeFrameObj;
    [SerializeField] GameObject loadingFrameObj;
    [SerializeField] Image loadingFrameImg;

    [SerializeField] Image nameFrame;

    [SerializeField] Color defaultColorNameFrame;
    [SerializeField] Color activeColorNameFrame;

    [SerializeField] bool selectable; //если можно активировать
    [SerializeField] bool startLoading; // загрузка запущена
    [SerializeField] bool loading; // загрузка в процессе
    [SerializeField] bool activated; // персона активна (говорит)
    public GameObject pivot;
    public bool nowUnvisible;

    private Animator animator;

    [SerializeField] private Transform targetPosition;
    public Transform defoultPosition;
    public Transform tribunePosition;
    public Transform pererivPosition;
    public float speed = 5.0f;
    private Vector3 velocity = Vector3.zero;
    public float smoothTime = 0.1f;

    public bool test;

    private void Start()
    {
        animator = GetComponent<Animator>();
        loadingFrameImg.fillAmount = 0;
        targetPosition = defoultPosition;
    }

    private void Update()
    {
        Loading();

        //if (test)
        //{
        //    targetPosition = tribunePosition;
        //}
        //else
        //{
        //    targetPosition = defoultPosition;
        //}
        
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
 
            transform.position = Vector3.SmoothDamp(transform.position,
                targetPosition.position, ref velocity, smoothTime);
    }

    void Loading()
    {
        if (loading)
        {
            loadingFrameImg.fillAmount += Time.deltaTime / 3;

            if (loadingFrameImg.fillAmount >= 1)
            {
                EndSelect();
                loading = false;
                
                loadingFrameObj.SetActive(false);
                startLoading = false;
                EndSelect();
            }
        }
    }

    public void GoToPererivPosition()
    {
        targetPosition = pererivPosition;
        targetPosition = pererivPosition;
    }

    public void GoToDefaultPosition(bool condition)
    {
        print("Go");
        if (condition)
        {
            print("DefaultPosition");

            targetPosition = defoultPosition;
            targetPosition = defoultPosition;
            targetPosition = defoultPosition;
        }
        else
        {
            print("TribunePosition");

            targetPosition = tribunePosition;
            targetPosition = tribunePosition;
            targetPosition = tribunePosition;

        }
    }

    public void SelectPerson()
    {
        if (!selectable) return;
        if (activated) return;
        if (startLoading) return;

        loadingFrameImg.fillAmount = 0;
        loading = true;
        Loading();
        loadingFrameObj.SetActive(true);
        activeFrameObj.SetActive(false);
        startLoading = true;
    }

    private void EndSelect()
    {
        loadingFrameImg.fillAmount = 0;
        selectable = false;
        StepsController.instance.NextStep();

    }

    public void ActivatePerson()
    {
        selectable = false;
        activated = true;
        activeFrameObj.SetActive(true);
        nameFrame.color = activeColorNameFrame;

    }

    public void Disactivate()
    {
        activated = false;
        activeFrameObj.SetActive(false);
        nameFrame.color = defaultColorNameFrame;
    }

    public void ToDefault()
    {
        selectable = false;
        activated = false;
        activeFrameObj.SetActive(false);
        nameFrame.color = defaultColorNameFrame;
    }

    public void UnselectPerson()
    {
        if (!selectable) return;
        if (activated) return;

        loadingFrameObj.SetActive(false);
        startLoading = false;
        loading = false;
    }

    public void Selectable(bool condition)
    {
        selectable = condition;
    }

    public void StandUp()
    {
        animator.SetTrigger("Up");
    }

    public void SitDown()
    {
        animator.SetTrigger("Down");

    }

}

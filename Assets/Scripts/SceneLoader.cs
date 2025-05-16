
using System.Collections;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;
    [SerializeField] CanvasGroupFader flashScreen;
    [SerializeField] GridFader gridFader;
    int idOfFilialNowSelected;
    public Animator sphereAnim;
    public GameObject particalPrafab;
    public Transform transformForPrefab;
    public GameObject effect;


    void Awake()
    {
        instance = this;
    }

    public void LoadFilial(int filialId)
    {
        StartCoroutine(LoadScene(filialId));
        
    }

    public void LoadChooseFilial()
    {
        StartCoroutine(IELoadChooseFilial());
    }

    IEnumerator LoadScene(int filialId)
    {

        effect.SetActive(true);
        sphereAnim.SetTrigger("Twist");
        idOfFilialNowSelected = filialId;
        TransformMover.instance.MoveToTarget(TransformMover.instance.filialsObj[filialId], 5);
        yield return new WaitForSeconds(1f);
        GameObject obj = Instantiate(particalPrafab, transformForPrefab);

        flashScreen.FadeIn();
        yield return new WaitForSeconds(1f);
        sphereAnim.SetTrigger("Twist");
        ObjData.instance.OpenFilialPanel(filialId);
        flashScreen.FadeOut();
        if (obj != null)
        {
            Destroy(obj);

        }
        effect.SetActive(false);

        yield break;
        
    }

    IEnumerator IELoadChooseFilial(){
        flashScreen.FadeIn();

        effect.SetActive(true);

        GameObject obj = Instantiate(particalPrafab, transformForPrefab);
        sphereAnim.SetTrigger("Twist");
        yield return new WaitForSeconds(1f);
        

        ObjData.instance.OpenGamePanel();
        flashScreen.FadeOut();
       
        sphereAnim.SetTrigger("Twist");

        //yield return new WaitForSeconds(1f);
        TransformMover.instance.MoveToTarget(TransformMover.instance.
                       filialsObj[idOfFilialNowSelected], idOfFilialNowSelected);
        idOfFilialNowSelected = -1;
        if (obj != null)
        {
            Destroy(obj);

        }
        effect.SetActive(false);

        yield break;
    }
}


using System.Collections;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;
    [SerializeField] CanvasGroupFader flashScreen;
    int idOfFilialNowSelected;

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
        idOfFilialNowSelected = filialId;
        TransformMover.instance.MoveToTarget(TransformMover.instance.filialsObj[filialId], 5);
        yield return new WaitForSeconds(1f);
        flashScreen.FadeIn();
        yield return new WaitForSeconds(1f);
        ObjData.instance.OpenFilialPanel(filialId);
       flashScreen.FadeOut();
        yield break;
        
    }

    IEnumerator IELoadChooseFilial(){
        flashScreen.FadeIn();
        yield return new WaitForSeconds(1f);
        ObjData.instance.OpenGamePanel();
        flashScreen.FadeOut();
        //yield return new WaitForSeconds(1f);
        TransformMover.instance.MoveToTarget(TransformMover.instance.
                       filialsObj[idOfFilialNowSelected], idOfFilialNowSelected);
        idOfFilialNowSelected = -1;
        yield break;
    }
}

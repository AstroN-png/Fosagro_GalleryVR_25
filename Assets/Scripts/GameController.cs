using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    PositionPoint nowInPosition;
    public bool newGame;
    [SerializeField] Transform startPosition;
    [SerializeField] PositionPoint startPositionPoint;

    private void Awake()
    {
        instance = this;
        newGame = true;
    }

    private void Start()
    {
        //SetStartPositionCamera();
        // ObjData.instance.OpenMainMenu(true);
        Restart();
    }

    public void SetActualPosition(PositionPoint pos)
    {
        if (nowInPosition != null)
        {
            nowInPosition.ObjectsToDisactivate(true);
        }
        nowInPosition = pos;
        nowInPosition.ObjectsToDisactivate(false);
    }

    public void SetStartPositionCamera()
    {
        Transform camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        camera.position = startPosition.position;
        camera.transform.SetParent(startPosition);
        GameController.instance.SetActualPosition(startPositionPoint);
    }

    public void Restart()
    {
        ObjData.instance.OpenMainMenu();
       
    }

    public void Continiue()
    {
        ObjData.instance.OpenGamePanel(true);
    }
}

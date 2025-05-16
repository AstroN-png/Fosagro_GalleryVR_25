using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class PhotoPanel : MonoBehaviour
{
    public static PhotoPanel instance;
    public int filialId;

    [SerializeField] private GameObject leftBtn;
    [SerializeField] private GameObject rightBtn;
    [SerializeField] private Image image;

    private int currentPhotoIndex = 0;
    //public ImageGroup currentGroup;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] audioClips;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        //InitCurrentGroup();
       // ShowPhoto();
    }

    public void InitCurrentGroup(int filialId)
    {
        //if (DataLoader.instance == null || DataLoader.instance.imageGroups == null)
        //{
        //    Debug.LogError("DataLoader.instance �� ���������������.");
        //    return;
        //}

        //if (filialId < 0 || filialId >= DataLoader.instance.imageGroups.Count)
        //{
        //    Debug.LogError("�������� filialId: " + filialId);
        //    return;
        //}

        //currentGroup = DataLoader.instance.imageGroups[filialId];
        //currentPhotoIndex = 0;
        audioSource.clip = audioClips[filialId];
        audioSource.Play();
       // ShowPhoto();
    }

    // public void NextPhoto()
    // {
    //     if (currentGroup == null || currentGroup.imageNames.Count == 0)
    //         return;

    //     if (currentPhotoIndex < currentGroup.imageNames.Count - 1)
    //     {
    //         currentPhotoIndex++;
    //        // ShowPhoto();
    //     }
    // }

    // public void PreviousPhoto()
    // {
    //     if (currentGroup == null || currentGroup.imageNames.Count == 0)
    //         return;

    //     if (currentPhotoIndex > 0)
    //     {
    //         currentPhotoIndex--;
    //         //ShowPhoto();
    //     }
    // }

    // private void ShowPhoto()
    // {
    //     if (currentGroup == null || currentGroup.imageNames.Count == 0)
    //     {
    //         image.sprite = null;
    //         leftBtn.SetActive(false);
    //         rightBtn.SetActive(false);
    //         return;
    //     }

    //     string folderPath = Path.Combine(Application.streamingAssetsPath, currentGroup.folderName);
    //     string fileName = currentGroup.imageNames[currentPhotoIndex];
    //     //Sprite sprite = DataLoader.ImageSprite(folderPath, fileName);

    //     if (sprite != null)
    //     {
    //         image.gameObject.SetActive(false);
    //         image.sprite = sprite;
    //         image.gameObject.SetActive(true);

    //         Debug.Log($"����� ���� {currentPhotoIndex + 1}/{currentGroup.imageNames.Count}: {fileName}");
    //     }

    //     leftBtn.SetActive(currentPhotoIndex > 0);
    //     rightBtn.SetActive(currentPhotoIndex < currentGroup.imageNames.Count - 1);
    // }
}

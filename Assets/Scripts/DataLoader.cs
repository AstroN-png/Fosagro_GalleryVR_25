using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class DataLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public AudioSource audioSourceMainScreen;
    [SerializeField] AudioSource[] audioSource;
    [SerializeField] Transform[] spawnPhoto;
    [SerializeField] Transform spawnVideo;
    [SerializeField] GameObject photoPrefab;
    [SerializeField] GameObject[] videoPrefab;
    [SerializeField] VideoController videoController;

    private string[] pathPhoto;
    private string[] pathAudio;
    private string pathVideo;

    void Awake()
    {
        pathPhoto = new string[4]
        {
            "Media/Photo/Balakovo",
            "Media/Photo/Cherepovec",
            "Media/Photo/Kirovsk",
            "Media/Photo/Volhov"
        };

        pathVideo = "Media/Video";

        pathAudio = new string[5]
        {
            "Media/Audio/Balakovo",
            "Media/Audio/Cherepovec",
            "Media/Audio/Kirovsk",
            "Media/Audio/Volhov",
            "Media/Audio/Main"
        };

        StartCoroutine(LoadMedia());
    }

    IEnumerator LoadMedia()
    {
        loadingScreen.SetActive(true);
        audioSourceMainScreen.Pause();
        // --- Загрузка аудио ---
        for (int i = 0; i < pathAudio.Length && i < audioSource.Length; i++)
        {
            string audioPath = Path.Combine(Application.streamingAssetsPath, pathAudio[i] + ".mp3"); // или .wav
            using (WWW www = new WWW("file://" + audioPath))
            {
                yield return www;
                if (www.error == null)
                {
                    audioSource[i].clip = www.GetAudioClip();
                    audioSource[i].Play();
                }
                else
                {
                    Debug.LogError($"Failed to load audio: {audioPath} - {www.error}");
                }
            }
        }

        // --- Загрузка фото с сортировкой по числу в имени ---
for (int i = 0; i < pathPhoto.Length && i < spawnPhoto.Length; i++)
{
    string fullPhotoPath = Path.Combine(Application.streamingAssetsPath, pathPhoto[i]);
    if (!Directory.Exists(fullPhotoPath))
    {
        Debug.LogWarning($"Photo directory not found: {fullPhotoPath}");
        continue;
    }

    // Получаем только jpg/png, сортируем по номеру в имени
    string[] photoFiles = Directory.GetFiles(fullPhotoPath, "*.*")
        .Where(f => f.EndsWith(".jpg") || f.EndsWith(".png"))
        .OrderBy(f =>
        {
            string name = Path.GetFileNameWithoutExtension(f);
            int number = 0;
            int.TryParse(name, out number);
            return number;
        })
        .ToArray();

    foreach (string filePath in photoFiles)
    {
        using (WWW www = new WWW("file://" + filePath))
        {
            yield return www;
            if (www.error == null)
            {
                Texture2D tex = www.texture;
                GameObject photoObj = Instantiate(photoPrefab, spawnPhoto[i]);
                Image img = photoObj.GetComponentInChildren<Image>();
                if (img != null)
                {
                    img.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
                }
            }
            else
            {
                Debug.LogError($"Failed to load image: {filePath} - {www.error}");
            }
        }
    }
}

        //---Загрузка видео-- -
       string fullVideoPath = Path.Combine(Application.streamingAssetsPath, pathVideo);
        if (Directory.Exists(fullVideoPath))
        {
            string[] videoFiles = Directory.GetFiles(fullVideoPath, "*.mp4"); // или другие форматы
            videoController.videoPlayer = new VideoPlayer[videoFiles.Length];
            int k = 0;
            foreach (string videoPath in videoFiles)
            {
                GameObject videoObj = Instantiate(videoPrefab[k], spawnVideo);
                VideoPlayer vp = videoObj.GetComponentInChildren<VideoPlayer>();
                videoController.videoPlayer[k] = vp;
                k++;
                if (vp != null)
                {
                    vp.url = "file://" + videoPath;
                    vp.Play();
                }
            }
        }
        else
        {
            Debug.LogWarning($"Video directory not found: {fullVideoPath}");
        }
        loadingScreen.SetActive(false);
        audioSourceMainScreen.Play();

    }
}

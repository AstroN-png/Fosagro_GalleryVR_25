using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataLoader : MonoBehaviour
{
    public static DataLoader instance;
    public List<ImageGroup> imageGroups = new List<ImageGroup>();

    private readonly string[] folderNames = { "Balakovo", "Cherepovec", "Kirovsk", "Volhov" };

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        LoadImageNames();

        // Пример вывода
        foreach (var group in imageGroups)
        {
            Debug.Log($"--- {group.folderName} ---");

            if (group.imageNames.Count == 0)
            {
                Debug.Log("  (Пусто)");
            }

            foreach (string name in group.imageNames)
            {
                Debug.Log("  " + name);
            }
        }
    }

    void LoadImageNames()
    {
        imageGroups.Clear(); // очищаем список на случай повторной загрузки
        string streamingAssetsPath = Application.streamingAssetsPath;

        foreach (string folder in folderNames)
        {
            string folderPath = Path.Combine(streamingAssetsPath, folder);
            ImageGroup group = new ImageGroup { folderName = folder };

            if (Directory.Exists(folderPath))
            {
                string[] files = Directory.GetFiles(folderPath);
                foreach (string filePath in files)
                {
                    string ext = Path.GetExtension(filePath).ToLower();
                    if (ext == ".jpg" || ext == ".jpeg" || ext == ".png")
                    {
                        group.imageNames.Add(Path.GetFileName(filePath));
                    }
                }
            }
            else
            {
                Debug.LogWarning($"Папка не найдена: {folderPath}");
            }

            imageGroups.Add(group);
        }
    }

    public static Sprite ImageSprite(string path, string name)
    {
        string fullPath = Path.Combine(path, name);

        // Убедимся, что файл существует
        if (!File.Exists(fullPath))
        {
            Debug.LogError("Файл не найден: " + fullPath);
            return null;
        }

        // Считываем байты изображения
        byte[] imageData = File.ReadAllBytes(fullPath);

        // Создаем текстуру
        Texture2D texture = new Texture2D(2, 2); // Размер не важен — будет заменён при LoadImage
        if (!texture.LoadImage(imageData))
        {
            Debug.LogError("Не удалось загрузить изображение: " + fullPath);
            return null;
        }

        // Создаем спрайт
        Sprite sprite = Sprite.Create(
            texture,
            new Rect(0, 0, texture.width, texture.height),
            new Vector2(0.5f, 0.5f), // pivot по центру
            100f                     // pixels per unit — настрой по ситуации
        );

        return sprite;
    }
}

[Serializable]
public class ImageGroup
{
    public string folderName;
    public List<string> imageNames = new List<string>();
}

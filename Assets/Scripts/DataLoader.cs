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

        // ������ ������
        foreach (var group in imageGroups)
        {
            Debug.Log($"--- {group.folderName} ---");

            if (group.imageNames.Count == 0)
            {
                Debug.Log("  (�����)");
            }

            foreach (string name in group.imageNames)
            {
                Debug.Log("  " + name);
            }
        }
    }

    void LoadImageNames()
    {
        imageGroups.Clear(); // ������� ������ �� ������ ��������� ��������
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
                Debug.LogWarning($"����� �� �������: {folderPath}");
            }

            imageGroups.Add(group);
        }
    }

    public static Sprite ImageSprite(string path, string name)
    {
        string fullPath = Path.Combine(path, name);

        // ��������, ��� ���� ����������
        if (!File.Exists(fullPath))
        {
            Debug.LogError("���� �� ������: " + fullPath);
            return null;
        }

        // ��������� ����� �����������
        byte[] imageData = File.ReadAllBytes(fullPath);

        // ������� ��������
        Texture2D texture = new Texture2D(2, 2); // ������ �� ����� � ����� ������ ��� LoadImage
        if (!texture.LoadImage(imageData))
        {
            Debug.LogError("�� ������� ��������� �����������: " + fullPath);
            return null;
        }

        // ������� ������
        Sprite sprite = Sprite.Create(
            texture,
            new Rect(0, 0, texture.width, texture.height),
            new Vector2(0.5f, 0.5f), // pivot �� ������
            100f                     // pixels per unit � ������� �� ��������
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

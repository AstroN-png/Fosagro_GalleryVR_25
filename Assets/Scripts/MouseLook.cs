using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    private float xRotation = 0f;
    private float yRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Вращаем камеру по X (вверх-вниз) и по Y (влево-вправо)
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        // Если playerBody должен только вращаться по Y (например, персонаж), используйте:
        // playerBody.localRotation = Quaternion.Euler(0f, yRotation, 0f);
    }
}


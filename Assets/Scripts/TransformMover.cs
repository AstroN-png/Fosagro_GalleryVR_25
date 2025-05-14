using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformMover : MonoBehaviour
{
    public static TransformMover instance;

    [Header("Целевые точки")]
    public List<Transform> targets = new List<Transform>();

    [Header("Настройки движения")]
    public float moveSpeed = 2f;
    public float rotateSpeed = 5f;
    public float stopDistance = 0.01f;

    // Для отслеживания активных перемещений по объектам
    private Dictionary<Transform, Coroutine> activeMoves = new Dictionary<Transform, Coroutine>();

    /// <summary>
    /// Плавно перемещает указанный объект к целевой точке из списка по индексу.
    /// </summary>
    /// <param name="movingObject">Объект, который будет двигаться</param>
    /// <param name="targetIndex">Индекс целевой точки</param>

    private void Awake()
    {
        instance = this;
    }

    public void MoveToTarget(Transform movingObject, int targetIndex)
    {
        if (targetIndex < 0 || targetIndex >= targets.Count)
        {
            Debug.LogWarning("Неверный индекс цели: " + targetIndex);
            return;
        }

        Transform target = targets[targetIndex];

        if (activeMoves.ContainsKey(movingObject))
        {
            StopCoroutine(activeMoves[movingObject]);
            activeMoves.Remove(movingObject);
        }

        Coroutine moveRoutine = StartCoroutine(MoveTo(movingObject, target));
        activeMoves[movingObject] = moveRoutine;
    }

    private IEnumerator MoveTo(Transform movingObject, Transform target)
    {
        while (true)
        {
            // Плавное перемещение
            movingObject.position = Vector3.MoveTowards(movingObject.position, target.position, moveSpeed * Time.deltaTime);

            // Плавный поворот
            Quaternion targetRotation = Quaternion.LookRotation(target.forward, Vector3.up);
            movingObject.rotation = Quaternion.Slerp(movingObject.rotation, targetRotation, rotateSpeed * Time.deltaTime);

            // Проверка на завершение
            if (Vector3.Distance(movingObject.position, target.position) <= stopDistance &&
                Quaternion.Angle(movingObject.rotation, target.rotation) <= 0.1f)
            {
                movingObject.position = target.position;
                movingObject.rotation = target.rotation;
                break;
            }

            yield return null;
        }

        // Удаление записи после завершения движения
        if (activeMoves.ContainsKey(movingObject))
            activeMoves.Remove(movingObject);
    }
}


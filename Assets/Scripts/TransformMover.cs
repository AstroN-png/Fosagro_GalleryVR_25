using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformMover : MonoBehaviour
{
    public static TransformMover instance;

    [Header("������� �����")]
    public List<Transform> targets = new List<Transform>();
    public List<Transform> filialsObj = new List<Transform>();

    [Header("��������� ��������")]
    public float moveSpeed = 2f;
    public float rotateSpeed = 5f;
    public float stopDistance = 0.01f;

    // ��� ������������ �������� ����������� �� ��������
    private Dictionary<Transform, Coroutine> activeMoves = new Dictionary<Transform, Coroutine>();

    /// <summary>
    /// ������ ���������� ��������� ������ � ������� ����� �� ������ �� �������.
    /// </summary>
    /// <param name="movingObject">������, ������� ����� ���������</param>
    /// <param name="targetIndex">������ ������� �����</param>

    private void Awake()
    {
        instance = this;
    }

    public void AllToDefault()
    {
        //StopAllCoroutines();
        for (int i = 0; i < filialsObj.Count; i++)
        {
            MoveToTarget(filialsObj[i], i);
        }
    }

    public void MoveToTarget(Transform movingObject, int targetIndex)
    {
        if (targetIndex < 0 || targetIndex >= targets.Count)
        {
            Debug.LogWarning("�������� ������ ����: " + targetIndex);
            return;
        }

        Transform target = targets[targetIndex];

        if (activeMoves.ContainsKey(movingObject))
        {
           // StopCoroutine(activeMoves[movingObject]);
            activeMoves.Remove(movingObject);
        }

        Coroutine moveRoutine = StartCoroutine(MoveTo(movingObject, target));
        activeMoves[movingObject] = moveRoutine;
    }

    private IEnumerator MoveTo(Transform movingObject, Transform target)
    {
        while (true)
        {
            // ������� �����������
            movingObject.position = Vector3.MoveTowards(movingObject.position, target.position, moveSpeed * Time.deltaTime);

            // ������� �������
            Quaternion targetRotation = Quaternion.LookRotation(target.forward, Vector3.up);
            movingObject.rotation = Quaternion.Slerp(movingObject.rotation, targetRotation, rotateSpeed * Time.deltaTime);

            // �������� �� ����������
            if (Vector3.Distance(movingObject.position, target.position) <= stopDistance &&
                Quaternion.Angle(movingObject.rotation, target.rotation) <= 0.1f)
            {
                movingObject.position = target.position;
                movingObject.rotation = target.rotation;
                break;
            }

            yield return null;
        }

        // �������� ������ ����� ���������� ��������
        if (activeMoves.ContainsKey(movingObject))
            activeMoves.Remove(movingObject);
    }
}


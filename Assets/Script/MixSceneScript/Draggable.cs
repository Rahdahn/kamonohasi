using System;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    public Dictionary<MonoBehaviour, Vector2> snapPositions;
    public Action<MonoBehaviour, Action> onDropSuccess;
    public Action<Action> onDropFail;

    private Vector2 originalPosition;

    private void Start()
    {
        originalPosition = transform.position;
    }

    public void OnDrop(MonoBehaviour dropArea)
    {
        if (snapPositions.ContainsKey(dropArea))
        {
            Vector2 snapPosition = snapPositions[dropArea];
            if (onDropSuccess != null)
            {
                onDropSuccess.Invoke(dropArea, ResetPosition);
            }
        }
        else
        {
            if (onDropFail != null)
            {
                onDropFail.Invoke(ResetPosition);
            }
        }
    }

    private void ResetPosition()
    {
        transform.position = originalPosition;
    }
}

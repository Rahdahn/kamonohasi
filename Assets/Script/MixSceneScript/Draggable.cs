using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector2 originalPosition;
    private CanvasGroup canvasGroup;

    // snapToPosition ���O������ݒ�\�ɂ���
    private Transform snapToPosition;

    // �h���b�O�I�����̃R�[���o�b�N
    public Action<MonoBehaviour, Action> onDropSuccess;
    public Action<Action> onDropFail;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        // �����ʒu���L�^
        originalPosition = transform.position;
    }

    // �O������ snapToPosition ��ݒ肷�郁�\�b�h
    public void SetSnapPosition(Transform snapTransform)
    {
        snapToPosition = snapTransform;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = transform.position;
        if (canvasGroup != null)
        {
            canvasGroup.blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (canvasGroup != null)
        {
            canvasGroup.blocksRaycasts = true;
        }

        if (snapToPosition != null)
        {
            transform.position = snapToPosition.position;

            // DropAreaManager �Ȃǂɒʒm���ď������s��
            // �����ł͗�Ƃ��� onDropSuccess ���Ăяo���܂�
            // ���ۂ̎����ɉ����ēK�؂ɐݒ肵�Ă�������
            // �Ⴆ�΁ADropArea �𔻒肵�Ă��̃R�[���o�b�N���Ăяo���Ȃ�
            onDropSuccess?.Invoke(null, null);
        }
        else
        {
            // snapToPosition ���ݒ肳��Ă��Ȃ��ꍇ�͌��̈ʒu�ɖ߂�
            transform.position = originalPosition;

            onDropFail?.Invoke(() => transform.position = originalPosition);
        }
    }
}

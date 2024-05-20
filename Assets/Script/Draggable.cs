using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    // ���̃I�u�W�F�N�g�̌��̈ʒu
    private Vector2 prePos;

    // ���̃I�u�W�F�N�g�̌��̐e
    private GameObject preParent;

    // �h���b�v�\�G���A
    public List<MonoBehaviour> dropArea;

    // �h���b�O�J�n���Ɏ��s����A�N�V����
    public Action beforeBeginDrag;

    // �h���b�v�������Ɏ��s����A�N�V����
    public Action<MonoBehaviour, Action> onDropSuccess;

    // �h���b�v�\�G���A�ȊO�Ƀh���b�v���ꂽ�Ƃ��̏���
    public Action<Action> onDropFail;

    // �h���b�v�G���A�ɃX�i�b�v����ʒu��ݒ�
    public Dictionary<MonoBehaviour, Vector2> snapPositions;

    public void OnBeginDrag(PointerEventData eventData)
    {
        // �h���b�O�J�n���Ɏ��s����A�N�V���������s
        beforeBeginDrag?.Invoke();

        // ���̃I�u�W�F�N�g�̌��̈ʒu�Ɛe��\�ߕۑ�
        prePos = transform.position;
        preParent = transform.parent.gameObject;

        // �ŏ�ʂɈړ�
        transform.SetParent(transform.root.gameObject.transform, true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Screen Space Camera�̏ꍇ�A�J�����̈ʒu����̍��������ړ�������
        Vector3 vec = Camera.main.WorldToScreenPoint(transform.position);
        vec.x += eventData.delta.x;
        vec.y += eventData.delta.y;
        transform.position = Camera.main.ScreenToWorldPoint(vec);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        bool isSuccess = false;
        foreach (MonoBehaviour area in dropArea)
        {
            if (contains(area.GetComponent<RectTransform>(), eventData))
            {
                // �h���b�v�\�G���A�ɂ��̃I�u�W�F�N�g���܂܂��ꍇ
                isSuccess = true;
                if (snapPositions != null && snapPositions.ContainsKey(area))
                {
                    // �X�i�b�v�ʒu���ݒ肳��Ă���ꍇ�A���̈ʒu�Ɉړ�
                    transform.position = snapPositions[area];
                }
                onDropSuccess?.Invoke(area, resetPos());
                break;
            }
        }

        // ���s������
        if (!isSuccess)
        {
            if (onDropFail == null)
            {
                // ���s���A�N�V���������ݒ�̏ꍇ�A�ʒu�����Ƃɖ߂�
                resetPos().Invoke();
            }
            else
            {
                // �A�N�V�����ݒ�ς݂Ȃ炻������s
                onDropFail.Invoke(resetPos());
            }
        }
    }

    private Action resetPos()
    {
        return () =>
        {
            // �ʒu�����Ƃɖ߂�
            transform.position = prePos;
            transform.SetParent(preParent.transform, true);
        };
    }

    // target��area�͈͓̔��ɂ��邩�ǂ����𔻒肷��
    private bool contains(RectTransform area, PointerEventData target)
    {
        var selfBounds = GetBounds(area);
        Vector3 worldPos;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(
            area,
            target.position,
            target.pressEventCamera,
            out worldPos);
        worldPos.z = 0f;
        return selfBounds.Contains(worldPos);
    }

    private Bounds GetBounds(RectTransform target)
    {
        Vector3[] s_Corners = new Vector3[4];
        var min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
        var max = new Vector3(float.MinValue, float.MinValue, float.MinValue);
        target.GetWorldCorners(s_Corners);
        for (var index2 = 0; index2 < 4; ++index2)
        {
            min = Vector3.Min(s_Corners[index2], min);
            max = Vector3.Max(s_Corners[index2], max);
        }

        max.z = 0f;
        min.z = 0f;

        Bounds bounds = new Bounds(min, Vector3.zero);
        bounds.Encapsulate(max);
        return bounds;
    }
}

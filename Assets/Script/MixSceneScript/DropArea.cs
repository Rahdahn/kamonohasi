using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // �X���C�_�[�p�̖��O��Ԃ�ǉ�

public class DropArea : MonoBehaviour
{
    // �h���b�v�\�I�u�W�F�N�g���X�g
    public List<Draggable> dropObjs;

    // �h���b�v�G���A�i�G�f�B�^�ŃA�^�b�`�j
    public MonoBehaviour dropArea1;
    public MonoBehaviour dropArea2;
    public MonoBehaviour dropArea3;
    public MonoBehaviour dropArea4;

    // sliderMove1�i�G�f�B�^�ŃA�^�b�`�j
    public SliderMove1 sliderMove1;

    // �e�h���b�v�G���A�̏�Ԃ�ǐՂ��鎫��
    private Dictionary<MonoBehaviour, bool> dropAreaOccupied;

    void Start()
    {
        dropAreaOccupied = new Dictionary<MonoBehaviour, bool>
        {
            { dropArea1, false },
            { dropArea2, false },
            { dropArea3, false },
            { dropArea4, false }
        };

        foreach (var dropObj in dropObjs)
        {
            // �h���b�v�G���A�ƃX�i�b�v�ʒu�̐ݒ�
            dropObj.snapPositions = new Dictionary<MonoBehaviour, Vector2>
            {
                { dropArea1, GetSnapPosition(dropArea1) },
                { dropArea2, GetSnapPosition(dropArea2) },
                { dropArea3, GetSnapPosition(dropArea3) },
                { dropArea4, GetSnapPosition(dropArea4) }
            };

            dropObj.onDropSuccess = (MonoBehaviour area, Action resetAction) =>
            {
                Debug.Log("�h���b�O������");

                if (dropAreaOccupied[area])
                {
                    Debug.Log("���̃G���A�͂��łɎg�p����Ă��܂��B");
                    resetAction.Invoke();
                }
                else
                {
                    dropAreaOccupied[area] = true;
                    // �I�u�W�F�N�g���X�i�b�v�ʒu�Ɉړ�
                    dropObj.transform.position = GetSnapPosition(area);

                    // ���ׂẴh���b�v�G���A�����܂������`�F�b�N
                    CheckAllDropAreasFilled();
                }
            };
            dropObj.onDropFail = (Action resetAction) =>
            {
                Debug.Log("�h���b�O���s��");
                resetAction.Invoke();
            };
        }
    }

    Vector2 GetSnapPosition(MonoBehaviour dropArea)
    {
        return dropArea.transform.position;
    }

    void CheckAllDropAreasFilled()
    {
        foreach (var occupied in dropAreaOccupied.Values)
        {
            if (!occupied)
            {
                return; // �܂����܂��Ă��Ȃ��G���A������ꍇ�͏I��
            }
        }
        // ���ׂẴG���A�����܂��Ă���ꍇ�AsliderMove1���N��
        StartsliderMove1();
    }

    void StartsliderMove1()
    {
        sliderMove1.gameObject.SetActive(true); // sliderMove1���A�N�e�B�u�ɂ���
    }
}

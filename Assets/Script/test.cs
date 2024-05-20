using System;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // �h���b�v�\�I�u�W�F�N�g
    public Draggable dropObj;

    // �h���b�v�G���A1��2�i�G�f�B�^�ŃA�^�b�`�j
    public MonoBehaviour dropArea1;
    public MonoBehaviour dropArea2;

    void Start()
    {
        // �h���b�v�G���A�ƃX�i�b�v�ʒu�̐ݒ�
        dropObj.snapPositions = new Dictionary<MonoBehaviour, Vector2>
        {
            { dropArea1, new Vector2(100, 100) }, // �X�i�b�v�ʒu1
            { dropArea2, new Vector2(200, 200) }  // �X�i�b�v�ʒu2
        };

        dropObj.beforeBeginDrag = () =>
        {
            Debug.Log("�h���b�O�O�ɌĂяo����鏈��");
        };
        dropObj.onDropSuccess = (MonoBehaviour area, Action resetAction) =>
        {
            Debug.Log("�h���b�O�������ɌĂяo����鏈��");
            // resetAction.Invoke(); // �X�i�b�v�����邽�߁A�����ł̓��Z�b�g���Ăяo���Ȃ�
        };
        dropObj.onDropFail = (Action resetAction) =>
        {
            Debug.Log("�h���b�O���s���ɌĂяo����鏈��");
            resetAction.Invoke();
        };
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
//��ü�̼��� ��� �ϼ��Ͽ� ī��Ʈ���� ���϶� �����߾�並 ����
public class Answer : MonoBehaviour
{
    [SerializeField]
    GameObject pico;
    [SerializeField]
    GameObject zoom;
    [SerializeField]
    Image zoomPosition;
    [SerializeField]
    GameObject invisible;
    [SerializeField]
    GameObject star;
    Animator animator;
    void Start()
    {
        GetComponent<ForCount>().Connect += AnswerIs;
    }
    void AnswerIs(object sender, CountParameter e)
    {
        if (e.count == 3)
        {
            SoundInterface.instance.SoundPlay(5);
            //SoundInterface.instance.SoundPlay(6);
            print("�̼ǳ� : ���� win(hi-host) �ִϸ��̼� �÷���");
            StartCoroutine(Delay_forZoom());
            animator = pico.GetComponent<Animator>();
            animator.SetInteger("PicoAction", 3); //���ھִϸ��̼� �߿� 3�� hi-host�ѱ�
            star.SetActive(false);
        }
    }
    //�̼ǿϼ��� �����Ⱑ ���� õõ�� ���ڸ��� ���Բ�
    IEnumerator Delay_forZoom()
    {
        yield return new WaitForSeconds(2.5f);
        zoom.transform.position = zoomPosition.transform.position;
        zoom.GetComponent<Image>().raycastTarget = false;
        yield break;
    }
}

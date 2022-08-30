using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//������� �巡�� �� �� ��ũ��Ʈ�� ����ִ� ������Ʈ ���� ���콺�����Ͱ� ��⸸ �ص� �����ؼ� �����Դܾ �´ٴ� ��Ʈ�� �ִ� �ִϸ��̼� ��� 
//�����Դܾ��� ������Ʈ�� �� ��ũ��Ʈ �־��ֱ�
public class PointerEnterCheckHandle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Animator animator;
    public void OnPointerEnter(PointerEventData eventData)
    {
        print("����üũ");
        if (GetComponent<Image>().name.Contains("Giyeok"))
        {
            GetComponent<Animator>().enabled = true;
            animator = GetComponent<Animator>();
            animator.SetInteger("BigAction", 1);
        }
        if (GetComponent<Image>().name.Contains("Cloud"))
        {
            print("���� Ȯ�ο�");
            //�ִϸ��̼��� ������ ó����ġ�� ���ư��� ������ ���� �ذ��� ���� ���� ����ġ�� ���� �̵������ص� �ִϸ��̼��� Ʋ�������
            //transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        print("���콺����Ʈ �������� üũ");
        //animator = GetComponent<Animator>();
        //animator.SetInteger("BigAction", default);
        GetComponent<Animator>().enabled = false;
    }
}

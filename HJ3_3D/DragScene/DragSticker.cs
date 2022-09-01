using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class DragSticker : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    #region ����
    [SerializeField]
    GameObject again; //�ٽ��غ����
    //ForCount��ũ��Ʈ �־��ֱ�
    [SerializeField]
    ForCount countClass; //ForCount��ũ��Ʈ
    [SerializeField]
    GameObject ex; //��ƼĿ�� �ٿ�����
    [SerializeField]
    GameObject star; //�Ϸ�� ��
    Vector3 startPosition;

    [SerializeField]
    GameObject invisible; //���ͷ��� ��Ȱ��ȭ�� �����̹���

    public NewRandomTry randomTry;
    #endregion
    void Awake() //Answer��ũ��Ʈ�� AnswerIs�Լ����� ���� ����Ǳ� ���� ������ Start���ٰ� Start���� �ο򳪼� Start���� ��������Ǵ� Awake�� �ٲ㼭 ������ذ�
    {
        ex.SetActive(true);
        //�̺�Ʈ �ڵ鷯 ������� �ٲ� : ���ٽ� ���·� ���� ��
        countClass.Connect += (c, index) => //ForCount��ũ��Ʈ�� Connect�̺�Ʈ�ڵ鷯�� ���ٽ� �Լ�(�Ű�����2���� ������ �ִ� �̸����� �Լ�)�� �־���(+=)
        {
            if (index.transformIndex == transform.GetSiblingIndex()) //����������Ʈ���� 0������ �����ϴ� �ε����� Ȱ��
            {
                print("���߾��");
                StopAllCoroutines();
                gameObject.transform.position = index.gameObject.transform.position; //�浹�� ������Ʈ�� ��ġ�� �ٲ���
                index.gameObject.SetActive(false);
                GetComponent<Image>().raycastTarget = false;
                again.SetActive(false);
            }
        };
        startPosition = transform.position; //��ġ ���� //cf)startPosition = eventData.position;
        //�Լ� �ٱ��Ͽ� ��� �Լ��� ���� ���ΰ�? : ������ ���� : �Լ��� ���´� ���ƾ� �� : �Ű�����, ���� ���� ���ƾ� ��       
    }
    #region �Լ�
    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetAsLastSibling(); //������
        StopAllCoroutines();
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.pressEventCamera.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, 90)); //Screen Space - camera ������ Ondrag������ �Ⱥ��̴� ����:z�� �����ֱ�
    }
    //�巡�� ����� �浹ó���� ���� �ؽ�Ʈ�� �� �ڽĿ�����Ʈ�� ������ �ִ����� �´� ��ġ üũ
    public void OnEndDrag(PointerEventData eventData)
    {
        GraphicRaycaster raycaster = transform.root.GetComponent<GraphicRaycaster>();
        List<RaycastResult> result = new List<RaycastResult>();
        raycaster.Raycast(eventData, result);
        foreach (var a in result)
        {
            if (a.gameObject != gameObject)
            {
                if (a.gameObject.GetComponentInChildren<Text>().text.Contains(gameObject.GetComponent<Image>().sprite.name)) //�浹�� ������Ʈ�� �ڽĿ�����Ʈ �ؽ�Ʈ�� ���Ͽ� ������ ī��Ʈ+1
                {
                    countClass.countParameter.gameObject = a.gameObject;
                    countClass.countParameter.transformIndex = transform.GetSiblingIndex();
                    int index = countClass.Count;
                    countClass.Count++;
                    star.transform.SetAsLastSibling();
                    star.SetActive(true);
                    star.transform.position = a.gameObject.transform.position;
                    invisible.SetActive(true); //�ؿ� �ٿ��� ȣ���ϴ� �� �����Լ����� ���� ��Ȱ��ȭ�� �̹����� Ȱ��ȭ���Ѿ� ���ִϸ��̼��� �����ڸ��� ���ͷ��� ��Ȱ��ȭ����
                    StartCoroutine(randomTry.Speed_forStar(index)); 
                }
                else
                {
                    StartCoroutine(Speed_forStiker()); //Ʋ���� ��� �ٽ� ����ġ�� õõ�� �̵�
                    again.SetActive(true); //again�̹��� Ȱ��ȭ
                }
            }
            else
            {
                StartCoroutine(Speed_forStiker());
                again.SetActive(true); 
            }
            //Invoke("play", 1f); //invoke�� �ǵ��� ���� ����
        }
        
    }
    void play() //invoke�� �Լ������� ȣ���ؼ� ���Ƿ� �Լ��� �����а�
    {
        again.SetActive(false);
    }
    // ��ƼĿ �ѻ��� �Ÿ��� �缭 �ѻ����� �Ÿ��� ��������� �� ���� ��ٷȴٰ� ���Բ����ִ� ���� �Լ�
    IEnumerator Speed_forStiker()   
    {
        while(Vector3.Distance(transform.position, startPosition) > 0) //�ѻ����� �Ÿ��� �ִ� ����
        {
            transform.position = Vector3.Lerp(transform.position, startPosition, Time.deltaTime*10); 
            yield return new WaitForSeconds(Time.deltaTime);
            if(Vector3.Distance(transform.position, startPosition) <= 0)
            {
                break;
            }
        }
        transform.position = startPosition;
        yield break;
    }
    #endregion
}





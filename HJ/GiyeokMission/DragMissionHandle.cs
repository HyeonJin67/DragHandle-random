using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//�����⿡ �� �巡�ױ�� �Ѱ� ��ũ��Ʈ
public class DragMissionHandle : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    #region ����
    [SerializeField]
    ForCount forCount; //ForCount��ũ��Ʈ �־��ֱ�
    public GiyeokMissionManager randomTry; //������ ����ó�� ����, �Ҹ� ������ ����ϴ� ��ũ��Ʈ //Scriptable Object���� ���� ���ϴ� ��ũ��Ʈ���� �ʿ�� �ϴ� ������Ʈ���� ������ ����
    [SerializeField]
    GameObject hand; //�巡�׸� �����ϴ� �� �ִϸ��̼ǿ�
    [SerializeField]
    GameObject invisible; //���ͷ��� ��Ȱ��ȭ�� �����̹���
    [SerializeField]
    GameObject pico;
    [SerializeField]
    Image zoomPosition; //�����Ⱑ ������ ��
    [SerializeField]
    Image cloud;
    [Tooltip("�� ���� ������ �̸��� �Է����ּ��� ex)Giyeok")]
    [SerializeField]
    string obName; //������Ʈ�� �Ǻ��� ������ �̸��� �Է¹޴� ��

    Vector3 startPosition; //�����Ⱑ ���� �ڸ��� ���ư��� ���� ó�� ��ġ �����
    int count = 0; //�巡�װ� ������ �⿪���� �ܾ �Ǻ��� �� �ִ� �뵵
    private Animator animator; //Ư�� �ִϸ��̼��� �������ֱ� ���� �뵵
    #endregion

    void Awake() //Answer��ũ��Ʈ�� AnswerIs�Լ����� ���� ����Ǳ� ���� ������ Start���ٰ� Start���� �ο򳪼� Start���� ��������Ǵ� Awake�� �ٲ㼭 ������ذ�
    {
        animator = cloud.GetComponent<Animator>();
        animator.SetInteger("BigAction", 2); //������ �ϴÿ��� �����̴� �ִϸ��̼� �ѱ�  
        animator = pico.GetComponent<Animator>();
        animator.SetInteger("PicoAction", 5); //���ھִϸ��̼� �߿� 5�� Walk�ѱ�
        StartCoroutine(Speed_StartZoom());
        //�̺�Ʈ �ڵ鷯 ������� �ٲ� : ���ٽ� ���·� ���� ��
        forCount.Connect += (c, index) => //ForCount��ũ��Ʈ�� Connect�̺�Ʈ�ڵ鷯�� ���ٽ� �Լ�(�Ű�����2���� ������ �ִ� �̸����� �Լ�)�� �־���(+=)
        {
            if (index.transformIndex == transform.GetSiblingIndex()) //����������Ʈ���� 0������ �����ϴ� �ε����� Ȱ��
            {
                print("���߾��");
                StopAllCoroutines();
                //gameObject.transform.position = index.gameObject.transform.position; //�浹�� ������Ʈ�� ��ġ�� �ٲ���
                //index.gameObject.SetActive(false);
                //GetComponent<Image>().raycastTarget = false;
            }
        };
        startPosition = zoomPosition.transform.position; //��ġ ���� //cf)startPosition = eventData.position;
        //�Լ� �ٱ��Ͽ� ��� �Լ��� ���� ���ΰ�? : ������ ���� : �Լ��� ���´� ���ƾ� �� : �Ű�����, ���� ���� ���ƾ� ��       
    }
    #region �Լ�
    public void OnBeginDrag(PointerEventData eventData)
    {
        count = 0; //�巡�װ� ������ �ٽ� �巡�װ� ���۵� ���� "�����Դܾ ����������" �浹üũ�� ���� count�� ����
        hand.SetActive(false);
        StopAllCoroutines();
        animator = pico.GetComponent<Animator>();
        animator.SetInteger("PicoAction", 4); //���ھִϸ��̼� �߿� 4�� idle(�θ����θ���)�ѱ�
        GetComponent<Animator>().enabled = true; //�巡�� ���۽� �����Ⱑ Ŀ���� �ִϸ��̼� ���ֱ�
        transform.GetChild(0).GetComponent<Image>().gameObject.SetActive(true); //����ũ������ ù��° �ڽĿ�����Ʈ �Ѽ� �巡�װ� ���۵Ǹ� ������ �ָ� ��Ӱ� �����
    }
    public void OnDrag(PointerEventData eventData)
    {
        //�巡�� �� ������Ʈ ������Բ�
        transform.position = eventData.pressEventCamera.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, 90)); //Screen Space - camera ������ Ondrag������ �Ⱥ��̴� ����:z�� ����
    }
    //�巡�� ����� �浹ó���� ���� �ؽ�Ʈ�� �� �ڽĿ�����Ʈ�� ������ �ִ����� �´� ��ġ üũ
    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject ob = MeetObject(eventData); //�浹üũ�ϴ� �Լ� ȣ��
        if (ob != null)
        {
            print("�浹üũ" + ob.name);
            if (ob.gameObject.name.Contains(obName)) //�Է¹��� ������ �����ϴ� �ܾ ����� ���
            {
                invisible.transform.SetAsLastSibling(); 
                invisible.SetActive(true); //����ܾ��� �ִϸ��̼ǰ� �� �̵��� ������ ���� ��Ȱ��ȭ��
                count++;
                if (count == 1)
                {
                    print("�����Դܾ� Ȯ��");
                    if (ob.gameObject.name.Contains("Cloud"))
                    {
                        print("�̵�Ȯ��");
                        ob.transform.position = transform.position;  
                        //ob.transform.position = new Vector3(transform.position.x, transform.position.y, 0); //���ڸ��� �̵����Ѽ� �ִϸ��̼� �ѱ�
                    }
                    ob.GetComponent<Animator>().enabled = true;
                    animator = pico.GetComponent<Animator>();
                    animator.SetInteger("PicoAction", 1); //���ھִϸ��̼� �߿� 1�� win�ѱ�
                    animator = ob.GetComponent<Animator>();
                    animator.SetInteger("BigAction", 1); //������ �� ����Ǵ� ������ �ִϸ��̼� ���� �� ���
                    //�̺�Ʈ�ڵ鷯�� �ܾ�� ī��Ʈ
                    forCount.countParameter.gameObject = ob.gameObject;
                    forCount.countParameter.transformIndex = transform.GetSiblingIndex();
                    int index = forCount.Count;
                    forCount.Count++;
                    StartCoroutine(randomTry.Speed_forStar(index, ob)); //�¾��� ���� �� �θ���
                    ob.GetComponent<Image>().raycastTarget = false; //������ �� ���̻� �浹ó���Ǽ� �νĵ��� �ʰ� 
                }
            }
            else //Ʋ���ܾ ����� ���
            {
                animator = pico.GetComponent<Animator>();
                animator.SetInteger("PicoAction", 2); //���ھִϸ��̼� �߿� 2�� lose�ѱ�
                StartCoroutine(Speed_forZoom());
            }
        }
    }

    //�����Ⱑ �� ������ õõ�� ������ ������ִ� ���� �Լ�
    IEnumerator Speed_StartZoom()
    {
        invisible.SetActive(true);
        while (Vector3.Distance(transform.position, zoomPosition.transform.position) > 10)//�ѻ����� �Ÿ��� �ִ� ���� //÷�� 0���� �ߴٰ� �ʹ� ������ 10���� �ٲ�
        {
            transform.position = Vector3.Lerp(transform.position, zoomPosition.transform.position, Time.deltaTime* 0.7f);
            yield return new WaitForSeconds(Time.deltaTime); //���ڸ��� ���ư��� �ӵ� �����ϴ� ��
            if (Vector3.Distance(transform.position, zoomPosition.transform.position) <= 10)
            {
                break;
            }
        }
        transform.position = zoomPosition.transform.position;
        invisible.SetActive(false);
        hand.SetActive(true);
        yield break;
    }
    //�浹 üũ�� ���� �Լ�
    GameObject MeetObject(PointerEventData eventData)
    {
        GraphicRaycaster raycaster = transform.root.GetComponent<GraphicRaycaster>();
        List<RaycastResult> result = new List<RaycastResult>();
        raycaster.Raycast(eventData, result);
        foreach (var a in result)
        {
            if (a.gameObject != gameObject) //�ڱ��ڽ��� �ƴ� ��쿡��
            {
                print("�ڱ��ڽ��� �ƴѰ�");
                return a.gameObject;
            }
        }
        return null;
    }
    //Ʋ���� ��쿡 �����Ⱑ ���ڸ��� õõ�� ���ư��� ���ִ� �����Լ�
    IEnumerator Speed_forZoom()
    {
        yield return new WaitForSeconds(0.5f); //0.5������ ��ٷȴٰ� �̵�
        while (Vector3.Distance(transform.position, startPosition) > 10) //�ѻ����� �Ÿ��� �ִ� ���� //÷�� 0���� �ߴٰ� �ʹ� ������ 10���� �ٲ�
        {
            transform.position = Vector3.Lerp(transform.position, startPosition, Time.deltaTime*10); //����ϴ� ���� ������ ���� Lerp
            yield return new WaitForSeconds(Time.deltaTime * 2f); //���ڸ��� ���ư��� �ӵ� �����ϴ� ��
            if (Vector3.Distance(transform.position, startPosition) <= 10)
            {
                break;
            }
        }
        transform.position = startPosition;
        print("���ڸ��� ���ư��� �Ϸ�");
        yield break; 
    }
    #endregion
}

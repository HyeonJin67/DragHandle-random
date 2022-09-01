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
    ForCount forCount; //����ó�� ���� �̼� ���� ī��Ʈ�� ��ũ��Ʈ
    public GiyeokMissionManager randomTry; //������ ����ó�� ����, �Ҹ� ������ ����ϴ� ��ũ��Ʈ //Scriptable Object���� ���� ���ϴ� ��ũ��Ʈ���� �ʿ�� �ϴ� ������Ʈ���� ������ ����
    public ZoomDelayHandle delayFunc; //�������� �Լ� ���� ��ũ��Ʈ
    [SerializeField]
    GameObject invisible; //���ͷ��� ��Ȱ��ȭ�� �����̹���
    [SerializeField]
    GameObject pico;
    [SerializeField]
    Image zoomPosition; //�����Ⱑ ������ ��
    [SerializeField]
    GameObject hand; //�巡�׸� �����ϴ� �� �ִϸ��̼ǿ�
    [SerializeField]
    Image cloud;
    [SerializeField]
    Image cloudPosition; //������ ��� ���ƿ��� ���� ��ġ
    [Tooltip("�� ���� ������ �̸��� �Է����ּ��� ex)Giyeok")]
    [SerializeField]
    string obName; //������Ʈ�� �Ǻ��� ������ �̸��� �Է¹޴� ��
    Vector3 startPosition; //�����Ⱑ ���� �ڸ��� ���ư��� ���� ó�� ��ġ �����
    int count = 0; //�巡�װ� ������ �⿪���� �ܾ �Ǻ��� �� �ִ� �뵵
    private Animator animator; //Ư�� �ִϸ��̼��� �������ֱ� ���� �뵵
    GameObject savedOb;
    #endregion
    #region �̺�Ʈ
    //public event System.Action<PointerEventData> OnPointerFunction;
    #endregion
    void Awake() //Answer��ũ��Ʈ�� AnswerIs�Լ����� ���� ����Ǳ� ���� ������ Start���ٰ� Start���� �ο򳪼� Start���� ��������Ǵ� Awake�� �ٲ㼭 ������ذ�
    {
        SoundInterface.instance.SoundPlay(0);
        StartCoroutine(Speed_forCloud());
        animator = pico.GetComponent<Animator>();
        animator.SetInteger("PicoAction", 5); //���ھִϸ��̼� �߿� 5�� Walk�ѱ�
        StartCoroutine(delayFunc.Speed_StartZoom());
        //�̺�Ʈ �ڵ鷯 ������� �ٲ� : ���ٽ� ���·� ���� ��
        forCount.Connect += (c, index) => //ForCount��ũ��Ʈ�� Connect�̺�Ʈ�ڵ鷯�� ���ٽ� �Լ�(�Ű�����2���� ������ �ִ� �̸����� �Լ�)�� �־���(+=)
        {
            if (index.transformIndex == transform.GetSiblingIndex()) //����������Ʈ���� 0������ �����ϴ� �ε����� Ȱ��
            {
                print("���߾��");
                //StopAllCoroutines();
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
        SoundInterface.instance.SoundPlay(2);
        count = 0; //�巡�װ� ������ �ٽ� �巡�װ� ���۵� ���� "�����Դܾ ����������" �浹üũ�� ���� count�� ����
        hand.SetActive(false);
        //StopAllCoroutines();
        animator = pico.GetComponent<Animator>();
        animator.SetInteger("PicoAction", 4); //���ھִϸ��̼� �߿� 4�� idle(�θ����θ���)�ѱ�
        GetComponent<Animator>().enabled = true; //�巡�� ���۽� �����Ⱑ Ŀ���� �ִϸ��̼� ���ֱ�
        transform.GetChild(0).GetComponent<Image>().gameObject.SetActive(true); //����ũ������ ù��° �ڽĿ�����Ʈ �Ѽ� �巡�װ� ���۵Ǹ� ������ �ָ� ��Ӱ� �����
    }
    public void OnDrag(PointerEventData eventData)
    {
        //�巡�� �� ������Ʈ ������Բ�
        transform.position = eventData.pressEventCamera.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, 90)); //Screen Space - camera ������ Ondrag������ �Ⱥ��̴� ����:z�� ����
        GameObject ob = MeetObject(eventData); //�浹üũ�ϴ� �Լ� ȣ��
        if (ob != null)
        {
            if (ob.gameObject.name.Contains(obName)) //�Է¹��� ������ �����ϴ� �ܾ ����� ���
            {
                if(savedOb != null && savedOb.GetComponent<Image>().raycastTarget == true && savedOb.gameObject.name.Contains(obName))
                {
                    animator = savedOb.GetComponent<Animator>();
                    animator.SetInteger("BigAction", 2);
                }
                savedOb = ob;
                animator = ob.GetComponent<Animator>();
                animator.SetInteger("BigAction", 1); //������ �� ����Ǵ� ������ �ִϸ��̼� ���� �� ���
                //print("savedOb1" + savedOb);
            }
            else if (savedOb != null && savedOb.GetComponent<Image>().raycastTarget == true)
            {
                print("savedOb2" + savedOb);
                animator = savedOb.GetComponent<Animator>();
                animator.SetInteger("BigAction", 2);
            }
        }
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
                SoundInterface.instance.SoundPlay(3);
                //SoundInterface.instance.SoundPlay(6);
                invisible.transform.SetAsLastSibling(); 
                invisible.SetActive(true); //����ܾ��� �ִϸ��̼ǰ� �� �̵��� ������ ���� ��Ȱ��ȭ��
                count++;
                if (count == 1)
                {
                    print("�����Դܾ� Ȯ��");
                    if (ob.gameObject.name.Contains("Cloud")) //������ �ϴÿ��� �����̴°� ���߱� ����
                    {
                        //StopCoroutine(Speed_forCloud()); //StopCoroutine("Speed_forCloud"); //"���� ��ũ��Ʈ"�� �ִ� "Ư��" �ڷ�ƾ ���� //�ٵ� �ȵ�
                        StopAllCoroutines(); //"���� ��ũ��Ʈ"���� �������� ��� �ڷ�ƾ ����
                    }
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
                SoundInterface.instance.SoundPlay(4);
                //SoundInterface.instance.SoundPlay(7);
                animator = pico.GetComponent<Animator>();
                animator.SetInteger("PicoAction", 2); //���ھִϸ��̼� �߿� 2�� lose�ѱ�
                StartCoroutine(delayFunc.Speed_forZoom(startPosition));
            }
        }
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
    //������ �ϴÿ��� �����̴� �ִϸ��̼� ��� ���� ���� �Լ�
    public IEnumerator Speed_forCloud()
    {
        yield return new WaitForSeconds(0.5f);
        while (Vector3.Distance(cloud.transform.position, cloudPosition.transform.position) > 0.7f)
        {
            cloud.transform.position = Vector3.Lerp(cloud.transform.position, cloudPosition.transform.position, Time.deltaTime * 0.7f);
            yield return new WaitForSeconds(Time.deltaTime * 15f);
            if (Vector3.Distance(cloud.transform.position, cloudPosition.transform.position) <= 0.7f)
            { break; }
        }
        cloud.transform.position = cloudPosition.transform.position;
        yield break;
    }
    #endregion
}

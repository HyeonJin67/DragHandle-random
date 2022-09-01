using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//�����Լ��� ���� ������ ��ũ��Ʈ
public class ZoomDelayHandle : MonoBehaviour
{
    #region ����
    [SerializeField]
    Image zoom;
    [SerializeField]
    Image zoomPosition; //�����Ⱑ ������ ��
    [SerializeField]
    GameObject invisible; //���ͷ��� ��Ȱ��ȭ�� �����̹���
    [SerializeField]
    GameObject hand; //�巡�׸� �����ϴ� �� �ִϸ��̼ǿ�
    #endregion
    #region �����ռ�
    //�����Ⱑ �� ������ õõ�� ������ ������ִ� ���� �Լ�
    public IEnumerator Speed_StartZoom()
    {
        invisible.SetActive(true);
        while (Vector3.Distance(zoom.transform.position, zoomPosition.transform.position) > 8)//�ѻ����� �Ÿ��� �ִ� ���� //÷�� 0���� �ߴٰ� �ʹ� ������ 10���� �ٲ�
        {
            zoom.transform.position = Vector3.Lerp(zoom.transform.position, zoomPosition.transform.position, Time.deltaTime* 0.7f);
            yield return new WaitForSeconds(Time.deltaTime); //���ڸ��� ���ư��� �ӵ� �����ϴ� ��
            if (Vector3.Distance(zoom.transform.position, zoomPosition.transform.position) <= 0.7f)
            {
                break;
            }
        }
        zoom.transform.position = zoomPosition.transform.position;
        invisible.SetActive(false);
        hand.SetActive(true);
        SoundInterface.instance.SoundPlay(1);
        yield break;
    }
    //Ʋ���� ��쿡 �����Ⱑ ���ڸ��� õõ�� ���ư��� ���ִ� �����Լ�
    public IEnumerator Speed_forZoom(Vector3 startposi)
    {
        //yield return new WaitForSeconds(0.5f); //0.5������ ��ٷȴٰ� �̵�
        while (Vector3.Distance(zoom.transform.position, startposi) > 10) //�ѻ����� �Ÿ��� �ִ� ���� //÷�� 0���� �ߴٰ� �ʹ� ������ 10���� �ٲ�
        {
            zoom.transform.position = Vector3.Lerp(zoom.transform.position, startposi, Time.deltaTime*10); //����ϴ� ���� ������ ���� Lerp
            yield return new WaitForSeconds(Time.deltaTime * 2f); //���ڸ��� ���ư��� �ӵ� �����ϴ� ��
            if (Vector3.Distance(zoom.transform.position, startposi) <= 10)
            {
                break;
            }
        }
        zoom.transform.position = startposi;
        print("���ڸ��� ���ư��� �Ϸ�");
        yield break;
    }
    #endregion
}

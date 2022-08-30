using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
//���� �� ����ó��
public class NewRandomTry : MonoBehaviour
{
    #region ����
    /*
    [Tooltip("�������� �ٲ� ��� sprite�� �־��ּ���")]
    [SerializeField]
    Sprite[] EveryImage; //�������� �ٲ� ��� �̹���
    [Tooltip("�̹����� ��Ʈ�� �� ��� ������� ������� �־��ּ���")]
    [SerializeField]
    AudioClip[] EveryAudio; //�̹����� ��Ʈ ��������� //AudioClip�� ����� ���� ��ü //AudioSource�� ������Ʈ
    */
    [Tooltip("�������� �ٲ�� �� �̹����� ��� �־��ּ���")]
    [SerializeField]
    Image[] ui; //�巡���ؼ� �ű� �̹�����
    [Tooltip("�巡���ؼ� �ű� ��ġ�� ��� �־��ּ���")]
    [SerializeField]
    GameObject[] uiPosition; //��ƼĿ���� �巡���ؼ� �浹ó�������� ��ġ�� �����̹���
    [SerializeField]
    GameObject star; //�Ϸ�� ��
    [Tooltip("������ �ʿ��� �̼� �Ϸ� ���� ������ŭ�� �־��ּ���")]
    [SerializeField]
    GameObject[] starPosi; //���� �̵��� ��ġ
    [SerializeField]
    GameObject invisible; //���ͷ��� ��Ȱ��ȭ�� �����̹���
    [SerializeField]
    AudioSource narrations;
    int plus = 0; //�ȹٲ�� ���̹Ƿ� ����� ���濹��
    #endregion
    
    [SerializeField]
    RandomStar randomStar;

    #region �Լ�
    public void Test() //��ư Ŭ���� ȣ��Ǵ� �Լ��� �ν�����â���� ���� �������ֱ�
    {
        var indexArray = Enumerable.Range(0, randomStar.EveryImages_Giyeok.Length).ToArray(); //�ڵ����� �迭 ���·� �������� ���ڵ��� �־��� sprite�� ���� ������� �־���
        System.Random random = new System.Random(); //�ý��ۿ� �ִ� ���� ����
        var shuffleArray1 = indexArray.OrderBy(x => random.Next()).ToArray(); //OrderBy�� �������� �ϰڴٴ� �ǹ� : ������� �̹� �� �ִ� �Ÿ� �������� �����ְ� �� //�ٲ�� �̹�����
        var shuffleArray2 = uiPosition.OrderBy(x => random.Next()).ToArray(); //�ٲ�� �ؽ�Ʈ��
        while (plus < ui.Length) //�־�� ui�� ������ŭ�� �ݺ�
        {
            uiPosition[plus].GetComponent<Image>().enabled = false;
            int x = Random.Range(-30, 30); //����,���� ������ٸ� ���簢���� ���������� ����
            int y = Random.Range(-20, 20);
            ui[plus].sprite = randomStar.EveryImages_Giyeok[shuffleArray1[plus]];
            shuffleArray2[plus].GetComponentInChildren<Text>().text = ui[plus].sprite.name; //����� �̹����� sprite������ �̸����� �ؽ�Ʈ ����
            //�ݰ� 1�� �� �ȿ��� �������� ��ġ ����
            //Vector2 randCirclePos = Random.insideUnitCircle;
            //uiPosition[plus].transform.position = randCirclePos;
            //Screen Space - camera�� �����ؾ� ����� ����
            Vector3 randSquarePos = new Vector3(x, y, 90); //z���� 90���� ����
            uiPosition[plus].transform.position = randSquarePos; //���簢�������� ������ġ�� ����
            //EveryAudio[shuffleArray1[plus]].Play(); //�̹����� �´� ��Ʈ�� �� ����� ���
            //StartCoroutine(StartClip(shuffleArray1[plus])); //����� ����� ���ͷ��� ��Ȱ��ȭ�� �ڷ�ƾ ȣ��
            StartCoroutine(Speed_forPosition(plus));
            ++plus; //���� �ε����� �ѱ�¿�
        }
        plus = 0; //��ư ���������� �ε��� 0���� �ٽ� ����
    }
    // < Wait ���� �����̷� >
    // WaitUntil�� ()������ ���� "��"�� �� ������ ���
    // WaitWhile�� ������ ���� "����"�� �� ������ ���
    //------------------------------------------------------
    //����� ����� ��� ���ͷ���(��ġ�� �巡��) ��Ȱ��ȭ��
    IEnumerator StartClip(int a) 
    {
        invisible.gameObject.SetActive(false);
        //narrations.Stop();
        //narrations.clip = randomStar.EveryAudio[a];
        //narrations.Play();
        //yield return new WaitUntil(() => !narrations.isPlaying); //yield return new WaitUntil(() => randomStar.EveryAudio[a].isPlaying); //������� �÷��̵ɶ����� ��ٷȴٰ�
        invisible.gameObject.SetActive(true); //������� �÷��̵Ǹ� ���������Ʈ Ȱ��ȭ
        yield break; //�ڷ�ƾ �����Ⱑ �־���� �ȱ׷� �ȳ����� �ӹ�������
    }
    //�̼Ǽ����� ���� ��Ÿ���� ȭ�鿡 ������ ��ġ�� ���� ���ȿ� ���������ִ� �Լ�
    public IEnumerator Speed_forStar(int index1)
    {
        invisible.gameObject.SetActive(true); //���� �����̴� ���� ��Ȱ��ȭ�� �����̹����̿�
        yield return new WaitForSeconds(1);
        while (Vector3.Distance(star.transform.position, starPosi[index1].transform.position) > 0)
        {
            //star.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 200);
            star.transform.position = Vector3.Lerp(star.transform.position, starPosi[index1].transform.position, Time.deltaTime *2);
            yield return new WaitForFixedUpdate();
            if (Vector3.Distance(star.transform.position, starPosi[index1].transform.position) <= 0.5)
            {
                invisible.SetActive(false);
                break;
            }
        }
        //star.transform.position = starPosi.GetChild(index).position;
        starPosi[index1].GetComponent<Image>().color = Color.white;
        invisible.gameObject.SetActive(false); //���� �������� ������ ���ͷ��� �ٽ� Ȱ��ȭ
        yield break;
    }
    //������ư ���� �Ŀ� ������������ �̹������� ���ļ� Rigidbody�� ���� �浹�� �з����� ����� ������ �ʰ��ϱ� ���� �Ͻ��� ���� �Լ� 
    IEnumerator Speed_forPosition(int index2)
    {
        yield return new WaitForSeconds(0.8f);
        uiPosition[index2].GetComponent<Image>().enabled = true;
        yield break;
    }
    #endregion
}

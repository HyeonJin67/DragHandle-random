using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
//���� �� ����ó��
public class GiyeokMissionManager : MonoBehaviour
{
    #region ����
    [SerializeField]
    RandomStar randomStar; //Scriptable Object�� ������ �ҷ��� ����
    [Tooltip("������ �ʿ��� �̼� �Ϸ� ���� ������ŭ�� �־��ּ���")]
    [SerializeField]
    GameObject[] starPosi; //���� �̵��� ��ġ
    [Tooltip("�������� �ٲ� ���� ��ġ�� �־��ּ���")]
    [SerializeField]
    Image[] randomPosi;
    [SerializeField]
    GameObject star; //�Ϸ�� ��
    [SerializeField]
    GameObject invisible; //���ͷ��� ��Ȱ��ȭ�� �����̹���
    [SerializeField]
    AudioSource narrations; //AudioSource�� ������Ʈ, AudioClip�� �����������ü
    int plus = 0;
    [Tooltip("�� ���� ������ �̸��� �Է����ּ��� ex)Giyeok")]
    [SerializeField]
    string obName;
    #endregion
    //�������� �̹����� ��ġ�� �ٲ��ִ� ��ũ��Ʈ
    void Start() 
    {
        var randomArray = Enumerable.Range(0, randomPosi.Length).ToArray(); //�ڵ����� �迭 ���·� �������� ���ڵ��� �־��� sprite�� ���� "�������" �־���
        var randomArrayGiyeok = Enumerable.Range(0, randomStar.EveryImages_Giyeok.Length).ToArray();
        System.Random random = new System.Random(); //�ý��ۿ� �ִ� ���� ����
        //OrderBy�� �������� �ϰڴٴ� �ǹ� : ������� �̹� �� �ִ� �Ÿ� �������� �����ְ� ��
        var shuffle1 = randomArray.OrderBy( x => random.Next()).ToArray(); //������ġ�� 
        var shuffle2 = randomArrayGiyeok.OrderBy( x => random.Next()).ToArray(); //������ �ܾ��
        while(plus < 2)
        {
            //print("�Ÿ�����"+Vector3.Distance(randomPosi[shuffle1[plus]].gameObject.transform.position, randomPosi[shuffle1[plus+1]].gameObject.transform.position));
            randomPosi[shuffle1[plus]].sprite = randomStar.EveryImages_Giyeok[shuffle2[plus]];
            Image im = randomPosi[shuffle1[plus]].GetComponent<Image>();
            Color col = im.color;
            col.a = 1f; //���İ� 0���� 1�� ���椤
            im.color = col;
            randomPosi[shuffle1[plus]].gameObject.name = "Contains_" + obName;
            plus++;
        }
        while (plus < 4)
        {
            randomPosi[shuffle1[plus]].sprite = randomStar.EveryImages_Nieun[shuffle2[plus]];
            Image im = randomPosi[shuffle1[plus]].GetComponent<Image>();
            Color col = im.color;
            col.a = 1f;
            im.color = col;
            randomPosi[shuffle1[plus]].gameObject.name = "Contains_Nieun";
            plus++;
        }
        if(plus == 4)
        {
            randomPosi[shuffle1[plus]].gameObject.SetActive(false); //���� �������� ���� ������Ʈ�� �浹ó�� �� �ִϸ��̼� �÷��̸� ���� ���� �ƿ� ���ֱ�
        }

        /*
        var indexArray = Enumerable.Range(0, randomStar.EveryImage_Giyeok.Length).ToArray(); //�ڵ����� �迭 ���·� �������� ���ڵ��� �־��� sprite�� ���� "�������" �־���
        System.Random random = new System.Random(); //�ý��ۿ� �ִ� ���� ����
        var shuffleArray1 = indexArray.OrderBy(x => random.Next()).ToArray(); //OrderBy�� �������� �ϰڴٴ� �ǹ� : ������� �̹� �� �ִ� �Ÿ� �������� �����ְ� �� //�ٲ�� �̹�����
        while (plus < uiPosition.Length)
        {
            
            uiPosition[plus].GetComponent<Image>().enabled = false; //������ġ�� ������ ���� ����� �����
            if (uiPosition[plus].gameObject.name.Contains(obName)) 
                uiPosition[plus].sprite = randomStar.EveryImage_Giyeok[shuffleArray1[plus]];
            else
                uiPosition[plus].sprite = randomStar.EveryImage_Nieun[shuffleArray1[plus]];
            //int x = Random.Range(-Screen.width/2, Screen.width/2); //����,���� ������ٸ� ���簢���� ���������� ����
            int x = Random.Range(-690, 370); //�������� ĵ���������� ������ ���� ������ ��������760��ŭ x���� +,- �α��� ��, ���Ʒ� y���� �������� �ϰ� �¿�θ� �Դٰ����ϰ� �����
            Vector3 randSquarePos = new Vector3(x, uiPosition[plus].transform.GetComponent<RectTransform>().localPosition.y, 0); //z���� 90���� ����//�ȱ׷� ī�޶�� ĵ�������̿� �Ÿ�(����)���� ���ƴٳ༭ ũ�� ����
            uiPosition[plus].transform.GetComponent<RectTransform>().localPosition = randSquarePos; //���簢�������� ������ġ�� ���� 
            StartCoroutine(Speed_forPosition(plus));
            ++plus; //���� �ε����� �ѱ�¿�
        }
        plus = 0; //�������� ������ �ε��� 0���� �ٽ� ����
        */
    }
    #region �Լ�
    //����� ����� ��� ���ͷ���(��ġ�� �巡��) ��Ȱ��ȭ��
    IEnumerator StartClip(int a) 
    {
        invisible.gameObject.SetActive(false);
        //narrations.Stop();
        //narrations.clip = randomStar.EveryAudios_Giyeok[a];
        //narrations.Play();
        //yield return new WaitUntil(() => !narrations.isPlaying); //yield return new WaitUntil(() => randomStar.EveryAudio[a].isPlaying); //������� �÷��̵ɶ����� ��ٷȴٰ�
        invisible.gameObject.SetActive(true); //������� �÷��̵Ǹ� ���������Ʈ Ȱ��ȭ
        yield break; //�ڷ�ƾ �����Ⱑ �־���� �ȱ׷� �ȳ����� �ӹ�������
    }
    //�̼Ǽ����� ���� ��Ÿ���� ȭ�鿡 ������ ��ġ�� ���� ���ȿ� ���������ִ� �Լ�
    public IEnumerator Speed_forStar(int index1, GameObject ob)
    {
        star.transform.position = ob.gameObject.transform.position; //���ܵ� ���� ���� ��ġ�� �浹������Ʈ���� ���� �ǰԲ� //�� �ϳ��� ������ ��������
        star.transform.SetAsLastSibling();
        star.SetActive(true);
        invisible.transform.SetAsLastSibling();
        invisible.SetActive(true); //���� �����̴� ���� ��Ȱ��ȭ�� �����̹����̿�
        yield return new WaitForSeconds(1); //1�� ���� �ִٰ� �� ����
        while (Vector3.Distance(star.transform.position, starPosi[index1].transform.position) > 0.7f)
        {
            star.transform.position = Vector3.Lerp(star.transform.position, starPosi[index1].transform.position, Time.deltaTime *2);
            yield return new WaitForSeconds(Time.deltaTime * 0.8f); //yield return new WaitForFixedUpdate();
            if (Vector3.Distance(star.transform.position, starPosi[index1].transform.position) <= 0)
            {
                invisible.SetActive(false);
                break;
            }
        }
        //starPosi[index1].GetComponent<Image>().sprite = randomStar.star[0]; //����ܰ����� ������� ��ü
        starPosi[index1].GetComponent<Image>().sprite = randomStar.Stars[0]; //����ܰ����� ������� ��ü //������Ƽ�� �� ��������
        invisible.SetActive(false); //���� �������� ������ ���ͷ��� �ٽ� Ȱ��ȭ
        star.SetActive(false);
        yield break;
    }
    #endregion
}

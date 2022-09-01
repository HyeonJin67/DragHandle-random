using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// MonoBehaviour ���� ScriptableObject ���
// ����, ������Ƽ
// ������ public���� �ϸ� �������ϱ� ������Ƽ�� �����ϴ� ������� �ٲٱ�!

[CreateAssetMenu(menuName = "ScriptableObject/Sprites", fileName = "RandomStarSprite")] //����Ƽ���� �޴� ����

public class RandomStar : ScriptableObject
{
    [Header("���� ���")]
    [Tooltip("�̼ǿϷ�� ������ sprite�� ��� �־��ּ���")]
    //�׳� private���� �ϸ� �ν�����â���� �Ⱥ��̴ϱ� [SerializeField]�� ����
    [SerializeField] //publicó�� �ν�����â���� ���̰� �ϸ鼭 private���� ������ �ȵǰ�(public�� private �ΰ��� �Ӽ� �� ������ ����)
    Sprite[] star; //�տ� �����ϸ� private // cf) protected�� ��ӹ��� �ֵ� 
    //�б� ���� ������Ƽ //�б� �����̶� ���⼱ �� ������Ƽ���ϰ� �׳� public ������ ����ص� �Ǳ� ��
    public Sprite[] Stars { get => star; }

    [Tooltip("�������� �ٲ� ���� ��� sprite�� �־��ּ���")]
    [SerializeField]
    Sprite[] everyImage_Giyeok; //�������� �ٲ� ��� �̹���
    public Sprite[] EveryImages_Giyeok { get { return everyImage_Giyeok; } }

    [Tooltip("�̹����� ��Ʈ�� �� ��� ������� ������� �־��ּ���")]
    [SerializeField]
    private AudioClip[] everyAudio_Giyeok; //�̹����� ��Ʈ �����
    public AudioClip[] EveryAudios_Giyeok { get { return everyAudio_Giyeok; } }

    [Tooltip("�������� �ٲ� ���� ��� sprite�� �־��ּ���")]
    [SerializeField]
    private Sprite[] everyImage_Nieun; //�������� �ٲ� ��� �̹���
    public Sprite[] EveryImages_Nieun { get { return everyImage_Nieun; } }

    //BananaScene Image
    [Header("��ȯ ���")]
    [Tooltip("�������� �ٲ� ���� ��� sprite�� �־��ּ���")]
    public Sprite[] Random_Gimage; //�������� �ٲ� ��� �̹���

    [Tooltip("�������� �ٲ� ���� ��� sprite�� �־��ּ���")]
    public Sprite[] Random_Nimage; //�������� �ٲ� ��� �̹���

    [Tooltip("�������� �ٲ� ���� ��� sprite�� �־��ּ���")]
    public Sprite[] Random_Dimage; //�������� �ٲ� ��� �̹���

    [Tooltip("�������� �ٲ� ���� �ѱ� �̹��� ")]
    public Sprite[] Random_letter_G;

    public Sprite[] Random_letter_N;

    public Sprite[] Random_letter_D;

    [Tooltip("�������� �ٲ� �ѱ� �̹���")]
    public Sprite[] Random_letter_G2;

    public Sprite[] Random_letter_N2;

    public Sprite[] Rnadom_letter_D2;

    [Tooltip("�������� �ٲ� AudioClip")]
    public AudioClip[] Random_AudioClip_G;

    [Tooltip("�������� �ٲ� ����Clip")]
    public AudioClip[] Random_AnswerClip_G;

    [Tooltip("�������� �ٲ� AudioClip")]
    public AudioClip[] Random_AudioClip_N;

    [Tooltip("�������� �ٲ� ����Clip")]
    public AudioClip[] Random_AnswerClip_N;
}

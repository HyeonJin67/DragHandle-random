using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// MonoBehaviour 말고 ScriptableObject 상속
// 변수, 프로퍼티
// 변수는 public으로 하면 안좋으니까 프로퍼티로 접근하는 방식으로 바꾸기!

[CreateAssetMenu(menuName = "ScriptableObject/Sprites", fileName = "RandomStarSprite")] //유니티에서 메뉴 생성

public class RandomStar : ScriptableObject
{
    [Header("현진 담당")]
    [Tooltip("미션완료용 별관련 sprite를 모두 넣어주세요")]
    //그냥 private으로 하면 인스펙터창에선 안보이니까 [SerializeField]는 해줘
    [SerializeField] //public처럼 인스펙터창에서 보이게 하면서 private으로 접근은 안되게(public과 private 두가지 속성 다 가지는 개념)
    Sprite[] star; //앞에 생략하면 private // cf) protected는 상속받은 애들 
    //읽기 전용 프로퍼티 //읽기 전용이라 여기선 꼭 프로퍼티안하고 그냥 public 변수로 사용해도 되긴 함
    public Sprite[] Stars { get => star; }

    [Tooltip("랜덤으로 바뀔 ㄱ의 모든 sprite를 넣어주세요")]
    [SerializeField]
    Sprite[] everyImage_Giyeok; //랜덤으로 바뀔 모든 이미지
    public Sprite[] EveryImages_Giyeok { get { return everyImage_Giyeok; } }

    [Tooltip("이미지와 세트로 된 모든 오디오를 순서대로 넣어주세요")]
    [SerializeField]
    private AudioClip[] everyAudio_Giyeok; //이미지별 세트 오디오
    public AudioClip[] EveryAudios_Giyeok { get { return everyAudio_Giyeok; } }

    [Tooltip("랜덤으로 바뀔 ㄴ의 모든 sprite를 넣어주세요")]
    [SerializeField]
    private Sprite[] everyImage_Nieun; //랜덤으로 바뀔 모든 이미지
    public Sprite[] EveryImages_Nieun { get { return everyImage_Nieun; } }

    //BananaScene Image
    [Header("수환 담당")]
    [Tooltip("랜덤으로 바뀔 ㄱ의 모든 sprite를 넣어주세요")]
    public Sprite[] Random_Gimage; //랜덤으로 바뀔 모든 이미지

    [Tooltip("랜덤으로 바뀔 ㄴ의 모든 sprite를 넣어주세요")]
    public Sprite[] Random_Nimage; //랜덤으로 바뀔 모든 이미지

    [Tooltip("랜덤으로 바뀔 ㄴ의 모든 sprite를 넣어주세요")]
    public Sprite[] Random_Dimage; //랜덤으로 바뀔 모든 이미지

    [Tooltip("랜덤으로 바뀔 정답 한글 이미지 ")]
    public Sprite[] Random_letter_G;

    public Sprite[] Random_letter_N;

    public Sprite[] Random_letter_D;

    [Tooltip("램덤으로 바뀔 한글 이미지")]
    public Sprite[] Random_letter_G2;

    public Sprite[] Random_letter_N2;

    public Sprite[] Rnadom_letter_D2;

    [Tooltip("랜덤으로 바뀔 AudioClip")]
    public AudioClip[] Random_AudioClip_G;

    [Tooltip("랜덤으로 바뀔 정답Clip")]
    public AudioClip[] Random_AnswerClip_G;

    [Tooltip("랜덤으로 바뀔 AudioClip")]
    public AudioClip[] Random_AudioClip_N;

    [Tooltip("랜덤으로 바뀔 정답Clip")]
    public AudioClip[] Random_AnswerClip_N;
}

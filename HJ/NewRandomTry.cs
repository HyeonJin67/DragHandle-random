using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
//랜덤 및 정답처리
public class NewRandomTry : MonoBehaviour
{
    #region 변수
    /*
    [Tooltip("랜덤으로 바뀔 모든 sprite를 넣어주세요")]
    [SerializeField]
    Sprite[] EveryImage; //랜덤으로 바뀔 모든 이미지
    [Tooltip("이미지와 세트로 된 모든 오디오를 순서대로 넣어주세요")]
    [SerializeField]
    AudioClip[] EveryAudio; //이미지별 세트 오디오파일 //AudioClip은 오디오 파일 자체 //AudioSource는 컴포넌트
    */
    [Tooltip("랜덤으로 바뀌게 될 이미지를 모두 넣어주세요")]
    [SerializeField]
    Image[] ui; //드래그해서 옮길 이미지들
    [Tooltip("드래그해서 옮길 위치를 모두 넣어주세요")]
    [SerializeField]
    GameObject[] uiPosition; //스티커들을 드래그해서 충돌처리시켜줄 위치용 투명이미지
    [SerializeField]
    GameObject star; //완료용 별
    [Tooltip("씬별로 필요한 미션 완료 별의 갯수만큼만 넣어주세요")]
    [SerializeField]
    GameObject[] starPosi; //별이 이동할 위치
    [SerializeField]
    GameObject invisible; //인터랙션 비활성화용 투명이미지
    [SerializeField]
    AudioSource narrations;
    int plus = 0; //안바뀌는 값이므로 상수로 변경예정
    #endregion
    
    [SerializeField]
    RandomStar randomStar;

    #region 함수
    public void Test() //버튼 클릭시 호출되는 함수로 인스펙터창에서 지정 설정해주기
    {
        var indexArray = Enumerable.Range(0, randomStar.EveryImages_Giyeok.Length).ToArray(); //자동으로 배열 형태로 범위내의 숫자들을 넣어준 sprite의 갯수 순서대로 넣어줌
        System.Random random = new System.Random(); //시스템에 있는 랜덤 생성
        var shuffleArray1 = indexArray.OrderBy(x => random.Next()).ToArray(); //OrderBy를 랜덤으로 하겠다는 의미 : 순서대로 이미 들어가 있는 거를 랜덤으로 섞어주게 됨 //바뀌는 이미지용
        var shuffleArray2 = uiPosition.OrderBy(x => random.Next()).ToArray(); //바뀌는 텍스트용
        while (plus < ui.Length) //넣어둔 ui의 갯수만큼만 반복
        {
            uiPosition[plus].GetComponent<Image>().enabled = false;
            int x = Random.Range(-30, 30); //가로,세로 사이즈다른 직사각형의 범위내에서 랜덤
            int y = Random.Range(-20, 20);
            ui[plus].sprite = randomStar.EveryImages_Giyeok[shuffleArray1[plus]];
            shuffleArray2[plus].GetComponentInChildren<Text>().text = ui[plus].sprite.name; //변경된 이미지의 sprite파일의 이름으로 텍스트 변경
            //반경 1인 원 안에서 랜덤으로 위치 변경
            //Vector2 randCirclePos = Random.insideUnitCircle;
            //uiPosition[plus].transform.position = randCirclePos;
            //Screen Space - camera로 변경해야 제대로 보임
            Vector3 randSquarePos = new Vector3(x, y, 90); //z값은 90으로 고정
            uiPosition[plus].transform.position = randSquarePos; //직사각형내에서 랜덤위치로 변경
            //EveryAudio[shuffleArray1[plus]].Play(); //이미지에 맞는 세트로 된 오디오 재생
            //StartCoroutine(StartClip(shuffleArray1[plus])); //오디오 재생시 인터랙션 비활성화용 코루틴 호출
            StartCoroutine(Speed_forPosition(plus));
            ++plus; //다음 인덱스로 넘기는용
        }
        plus = 0; //버튼 누를때마다 인덱스 0부터 다시 시작
    }
    // < Wait 관련 참고이론 >
    // WaitUntil은 ()내부의 식이 "참"이 될 때까지 대기
    // WaitWhile은 내부의 식이 "거짓"이 될 때까지 대기
    //------------------------------------------------------
    //오디오 재생시 모든 인터랙션(터치나 드래그) 비활성화용
    IEnumerator StartClip(int a) 
    {
        invisible.gameObject.SetActive(false);
        //narrations.Stop();
        //narrations.clip = randomStar.EveryAudio[a];
        //narrations.Play();
        //yield return new WaitUntil(() => !narrations.isPlaying); //yield return new WaitUntil(() => randomStar.EveryAudio[a].isPlaying); //오디오가 플레이될때까지 기다렸다가
        invisible.gameObject.SetActive(true); //오디오가 플레이되면 투명오브젝트 활성화
        yield break; //코루틴 끝내기가 있어줘야 안그럼 안끝나고 머물러있음
    }
    //미션성공시 별이 나타나서 화면에 정해진 위치로 가는 동안에 지연시켜주는 함수
    public IEnumerator Speed_forStar(int index1)
    {
        invisible.gameObject.SetActive(true); //별이 움직이는 동안 비활성화용 투명이미지이용
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
        invisible.gameObject.SetActive(false); //별의 움직임이 끝나면 인터랙션 다시 활성화
        yield break;
    }
    //랜덤버튼 누른 후에 랜덤포지션의 이미지들이 겹쳐서 Rigidbody로 인한 충돌시 밀려나는 모습을 보이지 않게하기 위한 일시적 지연 함수 
    IEnumerator Speed_forPosition(int index2)
    {
        yield return new WaitForSeconds(0.8f);
        uiPosition[index2].GetComponent<Image>().enabled = true;
        yield break;
    }
    #endregion
}

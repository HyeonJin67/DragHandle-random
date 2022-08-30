using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//돋보기로 드래그 중 이 스크립트가 들어있는 오브젝트 위에 마우스포인터가 닿기만 해도 반응해서 ㄱ포함단어가 맞다는 힌트를 주는 애니메이션 재생 
//ㄱ포함단어의 오브젝트에 이 스크립트 넣어주기
public class PointerEnterCheckHandle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Animator animator;
    public void OnPointerEnter(PointerEventData eventData)
    {
        print("엔터체크");
        if (GetComponent<Image>().name.Contains("Giyeok"))
        {
            GetComponent<Animator>().enabled = true;
            animator = GetComponent<Animator>();
            animator.SetInteger("BigAction", 1);
        }
        if (GetComponent<Image>().name.Contains("Cloud"))
        {
            print("구름 확인용");
            //애니메이션이 원래의 처음위치로 돌아가서 켜지는 현상 해결을 위해 만난 그위치로 먼저 이동시켜준뒤 애니메이션을 틀어줘야함
            //transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        print("마우스포인트 나갔는지 체크");
        //animator = GetComponent<Animator>();
        //animator.SetInteger("BigAction", default);
        GetComponent<Animator>().enabled = false;
    }
}

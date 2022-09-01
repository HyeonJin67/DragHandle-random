using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ForDirection : MonoBehaviour
{
    
    [Tooltip("획의 순서맞게 화살표를 넣어주세요")]
    [SerializeField]
    Sprite[] arrowSprites;
    [Tooltip("획순 중에 있는 꺾이는 구간의 스팟들을 순서대로 넣어주세요")]
    [SerializeField]
    Image[] spotArrow;
    public bool CheckDirection(Vector2 vec, PointerEventData eventData)
    {
        Vector2 delta = eventData.delta.normalized;
        //print("Vector2.Dot(delta, vec)는" + Vector2.Dot(delta, vec));
        if (Vector2.Dot(delta, vec) >= 0)
        {
            ArrowChangeCheck(eventData);
            
            return true;
        }
        
        return false;
        
        //print($"Vec {vec} | delta | {delta}");

    }
    void ArrowChangeCheck(PointerEventData eventData)
    {


        if (eventData.delta.x > 0)
        {
            GetComponent<Image>().sprite = arrowSprites[0];
            spotArrow[0].GetComponent<Image>().sprite = arrowSprites[0];
            spotArrow[0].gameObject.SetActive(true);
        }
        else if (eventData.delta.y < 0)
        {
            GetComponent<Image>().sprite = arrowSprites[1];
            spotArrow[0].GetComponent<Image>().sprite = arrowSprites[1];
            spotArrow[0].gameObject.SetActive(true);
        }
        else if (eventData.delta.x < 0)
        {
            GetComponent<Image>().sprite = arrowSprites[2];
            spotArrow[0].GetComponent<Image>().sprite = arrowSprites[2];
            spotArrow[0].gameObject.SetActive(true);
        }
        else if (eventData.delta.y > 0)
        {
            GetComponent<Image>().sprite = arrowSprites[3];
            spotArrow[0].GetComponent<Image>().sprite = arrowSprites[3];
            spotArrow[0].gameObject.SetActive(true);
        }
    }

    
    
}

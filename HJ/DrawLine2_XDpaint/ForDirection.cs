using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ForDirection : MonoBehaviour
{
    
    [Tooltip("ȹ�� �����°� ȭ��ǥ�� �־��ּ���")]
    [SerializeField]
    Sprite[] arrowSprites;
    [Tooltip("ȹ�� �߿� �ִ� ���̴� ������ ���̵��� ������� �־��ּ���")]
    [SerializeField]
    Image[] spotArrow;
    public bool CheckDirection(Vector2 vec, PointerEventData eventData)
    {
        Vector2 delta = eventData.delta.normalized;
        //print("Vector2.Dot(delta, vec)��" + Vector2.Dot(delta, vec));
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

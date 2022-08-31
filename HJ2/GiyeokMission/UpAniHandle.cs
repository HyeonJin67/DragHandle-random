using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//�� ���۽� �ܾ���� �ĵ� ���� �� �ö���� �ִϸ��̼Ǵ�� ���̴� �����Լ�
public class UpAniHandle : MonoBehaviour
{
    [SerializeField]
    Image upPosi;
    void Start()
    {
        StartCoroutine(Speed_forWord());
    }
    IEnumerator Speed_forWord()
    {
        //yield return new WaitForSeconds(0.5f);
        while (Vector3.Distance(transform.position, upPosi.transform.position) > 0.7f)
        {
            transform.position = Vector3.Lerp(transform.position, upPosi.transform.position, Time.deltaTime * 0.7f);
            yield return new WaitForSeconds(Time.deltaTime * 0.8f);
            if (Vector3.Distance(transform.position, upPosi.transform.position) <= 0.7f)
            { break; }
        }
        transform.position = upPosi.transform.position;
        yield break;
    }
}

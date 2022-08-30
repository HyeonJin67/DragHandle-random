using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PicoWalkHandle : MonoBehaviour
{
    [SerializeField]
    Image picoPosition;
    private Animator animator;
    void Start()
    {
        StartCoroutine(Speed_forPico());
    }
    //���� �ɾ ���� õõ�� �����Բ� ���ִ� ���� �Լ�
    IEnumerator Speed_forPico()
    {
        while (Vector3.Distance(transform.position, picoPosition.transform.position) > 10) //�ѻ����� �Ÿ��� �ִ� ���� //÷�� 0���� �ߴٰ� �ʹ� ������ 10���� �ٲ�
        {
            transform.position = Vector3.Lerp(transform.position, picoPosition.transform.position, Time.deltaTime * 0.7f);
            yield return new WaitForSeconds(Time.deltaTime);
            if (Vector3.Distance(transform.position, picoPosition.transform.position) <= 10)
            {
                break;
            }
        }
        transform.position = picoPosition.transform.position;
        transform.Rotate(0, -70, 0);
        animator = GetComponent<Animator>();
        animator.SetInteger("PicoAction", 4); //���ھִϸ��̼� �߿� 4�� �θ����θ����ѱ�
        yield break;
    }
}

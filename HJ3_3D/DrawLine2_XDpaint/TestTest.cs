using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Testinterface
{
    interface ICheck
    {
        Vector2 CheckNext()
        {
            Debug.Log("�׽�Ʈ");
            //���Ͱ��� ���������
            return Vector2.zero;
        }
        public class TestTest : MonoBehaviour
        {
        
        }
    }
}

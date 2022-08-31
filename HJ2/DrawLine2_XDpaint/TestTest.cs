using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Testinterface
{
    interface ICheck
    {
        Vector2 CheckNext()
        {
            Debug.Log("테스트");
            //벡터값을 보내줘워함
            return Vector2.zero;
        }
        public class TestTest : MonoBehaviour
        {
        
        }
    }
}

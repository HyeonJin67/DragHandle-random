using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
//�ְ� ���� �̺�Ʈ Args;
public class CountParameter : EventArgs
{
    public GameObject gameObject;
    public int count;
    public int transformIndex;
}

public class ForCount : MonoBehaviour
{
    public event EventHandler<CountParameter> Connect; //CountParameter��� Ŭ������ �Ű������� �ϴ� �̺�Ʈ�ڵ鷯
    public CountParameter countParameter = new CountParameter(); //CountParameterŬ�����ȿ� �ִ� �������� ����ϰ���

    int count;
    
    public int Count //��� �̼��� �ϼ��ߴ��� ī��Ʈ�ϴ� ������Ƽ
    {
        get => count; //�о����:count���� �о�´�.
        set //����:����Ѵ�
        {
            count = value;
            countParameter.count = value;
            Connect?.Invoke(this, countParameter);
        }
    }
    
}


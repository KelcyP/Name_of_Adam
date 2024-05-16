using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    [SerializeField] GameObject SplashObj;               //�ǳڿ�����Ʈ
    [SerializeField] SpriteRenderer sr;                            //�ǳ� �̹���
    private bool checkbool = false;     //���� ���� ���� ����

    void Awake()
    {
        SplashObj = this.gameObject;                         //��ũ��Ʈ ������ ������Ʈ

        sr = SplashObj.GetComponent<SpriteRenderer>();    //�ǳڿ�����Ʈ�� �̹��� ����

        Color color = sr.color;
        color.a = 1.0f;
        sr.color = color;
    }

    void Update()
    {
        StartCoroutine("MainSplash");                        //�ڷ�ƾ    //�ǳ� ���� ����

        if (checkbool)                                            //���� checkbool �� ���̸�
        {
            Destroy(this.gameObject);                        //�ǳ� �ı�, ����
        }
    }

    IEnumerator MainSplash()
    {
        Color color = sr.color;                            //color �� �ǳ� �̹��� ����

        for (int i = 100; i >= 0; i--)                            //for�� 100�� �ݺ� 0���� ���� �� ����
        {
            color.a -= Time.deltaTime * 0.005f;               //�̹��� ���� ���� Ÿ�� ��Ÿ �� * 0.01
            sr.color = color;                                //�ǳ� �̹��� �÷��� �ٲ� ���İ� ����
            if (sr.color.a <= 0)                        //���� �ǳ� �̹��� ���� ���� 0���� ������
            {
                checkbool = true;                              //checkbool �� 
            }
        }
        yield return null;                                        //�ڷ�ƾ ����
    }


}

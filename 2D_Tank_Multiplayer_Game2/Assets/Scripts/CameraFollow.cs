using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform m_TargetTransform; // ��ͷҪ���ٵ�Ŀ��
    private float depth = -10f;          // ��ͷ����ڽ�ɫ��ǰ��λ�ã���������λ�ڽ�ɫ�󷽣�
    [SerializeField]
    private float m_Speed = 10f; // ���ƾ�ͷ����ʱ���ٶȣ����ڵ�����ͷ��ƽ���ƶ�������ٶȹ��󣬼��������ֱ�Ӱ�Ŀ��λ�ø�����ͷ����ô��������һ��Ľ�ɫ˲��Ч������������������Ӿ�Ӱ��

    void Update()
    {

        if (m_TargetTransform != null)
        {
            var targetposition = m_TargetTransform.position + new Vector3(0,0, depth);
            transform.position = Vector3.MoveTowards(transform.position, targetposition, m_Speed * Time.deltaTime);
        }

    }

    public void SetTarget(Transform target)
    {

        m_TargetTransform = target;
    }
}

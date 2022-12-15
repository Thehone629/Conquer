using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWork : MonoBehaviour
{
    #region Private field
    [SerializeField]
    private float distance = 7.0f;

    [SerializeField]
    private float height = 3.0f;

    //�����������Ŀ�괹ֱƫ�ƣ������ṩ����ĳ����ع���ͼ�͸��ٵĵ���
    [SerializeField]
    private Vector3 centerOffset = Vector3.zero;

    //���Ԥ�Ƽ��������photon����ʵ��������������Ϊ false��������Ҫʱ�ֶ����� onStartFollowing����
    [SerializeField]
    private bool followOnStart = false;

    [SerializeField]
    private float smoothSpeed = 0.125f;

    //cache
    Transform cameraTransform;
    bool isFollowing;
    Vector3 cameraOffset = Vector3.zero;

    #endregion

    #region monobehaviour callbacks

    // Start is called before the first frame update
    void Start()
    {
        if (followOnStart)
        {
            OnStartFollowing();
        }
    }

    private void LateUpdate()
    {
        //���ؿ����ص�ʱ��Ŀ�����û�б�destroy
        //���ÿ�μ����³���ʱ��������Ҫ����������ͷ��ͬ�Ľ��䣬���ڷ����������ʱ��������
        if (cameraTransform == null && isFollowing)
        {
            OnStartFollowing();
        }
        //�����Ǹ���ͺ���
        if (isFollowing)
        {
            Follow();
        }
    }

    #endregion

    #region Public methods
    public void OnStartFollowing()
    {
        cameraTransform = Camera.main.transform;//��һ�����õ���������Ϊ��MainCamera��
        isFollowing = true;
        Cut();
    }

    #endregion

    #region private methods
    void Follow()
    {
        cameraOffset.z = -distance;
        cameraOffset.y = height;
        //����֮��� ���Բ�ֵ
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, this.transform.position + this.transform.TransformVector(cameraOffset), smoothSpeed * Time.deltaTime);
        cameraTransform.LookAt(this.transform.position + centerOffset);
    }

    void Cut()
    {
        cameraOffset.z = -distance;
        cameraOffset.y = height;

        cameraTransform.position = this.transform.position + this.transform.TransformVector(cameraOffset);
        cameraTransform.LookAt(this.transform.position + centerOffset);
    }
    #endregion
}
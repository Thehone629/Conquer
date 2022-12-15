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

    //允许摄像机与目标垂直偏移，例如提供更多的场景曝光视图和更少的地面
    [SerializeField]
    private Vector3 centerOffset = Vector3.zero;

    //如果预制件的组件被photon网络实例化，则将其设置为 false，并在需要时手动调用 onStartFollowing（）
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
        //当关卡加载的时候目标可能没有被destroy
        //因此每次加载新场景时，我们需要覆盖主摄像头不同的角落，并在发声这种情况时重新连接
        if (cameraTransform == null && isFollowing)
        {
            OnStartFollowing();
        }
        //仅仅是跟随就好了
        if (isFollowing)
        {
            Follow();
        }
    }

    #endregion

    #region Public methods
    public void OnStartFollowing()
    {
        cameraTransform = Camera.main.transform;//第一个启用的摄像机标记为“MainCamera”
        isFollowing = true;
        Cut();
    }

    #endregion

    #region private methods
    void Follow()
    {
        cameraOffset.z = -distance;
        cameraOffset.y = height;
        //两点之间的 线性插值
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
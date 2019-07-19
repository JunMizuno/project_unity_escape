using UnityEngine;
using System.Threading;
using System.Threading.Tasks;
using DG.Tweening;

/// <summary>
/// カメラマネージャー
/// </summary>
public class CameraManager : MonoBehaviour
{
    [SerializeField]
    public Camera MainCamera;

    [SerializeField]
    public GameObject TargetObject;

    private CameraManager instance;
    public CameraManager Instance {
        get
        {
            return this.instance;
        }
    }

    private Vector3 originalPosition;

    private readonly float CAMERA_POS_Y_WITH_GLOBAL_MODE = 5.0f;
    private readonly float CAMERA_DISTANCE_Z_WITH_GLOBAL_MODE = -7.0f;

    private void Awake()
    {
        if (instance == null) {
            instance = this;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        if (MainCamera != null)
        {
            originalPosition = MainCamera.transform.position;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
        RefreshCameraPos();
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

    private void OnDestroy()
    {

    }

    private void RefreshCameraPos()
    {
        if (MainCamera == null)
        {
            return;
        }

        if (TargetObject == null)
        {
            return;
        }

        RefreshCameraPosByGlobal();
        //RefreshCameraPosByFollowing();
    }

    private void SetPosition(Vector3 position)
    {
        if (MainCamera != null)
        {
            MainCamera.transform.position = position;
        }
    }

    private void SetRotation(Quaternion quaternion)
    {
        //Vector3 axis = new Vector3(0f, 0f, 1f); // 回転軸
        //float angle = 90f * Time.deltaTime; // 回転の角度
        //Quaternion q = Quaternion.AngleAxis(angle, axis); // 軸axisの周りにangle回転させるクォータニオン

        if (MainCamera != null)
        {
            MainCamera.transform.rotation = quaternion * MainCamera.transform.rotation;
        }
    }

    private void SetTargetTransform(Transform targetTrans)
    {
        if (MainCamera != null)
        {
            MainCamera.transform.LookAt(targetTrans);
        }
    }

    public void SetLookTarget(Transform targetTrans)
    {
        SetTargetTransform(targetTrans);
    }

    private void RefreshCameraPosByGlobal()
    {
        var backPosition = TargetObject.transform.position;
        backPosition.y = CAMERA_POS_Y_WITH_GLOBAL_MODE;
        backPosition.z = backPosition.z + CAMERA_DISTANCE_Z_WITH_GLOBAL_MODE;

        SetPosition(backPosition);
        // @todo.ローテーションもh必要ならここに
        SetLookTarget(TargetObject.transform);
    }

    private void RefreshCameraPosByFollowing()
    {



    }
}

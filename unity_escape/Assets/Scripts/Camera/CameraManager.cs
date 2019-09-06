using UnityEngine;
using System.Threading;
using System.Threading.Tasks;
using DG.Tweening;

/// <summary>
/// カメラマネージャーが
/// </summary>
public class CameraManager : MonoBehaviour
{
    [SerializeField]
    public Camera MainCamera;

    [SerializeField]
    public GameObject TargetObject;

    private static CameraManager instance;
    public static CameraManager Instance {
        get
        {
            return instance;
        }
    }

    private CameraMode.ViewMode currentViewMode = CameraMode.ViewMode.Global;
    public CameraMode.ViewMode CurrentViewMode
    {
        get
        {
            return currentViewMode;
        }

        set
        {
            currentViewMode = value;
        }
    }

    private Vector3 originalPosition;

    private readonly float CAMERA_POS_Y_AT_GLOBAL_MODE = 5.0f;
    private readonly float CAMERA_DISTANCE_Z_AT_GLOBAL_MODE = -7.0f;
    private readonly float CAMERA_POS_Y_AT_FOLLOWING_MODE = 1.5f;
    private readonly float CAMERA_DISTANCE_Z_AT_FOLLOWING_MODE = -5.0f;

    private void Awake()
    {
        if (instance == null) {
            instance = this;
        }
    }

    /// <summary>
    /// 開始時
    /// </summary>
    void Start()
    {
        if (MainCamera != null)
        {
            originalPosition = MainCamera.transform.position;
        }
    }

    /// <summary>
    /// 演算処理後の最終更新
    /// </summary>
    void FixedUpdate()
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

    /// <summary>
    /// カメラ座標を更新
    /// </summary>
    private void RefreshCameraPos()
    {
        if (MainCamera == null)
        {
            return;
        }

        if (TargetObject == null)
        {
            // @todo. ターゲットオブジェクトが無い場合の設定が必要
            return;
        }

        if (currentViewMode == CameraMode.ViewMode.Global)
        {
            RefreshCameraPosByGlobal();
        }
        else if (currentViewMode == CameraMode.ViewMode.Following)
        {
            RefreshCameraPosByFollowing();
        }
    }

    /// <summary>
    /// カメラの座標を設定
    /// </summary>
    /// <param name="position"></param>
    private void SetPosition(Vector3 position)
    {
        if (MainCamera != null)
        {
            MainCamera.transform.position = position;
        }
    }

    /// <summary>
    /// カメラの角度を設定
    /// </summary>
    /// <param name="quaternion"></param>
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

    /// <summary>
    /// カメラの向く座標を指定
    /// </summary>
    /// <param name="targetTrans"></param>
    private void SetTargetTransform(Transform targetTrans)
    {
        if (MainCamera != null)
        {
            MainCamera.transform.LookAt(targetTrans);
        }
    }

    /// <summary>
    /// カメラのターゲットを設定
    /// </summary>
    /// <param name="targetTrans"></param>
    public void SetLookTarget(Transform targetTrans)
    {
        SetTargetTransform(targetTrans);
    }

    /// <summary>
    /// 見下ろし型のカメラ
    /// </summary>
    private void RefreshCameraPosByGlobal()
    {
        var backPosition = TargetObject.transform.position;
        backPosition.y = CAMERA_POS_Y_AT_GLOBAL_MODE;
        backPosition.z = backPosition.z + CAMERA_DISTANCE_Z_AT_GLOBAL_MODE;

        SetPosition(backPosition);
        // @todo.ローテーションも必要ならここに
        SetLookTarget(TargetObject.transform);
    }

    /// <summary>
    /// 追従型のカメラ
    /// </summary>
    private void RefreshCameraPosByFollowing()
    {
        var targetPos = TargetObject.transform.position;

        float angle = TargetObject.transform.localEulerAngles.y;
        float rad = angle * Mathf.Deg2Rad;
        float newX = (float)(Mathf.Sin(rad) * CAMERA_DISTANCE_Z_AT_FOLLOWING_MODE);
        float newY = TargetObject.transform.position.y + CAMERA_POS_Y_AT_FOLLOWING_MODE;
        float newZ = (float)(Mathf.Cos(rad) * CAMERA_DISTANCE_Z_AT_FOLLOWING_MODE);
        var newPosition = targetPos + new Vector3(newX, newY, newZ);

        SetPosition(newPosition);
        SetLookTarget(TargetObject.transform);
    }
}

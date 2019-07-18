using UnityEngine;
using System.Threading;
using System.Threading.Tasks;
using DG.Tweening;

/// <summary>
/// カメラマネージャー
/// </summary>
public class CameraManager : MonoBehaviour
{
    private CameraManager instance;
    public CameraManager Instance {
        get { return this.instance; }
    }

    [SerializeField]
    public Camera MainCamera;

    private Vector3 originalPosition;

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
    }

    private void SetPoasition(Vector3 position)
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
}

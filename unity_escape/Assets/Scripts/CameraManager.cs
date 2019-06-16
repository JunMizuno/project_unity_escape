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
    private float velocityZ = 0.0f;

    private CancellationTokenSource tokenSource;

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

            //mainCamera_.transform.DOMoveZ(-30.0f, 5.0f);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
        // @memo. テスト
        TouchManager.OnTouchAction();


        // @memo. 以下、テスト
        //RefreshCameraPos();

        //if (tokenSource_ != null)
        //{
        //    var cancelToken = tokenSource_.Token;
        //    Task.Run(() => OnAsyncTest(cancelToken));
        //}
    }

    private void OnEnable()
    {
        if (tokenSource == null)
        {
            tokenSource = new CancellationTokenSource();
        }
    }

    private void OnDisable()
    {
        if (tokenSource != null)
        {
            tokenSource.Cancel();
            Debug.Log("OnDisable():タスク破棄");
        }
    }

    private void OnDestroy()
    {
        if (tokenSource != null)
        {
            tokenSource.Cancel();
            Debug.Log("OnDestroy():タスク破棄");
        }
    }

    private async Task OnAsyncTest(CancellationToken cancelToken)
    {
        //await Task.Run(() =>
        //{
        //    // 非同期したい処理
        //    velocityZ_ -= 0.0005f;

        //}).ContinueWith((obj) =>
        //{
        //    Debug.Log("スレッドナンバー:" + Thread.CurrentThread.ManagedThreadId);
        //});

        await Task.Delay(2000).ContinueWith((obj) =>
        {
            if (cancelToken.IsCancellationRequested)
            {
                return;
            }

            velocityZ -= 0.0005f;
            if (velocityZ < -0.5f)
            {
                velocityZ = -0.5f;
            }
            Debug.Log("スレッドナンバー:" + Thread.CurrentThread.ManagedThreadId);
        });
    }

    private void RefreshCameraPos()
    {
        if (MainCamera == null)
        {
            return;
        }

        var position = MainCamera.transform.position;
        position.z += velocityZ;
        if (position.z < -30.0f)
        {
            velocityZ = 0.0f;
            position.z = originalPosition.z;
        }
        MainCamera.transform.position = position;
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

using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// カメラマネージャー
/// </summary>
public class CameraManager : MonoBehaviour
{
    [SerializeField]
    public Camera mainCamera_;

    private Vector3 originalPosition_;
    private float velocityZ_ = 0.0f;

    private CancellationTokenSource tokenSource_;

    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        if (mainCamera_)
        {
            originalPosition_ = mainCamera_.transform.position;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
        RefreshCameraPos();

        if (tokenSource_ != null)
        {
            var cancelToken = tokenSource_.Token;
            Task.Run(() => OnAsyncTest(cancelToken));
        }
    }

    private void OnEnable()
    {
        if (tokenSource_ == null)
        {
            tokenSource_ = new CancellationTokenSource();
        }
    }

    private void OnDisable()
    {
        if (tokenSource_ != null)
        {
            tokenSource_.Cancel();
            Debug.Log("OnDisable():タスク破棄");
        }
    }

    private void OnDestroy()
    {
        if (tokenSource_ != null)
        {
            tokenSource_.Cancel();
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

            velocityZ_ -= 0.0005f;
            if (velocityZ_ < -0.5f)
            {
                velocityZ_ = -0.5f;
            }
            Debug.Log("スレッドナンバー:" + Thread.CurrentThread.ManagedThreadId);
        });
    }

    private void RefreshCameraPos()
    {
        if (mainCamera_ == null)
        {
            return;
        }

        var position = mainCamera_.transform.position;
        position.z += velocityZ_;
        if (position.z < -30.0f)
        {
            velocityZ_ = 0.0f;
            position.z = originalPosition_.z;
        }
        mainCamera_.transform.position = position;
    }

    private void SetPoasition(Vector3 position)
    {
        if (mainCamera_ != null)
        {
            mainCamera_.transform.position = position;
        }
    }

    private void SetRotation(Quaternion quaternion)
    {
        //Vector3 axis = new Vector3(0f, 0f, 1f); // 回転軸
        //float angle = 90f * Time.deltaTime; // 回転の角度
        //Quaternion q = Quaternion.AngleAxis(angle, axis); // 軸axisの周りにangle回転させるクォータニオン


        if (mainCamera_ != null)
        {
            mainCamera_.transform.rotation = quaternion * mainCamera_.transform.rotation;
        }
    }

    private void SetTargetTransform(Transform targetTrans)
    {
        if (mainCamera_ != null)
        {
            mainCamera_.transform.LookAt(targetTrans);
        }
    }
}

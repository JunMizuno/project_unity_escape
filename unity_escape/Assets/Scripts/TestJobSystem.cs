using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// NativeContainer
using Unity.Collections;

using Unity.Jobs;

public class TestJobSystem : MonoBehaviour
{
    // NativeContainer
    private NativeArray<int> intArray;
    private NativeArray<float> floatArray;
    private NativeArray<Vector3> vector3Array;

    NativeQueue<int> localQueue;
    NativeList<int> localList;

    // Job
    private struct TestJob : IJob
    {
        public int count;

        // @memo. ジョブの中でこのメソッド設定は必須
        public void Execute()
        {
            count += 1;
        }
    }
    private TestJob job;

    // Job
    private struct TestJobArray : IJobParallelFor
    {
        public NativeArray<Vector3> positions;
        public float deltaTime;

        // @memo. ジョブの中でこのメソッド設定は必須
        public void Execute(int index)
        {
            positions[index] = positions[index] + Vector3.forward * deltaTime;
        }
    }
    private TestJobArray jobArray;

    // Job
    private struct TestAddJob : IJob
    {
        public NativeQueue<int> queue;
        public NativeList<int> list;

        public void Execute()
        {
            while(queue.TryDequeue(out int item))
            {
                if (item % 2 == 0)
                {
                    list.Add(item);
                }
            }
        }
    }

    private struct TestUpdateJob : IJobParallelFor
    {
        public NativeArray<int> array;

        public void Execute(int index)
        {
            array[index] += 1;
        }
    }

    private struct TestShowLogJob : IJobParallelFor
    {
        [ReadOnly]
        public NativeList<int> list;

        public void Execute(int index)
        {
            Debug.LogFormat("TestShowLogJobのlistの中身:{0}(index:{1})", list[index], index);
        }
    }

    private void Awake()
    {
        // @memo. 後ろの引数指定、下に行くほどメモリコストが高くなる
        //intArray = new NativeArray<int>(0, Allocator.Temp);                 // 引数内、Tempは「そのフレームのみ」生存指定
        //floatArray = new NativeArray<float>(0, Allocator.TempJob);          // 引数内、TempJobは「そのJob中のみ」生存指定
        //vector3Array = new NativeArray<Vector3>(0, Allocator.Persistent);   // 引数内、Persistentは解放するまで生存指定

        intArray = new NativeArray<int>(3, Allocator.Persistent);
        intArray[0] = 1;
        intArray[1] = 2;
        intArray[2] = 3;

        floatArray = new NativeArray<float>(3, Allocator.Persistent);
        floatArray[0] = 1.0f;
        floatArray[1] = 2.0f;
        floatArray[2] = 3.0f;

        vector3Array = new NativeArray<Vector3>(3, Allocator.Persistent);
        vector3Array[0] = new Vector3(1.0f, 1.0f, 1.0f);
        vector3Array[1] = new Vector3(2.0f, 2.0f, 2.0f);
        vector3Array[2] = new Vector3(3.0f, 3.0f, 3.0f);

        job = new TestJob()
        {
            count = 0
        };

        jobArray = new TestJobArray
        {
            positions = new NativeArray<Vector3>(100, Allocator.Persistent),
            deltaTime = Time.deltaTime
        };
    }

    private void Start()
    {
        //ShowNativeArraysLog();

        //ExecuteTestJob();

        TestUseAddJob();
    }

    private void Update()
    {

    }

    private void OnDestroy()
    {
        Debug.Log("TestJobSystem OnDestroy");

        // 以下、解放処理
        intArray.Dispose();
        floatArray.Dispose();
        vector3Array.Dispose();

        jobArray.positions.Dispose();
    }

    private void ShowNativeArraysLog()
    {
        foreach (var data in intArray)
        {
            Debug.Log("intArrayの中身:" + data);
        }

        foreach (var data in floatArray)
        {
            Debug.Log("floatArrayの中身:" + data);
        }

        foreach (var data in vector3Array)
        {
            Debug.LogFormat("vector3Arrayの中身 x:{0} y:{1} z:{2}", data.x, data.y, data.z);
        }
    }

    private void ExecuteTestJob()
    {
        // Job発行
        JobHandle handle = job.Schedule();

        // @memo. IJobの場合は意図的に実行しないと反映されない模様
        job.Execute();

        // Jobの完了を待つ
        handle.Complete();

        // 中身を確認
        Debug.Log("Job.count:" + job.count);

        // Arrayの場合
        // 第2引数にゼロを入れるとバランスよくスレッドを振り分けてくれるらしい
        JobHandle handleArray = jobArray.Schedule(100, 0);
        for (int i = 0; i < 100; i++)
        {
            //jobArray.Execute(i);
        }
        handleArray.Complete();

        for (int i = 0; i < 100; i++)
        {
            Debug.LogFormat("jobArrayのpositionsの中身 x:{0} y:{1} z:{2}", jobArray.positions[i].x, jobArray.positions[i].y, jobArray.positions[i].z);
        }
    }

    private void TestUseAddJob()
    {
        // そのジョブ内のみ有効を指定して生成
        localQueue = new NativeQueue<int>(Allocator.TempJob);
        localList = new NativeList<int>(8, Allocator.TempJob);

        localQueue.Enqueue(2);
        localQueue.Enqueue(3);
        localQueue.Enqueue(4);
        localQueue.Enqueue(5);

        // 2で割り切れる数のみ抽出
        var handle = new TestAddJob { queue = localQueue, list = localList }.Schedule();
        handle.Complete();

        // それらに+1する
        // AsDeferredJobArrayでNativeArrayにする
        handle = new TestUpdateJob { array = localList.AsDeferredJobArray() }.Schedule(localList, 4, handle);
        handle.Complete();

        // デバッグ表示
        handle = new TestShowLogJob { list = localList }.Schedule(localList, 8, handle);
        handle.Complete();

        // ジョブが終わったタイミングで解放(でないとリークの可能性)
        localQueue.Dispose();
        localList.Dispose();
    }
}

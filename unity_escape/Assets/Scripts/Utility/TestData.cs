using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 取得数値などのテスト検証用のクラス
/// </summary>
public class TestData : MonoBehaviour
{
    private int[] sampleIntArray = new int[1];
    private List<int> sampleIntList = new List<int>();
    private Dictionary<int, int> sampleIntDictionary = new Dictionary<int, int>();
    private Queue<int> sampleIntQueue = new Queue<int>();
    private Stack<int> sampleIntStack = new Stack<int>();

    private void Awake()
    {

    }

    private void Start()
    {

    }

    private void Update()
    {
        //GetAxisData();
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

    private void OnGUI()
    {
        
    }

    private void GetAxisData()
    {
        if (Input.GetMouseButton(0))
        {
            // @memo. スクロールやスライドすると値が取れる、それ以外はゼロが返る
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");
            Debug.LogFormat("<color=white>GetAxis x:{0} y:{1}</color>", x, y);
        }
    }

    private void TestArray()
    {
        // @memo. 第1引数に値、第2引数にインデックス
        sampleIntArray.SetValue(5, 0);
        Debug.Log("<color=white>" + "sampleIntArrayの要素数:" + sampleIntArray.Length + "</color>");
        Debug.LogFormat("<color=white>sampleIntArrayのインデックス:{0} その値:{1}</color>", 0, sampleIntArray.GetValue(0));
    }

    private void TestList()
    {
        sampleIntList.Add(1);
        sampleIntList.Add(2);
        sampleIntList.Add(3);

        Debug.Log("<color=white>" + "sampleIntListの要素数:" + sampleIntList.Count + "</color>");

        foreach (int value in sampleIntList)
        {
            Debug.Log("<color=white>" + "sampleIntList value:" + value + "</color>");
        }

        sampleIntList.Clear();
        Debug.Log("<color=white>" + "sampleIntListのClear後の要素数:" + sampleIntList.Count + "</color>");
    }

    private void TestDictionary()
    {
        // @memo. キーの上書きは出来ない
        sampleIntDictionary.Add(0, 1);
        sampleIntDictionary.Add(1, 2);
        sampleIntDictionary.Add(2, 4);

        Debug.Log("<color=cyan>" + "sampleIntDictionaryの要素数:" + sampleIntDictionary.Count + "</color>");

        foreach (var data in sampleIntDictionary)
        {
            Debug.LogFormat("<color=cyan>sampleIntDictionary Key:{0} Value:{1}</color>", data.Key, data.Value);
        }

        sampleIntDictionary.Remove(1);
        Debug.Log("<color=cyan>" + "sampleIntDictionaryのRemove後の要素数:" + sampleIntDictionary.Count + "</color>");
    }

    private void TestQueue()
    {
        sampleIntQueue.Enqueue(5);
        sampleIntQueue.Enqueue(4);
        sampleIntQueue.Enqueue(3);
        sampleIntQueue.Enqueue(2);
        sampleIntQueue.Enqueue(1);

        Debug.Log("<color=white>" + "sampleIntQueueの要素数:" + sampleIntQueue.Count + "</color>");

        int value = sampleIntQueue.Dequeue();

        Debug.Log("<color=white>" + "sampleIntQueueのDequeue後の要素数:" + sampleIntQueue.Count + " Dequeueの値:" + value + "</color>");

    }

    private void TestStack()
    {
        sampleIntStack.Push(9);
        sampleIntStack.Push(8);
        sampleIntStack.Push(7);
        sampleIntStack.Push(6);
        sampleIntStack.Push(5);

        Debug.Log("<color=white>" + "sampleIntStackの要素数:" + sampleIntStack.Count + "</color>");

        int value = sampleIntStack.Pop();

        Debug.Log("<color=white>" + "sampleIntStackのPop後の要素数:" + sampleIntStack.Count + " Popの値:" + value + "</color>");
    }
}

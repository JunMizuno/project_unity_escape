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
        //TestLerp();
        TestSLerp();
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

    private void TestLerp()
    {
        /*
        float floatValue1 = Mathf.Lerp(0.0f, 10.0f, 0.5f);
        Debug.Log("<color=cyan>" + "Leap(floatValue1)の中間点:" + floatValue1 + "</color>");

        float floatValue2 = Mathf.Lerp(10.0f, 20.0f, 0.5f);
        Debug.Log("<color=cyan>" + "Leap(floatValue2)の中間点:" + floatValue2 + "</color>");

        float floatValue3 = Mathf.Lerp(floatValue1, floatValue2, 0.5f);
        Debug.Log("<color=cyan>" + "Leap(floatValue3)の中間点:" + floatValue3 + "</color>");
        */

        Vector3 vectorValue1 = Vector3.Lerp(new Vector3(0.0f, 0.0f, 0.0f), new Vector3(10.0f, 10.0f, 10.0f), 0.5f);
        Debug.Log("<color=cyan>" + "Leap(VectorValue1)の中間点:" + vectorValue1 + "</color>");

        Vector3 vectorValue2 = Vector3.Lerp(new Vector3(5.0f, 5.0f, 5.0f), new Vector3(15.0f, 15.0f, 15.0f), 0.5f);
        Debug.Log("<color=cyan>" + "Leap(VectorValue2)の中間点:" + vectorValue2 + "</color>");

        Vector3 vectorValue3 = Vector3.Lerp(vectorValue1, vectorValue2, 0.5f);
        Debug.Log("<color=cyan>" + "Leap(VectorValue3)の中間点:" + vectorValue3 + "</color>");
    }

    private void TestSLerp()
    {
        /*
        Vector3 vectorValue1 = Vector3.Slerp(new Vector3(-10.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 10.0f), 0.5f);
        Debug.Log("<color=magenta>" + "Leap(VectorValue1)の中間点:" + vectorValue1 + "</color>");

        Vector3 vectorValue2 = Vector3.Slerp(new Vector3(0.0f, 0.0f, 10.0f), new Vector3(10.0f, 0.0f, 0.0f), 0.5f);
        Debug.Log("<color=magenta>" + "Leap(VectorValue2)の中間点:" + vectorValue2 + "</color>");

        Vector3 vectorValue3 = Vector3.Slerp(vectorValue1, vectorValue2, 0.5f);
        Debug.Log("<color=magenta>" + "Leap(VectorValue3)の中間点:" + vectorValue3 + "</color>");
        */

        int objectCount = 8;

        Vector3 topPoint1 = new Vector3(-5.0f, 0.0f, 0.0f);
        Vector3 middlePoint1 = new Vector3(0.0f, 0.0f, 5.0f);
        Vector3 bottomPoint1 = new Vector3(5.0f, 0.0f, 0.0f);

        for (float t = 0.0f; t <= 1.0f; t += (1.0f / (float)objectCount))
        {
            Vector3 firstSleapPoint = Vector3.Slerp(topPoint1, middlePoint1, t);
            Vector3 secondSlerpPoint = Vector3.Slerp(middlePoint1, bottomPoint1, t);
            Vector3 finallySlerpPoint = Vector3.Slerp(firstSleapPoint, secondSlerpPoint, t);
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = finallySlerpPoint;
            Material material = new Material(Shader.Find("Standard")) { color = new Color(Color.red.r, Color.red.g, Color.red.b, 0.5f) };
            UtilityRenderer.SetBlendMode(material, UtilityRenderer.Mode.Transparent);
            sphere.GetComponent<Renderer>().material = material;
        }

        Vector3 topPoint2 = new Vector3(-5.0f, 0.0f, 0.0f);
        Vector3 middlePoint2 = new Vector3(0.0f, 0.0f, 5.0f);
        Vector3 bottomPoint2 = new Vector3(5.0f, 0.0f, 0.0f);

        for (float t = 0.0f; t <= 1.0f; t += (1.0f / (float)objectCount))
        {
            Vector3 firstLeapPoint = Vector3.Lerp(topPoint2, middlePoint2, t);
            Vector3 secondLerpPoint = Vector3.Lerp(middlePoint2, bottomPoint2, t);
            Vector3 finallyLerpPoint = Vector3.Lerp(firstLeapPoint, secondLerpPoint, t);
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Cube);
            sphere.transform.position = finallyLerpPoint;
            Material material = new Material(Shader.Find("Standard")) { color = new Color(Color.yellow.r, Color.yellow.g, Color.yellow.b, 0.5f) };
            UtilityRenderer.SetBlendMode(material, UtilityRenderer.Mode.Transparent);
            sphere.GetComponent<Renderer>().material = material;
        }
    }
}

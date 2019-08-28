using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageLogger : MonoBehaviour
{
    public static MessageLogger Instance;

    readonly int rowCount = 5;
    readonly float displaySeconds = 5.0f;
    readonly int sidePadding = 2;

    GameObject[] rowObjects;
    Text[] textLabels;
    HorizontalLayoutGroup[] layoutGroups;
    List<string> textSlotList = new List<string>();
    List<Coroutine> coroutineSlotList = new List<Coroutine>();

    public static bool Enable
    {
        get
        {
            return Instance.rowObjects[0].activeSelf;
        }
        set
        {
            for (int i = 0; i < Instance.rowObjects.Length; i++)
            {
                Instance.rowObjects[i].SetActive(value);
            }
        }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        this.rowObjects = new GameObject[this.rowCount];
        this.textLabels = new Text[this.rowCount];
        this.layoutGroups = new HorizontalLayoutGroup[this.rowCount];

        for (int i = 0; i < this.rowCount; i++)
        {
            // @todo. オブジェクト内容決め打ちで記述している
            // @todo. ここにヒエラルキーにもオブジェクトを作ってしまうこと
            this.rowObjects[i] = this.transform.GetChild(i).gameObject;
            this.textLabels[i] = this.transform.GetChild(i).GetChild(0).GetComponent<Text>();
            this.layoutGroups[i] = this.transform.GetChild(i).GetComponent<HorizontalLayoutGroup>();
        }

        Enable = false;
    }

    public static void PushText(string text)
    {
        if (Instance == null)
        {
            Debug.LogWarning("MessageLogger PushText:instance is null");
            return;
        }

        if (Instance.textSlotList.Count >= Instance.rowCount)
        {
            Instance.PopTextImmediate();
        }

        Instance.textSlotList.Add(text);
        Instance.coroutineSlotList.Add(Instance.StartCoroutine(Instance.DelayPopText()));
        Instance.ApplyTextString();
    }

    IEnumerator DelayPopText()
    {
        yield return new WaitForSeconds(this.displaySeconds);
        this.textSlotList.RemoveAt(0);
        this.coroutineSlotList.RemoveAt(0);
        this.ApplyTextString();
    }

    void PopTextImmediate()
    {
        this.textSlotList.RemoveAt(0);
        this.StopCoroutine(this.coroutineSlotList[0]);
        this.coroutineSlotList.RemoveAt(0);
    }

    void ApplyTextString()
    {
        for (int i = 0; i < this.rowCount; i++)
        {
            this.rowObjects[i].SetActive((i < this.textSlotList.Count) ? true : false);
            this.textLabels[i].text = (i < this.textSlotList.Count) ? this.textSlotList[i] : "";
            this.layoutGroups[i].padding.right = (i < this.textSlotList.Count) ? this.sidePadding : 0;
            this.layoutGroups[i].padding.left = (i < this.textSlotList.Count) ? this.sidePadding : 0;
            this.layoutGroups[i].SetLayoutHorizontal();
        }
    }
}

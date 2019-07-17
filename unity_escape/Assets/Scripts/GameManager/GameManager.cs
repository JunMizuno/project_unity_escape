using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private TouchManager TouchManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public TouchManager GetTouchManager()
    {
        return this.TouchManager;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestButton : MonoBehaviour
{
    [SerializeField]
    public Text InputText;

    public void OnClickDecideButton()
    {
        if (InputText == null)
        {
            return;
        }

        Application.OpenURL(InputText.text);
    }
}

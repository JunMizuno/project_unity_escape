using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace UnityChan
{
	[ExecuteInEditMode]
	public class SplashScreen : MonoBehaviour
	{
        [System.Obsolete]
        void NextLevel ()
		{
            Application.LoadLevel(Application.loadedLevel + 1);
		}
	}
}
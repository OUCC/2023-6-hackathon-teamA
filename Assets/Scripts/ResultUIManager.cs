using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace HackathonA
{

    public class ResultUIManager : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI resultText;
        [SerializeField]
        private Button restartButton;
        [SerializeField]
        private Button quitButton;
        // Start is called before the first frame update
        void Start()
        {
            resultText.SetText(PlayerPrefs.GetInt("result") switch
            {
                -1 => "Playerの負け",
                1 => "Playerの勝ち",
                _ => "Error"
            });
            restartButton.onClick.AddListener(() => ButtonClicked(0).Forget());
            quitButton.onClick.AddListener(() => ButtonClicked(1).Forget());
        }

        private async UniTaskVoid ButtonClicked(int buttonId)
        {
            if(buttonId == 0)
            {
                await SceanLoadCoroutine(LoadSceneMode.Single, 0f).ToUniTask();
            }
            else if(buttonId == 1)
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            }
        }

        internal IEnumerator SceanLoadCoroutine(LoadSceneMode sceneMode, float i_waitTime)
        {
            var ao = SceneManager.LoadSceneAsync("BattleScene", sceneMode);
            if (i_waitTime > 0.0f)
            {
                ao.allowSceneActivation = false;
                yield return new WaitForSeconds(i_waitTime);
                ao.allowSceneActivation = true;
            }
            yield return ao;
        }
    }
}

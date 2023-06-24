using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

namespace HackathonA
{
    public class BattleUIManager : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI messageText;
        [SerializeField]
        private TextMeshProUGUI enemyHpText;
        [SerializeField]
        private TextMeshProUGUI playerHpText;
        [SerializeField]
        private Button physicalButton;
        [SerializeField]
        private Button magicButton;
        [SerializeField]
        private Button recoverButton;
        [SerializeField]
        private Button physicalCounterButton;
        [SerializeField]
        private Button magicCounterCounterButton;
        [SerializeField]
        private Image enemyImage;
        [SerializeField]
        private HorizontalLayoutGroup buttonGroup;
        [SerializeField]
        private Image backButtonGroup;
        [SerializeField]
        private Image messageTextBackground;
        [SerializeField]
        private GameObject battleManagerObject;
        private BattleManager battleManager; 

        // Start is called before the first frame update
        void Start()
        {
            battleManager = battleManagerObject.GetComponent<BattleManager>();
            physicalButton.onClick.AddListener(async () => await ButtonCliledAsync(0));
            magicButton.onClick.AddListener(async () => await ButtonCliledAsync(1));
            recoverButton.onClick.AddListener(async () => await ButtonCliledAsync(2));
            physicalCounterButton.onClick.AddListener(async () => await ButtonCliledAsync(3));
            magicCounterCounterButton.onClick.AddListener(async () => await ButtonCliledAsync(4));
            messageTextBackground.gameObject.SetActive(false);
        }

        private async UniTask ButtonCliledAsync(int playerAction)
        {
            int battleJudge = 0;
            var ct = this.GetCancellationTokenOnDestroy();
            try
            {
                buttonGroup.gameObject.SetActive(false);
                backButtonGroup.gameObject.SetActive(false);
                (string playerMessage, string enemyMessage, int playerHP, int enemyHP, bool actionJudge, int battleJudge1) = await battleManager.StateUpdateAsync(playerAction);
                battleJudge = battleJudge1;
                playerHpText.SetText($"HP：{playerHP}");
                enemyHpText.SetText($"HP：{enemyHP}");
                messageTextBackground.gameObject.SetActive(true);
                backButtonGroup.gameObject.SetActive(true);
                if (actionJudge)
                {
                    messageText.SetText(playerMessage);
                    if (battleJudge != 1)
                    {
                        await UniTask.Delay(TimeSpan.FromSeconds(4), cancellationToken: ct);
                        messageText.SetText(enemyMessage);
                    }
                }
                else
                {
                    messageText.SetText(enemyMessage);
                    await UniTask.Delay(TimeSpan.FromSeconds(4), cancellationToken: ct);
                    messageText.SetText(playerMessage);
                }
                if (battleJudge == 0)
                {
                    await UniTask.Delay(TimeSpan.FromSeconds(4), cancellationToken: ct);
                }
                else
                {
                    PlayerPrefs.SetInt("result", battleJudge);
                    PlayerPrefs.Save();
                    await SceanLoadCoroutine(LoadSceneMode.Single, 4.0f).ToUniTask();
                }
            }
            finally
            {
                if (battleJudge == 0)
                {
                    messageTextBackground.gameObject.SetActive(false);
                    buttonGroup.gameObject.SetActive(true);
                }
            }
        }

        internal IEnumerator SceanLoadCoroutine(LoadSceneMode sceneMode, float i_waitTime)
        {
            var ao = SceneManager.LoadSceneAsync("ResultScene", sceneMode);
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

﻿using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HackathonA
{
    public class UIManager : MonoBehaviour, IDisposable
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
        private CancellationTokenSource cancellationTokenSource = new();

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

        private void OnDestroy()
        {
            cancellationTokenSource?.Cancel();
            cancellationTokenSource?.Dispose();
        }

        private async UniTask ButtonCliledAsync(int playerAction)
        {
            try
            {
                buttonGroup.gameObject.SetActive(false);
                backButtonGroup.gameObject.SetActive(false);
                (string playerMessage, string enemyMessage, int playerHP, int enemyHP, bool actionJudge) = await battleManager.StateUpdateAsync(playerAction);
                playerHpText.SetText($"HP：{playerHP}");
                enemyHpText.SetText($"HP：{enemyHP}");
                messageTextBackground.gameObject.SetActive(true);
                backButtonGroup.gameObject.SetActive(true);
                if (actionJudge)
                {
                    messageText.SetText(playerMessage);
                    await UniTask.Delay(TimeSpan.FromSeconds(3), cancellationToken: cancellationTokenSource.Token);
                    messageText.SetText(enemyMessage);
                }
                else
                {
                    messageText.SetText(enemyMessage);
                    await UniTask.Delay(TimeSpan.FromSeconds(3), cancellationToken: cancellationTokenSource.Token);
                    messageText.SetText(playerMessage);
                }           
                await UniTask.Delay(TimeSpan.FromSeconds(5), cancellationToken: cancellationTokenSource.Token);
            }
            finally
            {
                messageTextBackground.gameObject.SetActive(false);
                buttonGroup.gameObject.SetActive(true);
                cancellationTokenSource.Dispose();
                cancellationTokenSource = new();
            }
        }

        public void Dispose()
        {
            cancellationTokenSource?.Cancel();
            cancellationTokenSource?.Dispose();
        }
    }
}

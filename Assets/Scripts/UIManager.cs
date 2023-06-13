using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace HackathonA
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI messageText;
        [SerializeField]
        private TextMeshProUGUI enemyHPText;
        [SerializeField]
        private TextMeshProUGUI playerHPText;
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
        private BattleManagerFake battleManager;

        // Start is called before the first frame update
        void Start()
        {
            battleManager = new();
            battleManager.MessageChanged.Subscribe(message => messageText.SetText(message));
            battleManager.PlayerHPChanged.Subscribe(playerHP => playerHPText.SetText($"HP：{playerHP}"));
            battleManager.EnemyHPChanged.Subscribe(enemyHP => enemyHPText.SetText($"HP：{enemyHP}"));
            physicalButton.onClick.AddListener(() => ButtonCliled(0));
            magicButton.onClick.AddListener(() => ButtonCliled(1));
            recoverButton.onClick.AddListener(() => ButtonCliled(2));
            physicalCounterButton.onClick.AddListener(() => ButtonCliled(3));
            magicCounterCounterButton.onClick.AddListener(()=>ButtonCliled(4));
        }

        private void ButtonCliled(int playerAction)
        {
            var message = playerAction switch
            {
                0 => "物理攻撃",
                1 => "魔法攻撃",
                2 => "回復",
                3 => "物理カウンター",
                4 => "魔法カウンター",
                _ => "default",
            };
            battleManager.TestInvoke(message);
        }
    }

    public class BattleManagerFake : IDisposable
    {
        private Subject<string> messageChanged = new();
        private Subject<int> playerHPChanged = new();
        private Subject<int> enemyHPChanged = new();
        internal IObservable<string> MessageChanged { get => messageChanged; }
        internal IObservable<int> PlayerHPChanged { get => playerHPChanged; }
        internal IObservable<int> EnemyHPChanged { get => enemyHPChanged; }

        public void Dispose()
        {
            messageChanged.Dispose();
            playerHPChanged.Dispose();
            enemyHPChanged.Dispose();
        }

        public void TestInvoke(string message)
        {
            if (message == null)
                return;
            messageChanged.OnNext(message);
            var rnd = new System.Random();
            enemyHPChanged.OnNext(rnd.Next(0, 100));
            playerHPChanged.OnNext(rnd.Next(0, 100));
        }
    }
}

using UnityEngine;

namespace HackathonA
{
    public class SampleGameBehaviour : MonoBehaviour
    {
        public int PlayerHP = 100;
        public int EnemyHP = 100;

        async void Start()
        {
            var config = Resources.Load("ApiKey") as TextAsset;
            var _apiKey = config.text.Trim();
            var enemyBehaviour = new EnemyAI(_apiKey);
            DebugLoger.Log($"Enemy Action: {await enemyBehaviour.GetEnemyActionAsync(PlayerHP, EnemyHP)}");
        }
    }
}

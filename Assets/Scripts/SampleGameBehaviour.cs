using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace HackathonA
{
    public class SampleGameBehaviour : MonoBehaviour
    {
        public GameObject enemy;
        public int PlayerHP = 100;
        public int EnemyHP = 100;

        void Start()
        {
            EnemyBehaviour enemyBehaviour = enemy.GetComponent<EnemyBehaviour>();
            StartCoroutine(enemyBehaviour.GetEnemyAction(PlayerHP, EnemyHP, enemyAction =>
            {
                // レスポンスをコンソールに出力
                Debug.Log($"Enemy Action: {enemyAction}");
            }));
        }

    }
}

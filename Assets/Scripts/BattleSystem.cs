using System;
using Cysharp.Threading.Tasks;

namespace HackathonA
{
    public class BattleSystem
    {
        private EnemyAI enemyAI;
        public BattleSystem()
        {
            var config = UnityEngine.Resources.Load("ApiKey") as UnityEngine.TextAsset;
            var _apiKey = config.text.Trim();
            enemyAI = new EnemyAI(_apiKey);
        }

        public async UniTask<BattleData> BattleProcessAsync(int playerAction)
        {
            Player player = new Player();
            Enemy enemy = new Enemy();

            player.ActionType = playerAction;
            //ChatGPTからEnemyのActionTypeを取得
            enemy.ActionType = await enemyAI.GetEnemyActionAsync(player.Hp, enemy.Hp);

            //ダメージ計算とカウンターの成功判定
            (player.DamageValue, enemy.DamageValue, player.CounterJudge, enemy.CounterJudge) = Damage(player.ActionType, enemy.ActionType);

            //HP計算
            (player.Hp, enemy.Hp) = HpCalculate(player.Hp, player.ActionType, player.DamageValue, enemy.Hp, enemy.ActionType, enemy.DamageValue);

            //バトル終了判定
            int battleJudge = BattleJudge(player.Hp, enemy.Hp);

            //先行判定
            bool moveJudge = ActionJudge(player.CounterJudge);

            return new BattleData(battleJudge, moveJudge, player, enemy);
        }

        //カウンターの成功判定（成功が1、失敗が0）
        private (bool playerCounterJudge, bool enemyCounterJudge) CounterJudge(int playerAction, int enemyAction)
        {
            bool playerCounterJudge = false;
            bool enemyCounterJudge = false;

            //player側の判定
            if (playerAction == 3)
            {
                switch (enemyAction)
                {
                    case 0:
                        playerCounterJudge = true;
                        break;
                    case 1:
                        playerCounterJudge = false;
                        break;
                    case 2:
                        playerCounterJudge = false;
                        break;
                    case 3:
                        playerCounterJudge = false;
                        break;
                    case 5:
                        playerCounterJudge = false;
                        break;
                }
            }
            else if (playerAction == 4)
            {
                switch (enemyAction)
                {
                    case 0:
                        playerCounterJudge = false;
                        break;
                    case 1:
                        playerCounterJudge = true;
                        break;
                    case 2:
                        playerCounterJudge = false;
                        break;
                    case 3:
                        playerCounterJudge = false;
                        break;
                    case 5:
                        playerCounterJudge = false;
                        break;
                }
            }

            //enemy側の判定
            else if (enemyAction == 3)
            {
                switch (playerAction)
                {
                    case 0:
                        enemyCounterJudge = true;
                        break;
                    case 1:
                        enemyCounterJudge = false;
                        break;
                    case 2:
                        enemyCounterJudge = false;
                        break;
                    case 3:
                        enemyCounterJudge = false;
                        break;
                    case 5:
                        enemyCounterJudge = false;
                        break;
                }
            }
            else if (enemyAction == 4)
            {
                switch (playerAction)
                {
                    case 0:
                        enemyCounterJudge = false;
                        break;
                    case 1:
                        enemyCounterJudge = true;
                        break;
                    case 2:
                        enemyCounterJudge = false;
                        break;
                    case 3:
                        enemyCounterJudge = false;
                        break;
                    case 5:
                        enemyCounterJudge = false;
                        break;
                }
            }
            return (playerCounterJudge, enemyCounterJudge);
        }

        //ダメージ計算
        private (int playerDamageValue, int enemyDamageValue, bool playerCounterJudge, bool enemyCounterJudge) Damage(int playerAction, int enemyAction)
        {
            int playerDamageValue = 0;
            int enemyDamageValue = 0;
            (bool playerCounterJudge, bool enemyCounterJudge) = CounterJudge(playerAction, enemyAction);

            var rand = new Random();
            switch (playerAction)
            {
                case 0:
                    playerDamageValue = rand.Next(20, 30);
                    break;
                case 1:
                    playerDamageValue = rand.Next(20, 30);
                    break;
                case 2:
                    playerDamageValue = rand.Next(10, 15);
                    break;
            }
            switch (enemyAction)
            {
                case 0:
                    enemyDamageValue = rand.Next(20, 30);
                    break;
                case 1:
                    enemyDamageValue = rand.Next(20, 30);
                    break;
                case 2:
                    enemyDamageValue = rand.Next(10, 15);
                    break;
            }


            //カウンターの処理
            if (playerCounterJudge)
            {
                playerDamageValue = enemyDamageValue;
                enemyDamageValue = 0;
            }
            if (enemyCounterJudge)
            {
                enemyDamageValue = playerDamageValue;
                playerDamageValue = 0;
            }
            return (playerDamageValue, enemyDamageValue, playerCounterJudge, enemyCounterJudge);
        }

        //HP計算
        private (int playerHp, int enemyHp) HpCalculate(int playerHp, int playerAction, int playerDamageValue, int enemyHp, int enemyAction, int enemyDamageValue)
        {
            //回復だけ区別
            if ((playerAction == 2) && (enemyAction == 2))
            {
                playerHp += playerDamageValue;
                enemyHp += enemyDamageValue;
            }
            else if (playerAction == 2)
            {
                playerHp += playerDamageValue - enemyDamageValue;
            }
            else if (enemyAction == 2)
            {
                enemyHp += enemyDamageValue - playerDamageValue;
            }
            else
            {
                playerHp -= enemyDamageValue;
                enemyHp -= playerDamageValue;
            }

            return (playerHp, enemyHp);
        }

        //勝敗判定（１：player勝利、０：継戦、ー１：enemy勝利）
        private int BattleJudge(int playerHp, int enemyHp)
        {

            if (playerHp == 0 && enemyHp == 0)
            {
                return 1;
            }
            else if (playerHp == 0)
            {
                return -1;
            }
            else if (enemyHp == 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        //先行判定
        private bool ActionJudge(bool counterJudge)
        {
            if (counterJudge == true)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
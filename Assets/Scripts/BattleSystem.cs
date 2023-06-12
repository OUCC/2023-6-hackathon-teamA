using System;
namespace HackathonA
{

    public class BattleSystem
    {
        void Main()
        {
            //BattleManager,ChatGPTから取得
            int playerHp = 100;
            int enemyHp = 100;
            int playerAction = 0;
            int enemyAction = 0;
            //勝敗判定
            bool actionJudge = true;
            //カウンターの成功判定
            bool playerCounterJudge = true;
            bool enemyCounterJudge = true;

            //playerと敵のダメージ計算
            int playerDamageValue = BattleSystem.Damage(playerAction);
            int enemyDamageValue = BattleSystem.Damage(enemyAction);


            //カウンターの処理
            if (playerAction == 3 || playerAction == 4)
            {
                playerCounterJudge = BattleSystem.CounterJudge(playerAction, enemyAction, playerCounterJudge);
                if (playerCounterJudge == true)
                {
                    playerDamageValue = enemyDamageValue;
                    enemyDamageValue = 0;

                }
                else if (playerCounterJudge == false)
                {
                    playerDamageValue = 0;
                }

            }
            else if (enemyAction == 3 || enemyAction == 4)
            {
                enemyCounterJudge = BattleSystem.CounterJudge(enemyAction, playerAction, enemyCounterJudge);
                if (enemyCounterJudge == true)
                {
                    enemyDamageValue = playerDamageValue;
                    playerDamageValue = 0;

                }
                else if (enemyCounterJudge == false)
                {
                    enemyDamageValue = 0;
                }
            }


            //HP計算
            playerHp = BattleSystem.HpCalculate(playerHp, playerAction, playerDamageValue);
            enemyHp = BattleSystem.HpCalculate(enemyHp, enemyAction, enemyDamageValue);

            //勝敗判定
            actionJudge = BattleSystem.BattleJudge(playerHp, enemyHp);

            //BattleDataに再度入力



        }

        //カウンターの成功判定（成功がTrue、失敗がFalse）
        private static bool CounterJudge(int activeAction, int passiveAction, bool counterJudge)
        {
            int _activeAction = activeAction;
            int _passiveAction = passiveAction;
            bool _counterJudge = counterJudge;
            if (_activeAction == 3)
            {
                if (_passiveAction == 0)
                {
                    _counterJudge = true;
                }
                else
                {
                    _counterJudge = false;
                }
            }

            else if (_activeAction == 4)
            {
                if (_passiveAction == 1)
                {
                    _counterJudge = true;
                }
                else
                {
                    _counterJudge = false;
                }
            }

            return _counterJudge;
        }

        //HP計算
        private static int HpCalculate(int hp, int action, int damageValue)
        {
            int _hp = hp;
            int _damageValue = damageValue;

            //回復だけ区別
            if (action == 2)
            {
                _hp = _hp + _damageValue;
            }
            else
            {
                _hp = _hp - _damageValue;
            }

            return _hp;
        }

        //勝敗判定
        private static bool BattleJudge(int playerhp, int enemyhp)
        {
            int _playerhp = playerhp;
            int _enemyhp = enemyhp;
            bool judge = true;

            if (_playerhp <= 0 && _enemyhp <= 0)
            {
                judge = true;

            }
            else if (_playerhp <= 0)
            {
                judge = true;
            }
            else if (_enemyhp <= 0)
            {
                judge = false;
            }

            return judge;
        }

        //両者共通のダメージ計算
        private static int Damage(int action)
        {
            var rand = new Random();
            int value = 0;
            switch (action)
            {
                case 0:
                    value = rand.Next(20, 30);
                    break;
                case 1:
                    value = rand.Next(20, 30);
                    break;
                case 2:
                    value = rand.Next(10, 15);
                    break;
                case 3:
                    value = rand.Next(20, 30);

                    break;
                case 4:
                    value = rand.Next(20, 30);
                    break;
            }
            return value;
        }





    }
}


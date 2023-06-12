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

            //playerと敵のダメージ計算
            int playerDamageValue = BattleSystem.Damage(playerAction);
            int enemyDamageValue = BattleSystem.Damage(enemyAction);
            switch (playerAction)
            {
                case 0:
                    if (enemyAction == 3)
                    {
                        enemyDamageValue = playerDamageValue;
                        playerDamageValue = 0;
                    }
                    else
                    {
                        enemyDamageValue = 0;
                    }
                    break;
                case 1:
                    if (enemyAction == 4)
                    {
                        enemyDamageValue = playerDamageValue;
                        playerDamageValue = 0;
                    }
                    else
                    {
                        enemyDamageValue = 0;
                    }
                    break;
                case 3:
                    if (enemyAction == 0)
                    {
                        playerDamageValue = enemyDamageValue;
                        enemyDamageValue = 0;
                    }
                    else
                    {
                        playerDamageValue = 0;
                    }
                    break;
                case 4:
                    if (enemyAction == 1)
                    {
                        playerDamageValue = enemyDamageValue;
                        enemyDamageValue = 0;
                    }
                    else
                    {
                        playerDamageValue = 0;
                    }
                    break;
            }

            //HP計算
            playerHp = BattleSystem.HpCalculate(playerHp, playerAction, playerDamageValue);
            enemyHp = BattleSystem.HpCalculate(enemyHp, enemyAction, enemyDamageValue);

            //勝敗判定
            bool actionJudge = true;
            actionJudge = BattleSystem.BattleJudge(playerHp, enemyHp);

            //BattleDataに再度入力



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

            if (_playerhp == 0 && _enemyhp == 0)
            {
                judge = true;

            }
            else if (_playerhp == 0)
            {
                judge = true;
            }
            else if (_enemyhp == 0)
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


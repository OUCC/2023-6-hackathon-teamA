using System;
namespace HackathonA
{

    public class BattleSystem
    {
        //カウンターの成功判定（成功が1、失敗が0）
        private static (int playerCounterJudge, int enemyCounterJudge) CounterJudge(int playerAction, int enemyAction)
        {
            int _playerAction = playerAction;
            int _enemyAction = enemyAction;
            int playerCounterJudge = 0;
            int enemyCounterJudge = 0;

            //player側の判定
            if (_playerAction == 3)
            {
                switch (_enemyAction)
                {
                    case 0:
                        playerCounterJudge = 1;
                        break;
                    case 1:
                        playerCounterJudge = 0;
                        break;
                    case 2:
                        playerCounterJudge = 0;
                        break;
                    case 3:
                        playerCounterJudge = 0;
                        break;
                    case 5:
                        playerCounterJudge = 0;
                        break;
                }
            }
            else if (_playerAction == 4)
            {
                switch (_enemyAction)
                {
                    case 0:
                        playerCounterJudge = 0;
                        break;
                    case 1:
                        playerCounterJudge = 1;
                        break;
                    case 2:
                        playerCounterJudge = 0;
                        break;
                    case 3:
                        playerCounterJudge = 0;
                        break;
                    case 5:
                        playerCounterJudge = 0;
                        break;
                }
            }

            //enemy側の判定
            else if (_enemyAction == 3)
            {
                switch (_playerAction)
                {
                    case 0:
                        enemyCounterJudge = 1;
                        break;
                    case 1:
                        enemyCounterJudge = 0;
                        break;
                    case 2:
                        enemyCounterJudge = 0;
                        break;
                    case 3:
                        enemyCounterJudge = 0;
                        break;
                    case 5:
                        enemyCounterJudge = 0;
                        break;
                }
            }
            else if (_enemyAction == 4)
            {
                switch (_playerAction)
                {
                    case 0:
                        enemyCounterJudge = 0;
                        break;
                    case 1:
                        enemyCounterJudge = 1;
                        break;
                    case 2:
                        enemyCounterJudge = 0;
                        break;
                    case 3:
                        enemyCounterJudge = 0;
                        break;
                    case 5:
                        enemyCounterJudge = 0;
                        break;
                }
            }
            return (playerCounterJudge, enemyCounterJudge);
        }

        //ダメージ計算
        private static (int playerDamageValue, int enemyDamageValue) Damage(int playerAction, int enemyAction)
        {
            int playerDamageValue = 0;
            int enemyDamageValue = 0;
            (int playerCounterJudge, int enemyCounterJudge) = BattleSystem.CounterJudge(playerAction, enemyAction);

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
            switch (playerCounterJudge)
            {
                case 0:
                    playerAction = 0;
                    break;

                case 1:
                    playerDamageValue = enemyDamageValue;
                    enemyDamageValue = 0;
                    break;
            }
            switch (enemyCounterJudge)
            {
                case 0:
                    enemyDamageValue = 0;
                    break;

                case 1:
                    enemyDamageValue = playerDamageValue;
                    playerDamageValue = 0;
                    break;
            }
            return (playerDamageValue, enemyDamageValue);
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

        //勝敗判定（１：player勝利、０：継戦、ー１：enemy勝利）
        private static int BattleJudge(int playerHp, int enemyHp)
        {
            int _playerhp = playerHp;
            int _enemyhp = enemyHp;
            int judge = 0;

            if (_playerhp <= 0 && _enemyhp <= 0)
            {
                judge = 1;

            }
            else if (_playerhp <= 0)
            {
                judge = -1;
            }
            else if (_enemyhp <= 0)
            {
                judge = 1;
            }

            return judge;
        }

    }
}
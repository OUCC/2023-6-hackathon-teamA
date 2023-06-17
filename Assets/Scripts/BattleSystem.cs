using System;

namespace HackathonA
{

    public class BattleSystem
    {
        public void Main(int playerAction)
        {
            Player player = new Player();
            Enemy enemy = new Enemy();

            player.ActionType = playerAction;

            //ChatGPTから取得
            enemy.ActionType = 0;

            (player.DamageValue, enemy.DamageValue, player.CounterJudge, enemy.CounterJudge) = Damage(player.ActionType, enemy.ActionType);

            player.Hp = HpCalculate(player.Hp, player.ActionType, player.DamageValue);
            enemy.Hp = HpCalculate(enemy.Hp, enemy.ActionType, enemy.DamageValue);


            int battleJudge = BattleJudge(player.Hp,enemy.Hp);
            bool moveJudge = MoveJudge(player.CounterJudge);
            BattleData battle = new BattleData(battleJudge,moveJudge);
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
        private (int playerDamageValue, int enemyDamageValue,bool playerCounterJudge,bool enemyCounterJudge) Damage(int playerAction, int enemyAction)
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
            switch (playerCounterJudge)
            {
                case true:
                    playerAction = 0;
                    break;

                case false:
                    playerDamageValue = enemyDamageValue;
                    enemyDamageValue = 0;
                    break;
            }
            switch (enemyCounterJudge)
            {
                case true:
                    enemyDamageValue = 0;
                    break;

                case false:
                    enemyDamageValue = playerDamageValue;
                    playerDamageValue = 0;
                    break;
            }
            return (playerDamageValue, enemyDamageValue, playerCounterJudge, enemyCounterJudge);
        }


        //HP計算
        private int HpCalculate(int hp, int action, int damageValue)
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
        private int BattleJudge(int playerHp, int enemyHp)
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
        private bool MoveJudge(bool counterJudge)
        {
            bool moveJudge = true;
            if (counterJudge == true)
            {
                moveJudge = false;
            }

            return moveJudge;

        }

    }
}
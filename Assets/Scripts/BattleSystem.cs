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
            BattleSystem battleSystem = new BattleSystem();
            int playerDamageValue = battleSystem.Damage(playerAction);
            int enemyDamageValue = battleSystem.Damage(enemyAction);
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
            playerHp = playerHp - enemyDamageValue;
            enemyHp = enemyHp - playerDamageValue;

            //終了判定
            bool actionJudge;
            if (playerHp == 0 && enemyHp == 0)
            {
                actionJudge = true;

            }
            else if (playerHp == 0)
            {
                actionJudge = true;
            }
            else if (enemyHp == 0)
            {
                actionJudge = false;
            }

            //BattleDataに再度入力



        }






        //両者共通のダメージ計算
        private int Damage(int action)
        {
            var rand = new Random();
            int value = 0;
            switch (action)
            {
                case 0:
                    value = -rand.Next(20, 30);
                    break;
                case 1:
                    value = -rand.Next(20, 30);
                    break;
                case 2:
                    value = rand.Next(10, 15);
                    break;
                case 3:
                    value = -rand.Next(20, 30);

                    break;
                case 4:
                    value = -rand.Next(20, 30);
                    break;
            }
            return value;
        }





    }
}


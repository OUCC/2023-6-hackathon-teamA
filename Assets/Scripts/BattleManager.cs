﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackathonA
{
    public class BattleManager : MonoBehaviour
    {
        const int attacking = 0;
        const int magicAttacking = 1;
        const int healing = 2;
        const int counter = 3;
        const int magicCounter = 4;
        private BattleSystem battleSystem;

        // Start is called before the first frame update
        void Start()
        {
            battleSystem = new BattleSystem();
        }

        /*
         actionText
         ・攻撃時、ダメージのテキストを返す。
             */
        public async (string, int, int) StateUpdateAsync(int action)
        {
            BattleData battleData = await battleSystem.BattleProcessAsync(action);

            int playerHp = battleSystem.Player.HP;
            int enemyHp = battleSystem.Enemy.HP;

            string sendMessage = "";
            if (battleData.actionJudge)
            {
                sendMessage += GenerateMessage(battleSystem.Player);
                sendMessage += GenerateMessage(battleSystem.Enemy);
            }
            else
            {
                sendMessage += GenerateMessage(battleSystem.Enemy);
                sendMessage += GenerateMessage(battleSystem.Player);
            }
            return (sendMessage, playerHp, enemyHp);
        }
        private string GenerateMessage(ICharactor charactor)
        {
            string message = "";
            string actor, target;
            if (charactor is Player)
            {
                actor = "Player";
                target = "Enemy";
            }
            else
            {
                actor = "Enemy";
                target = "Player";
            }
            int damageValue = charactor.DamageValue;
            bool counterJudge = charactor.CounterJudge;
            switch (charactor.ActionType)
            {
                case attacking:
                    message += $"{actor}の攻撃!\n";
                    message += $"{target}は{damageValue}のダメージを受けた";
                    break;
                case magicAttacking:
                    message += $"{actor}の魔法攻撃!\n";
                    message += $"{target}は{damageValue}のダメージを受けた";
                    break;
                case healing:
                    message += $"{actor}は{damageValue}回復した";
                    break;
                case counter:
                    message += $"{actor}のカウンター攻撃!\n";
                    if (counterJudge)
                    {
                        message += $"{target}は{damageValue}のダメージを受けた";
                    }
                    else
                    {
                        message += "しかし失敗に終わった。";
                    }
                    break;
                case magicCounter:
                    message += $"{actor}の魔法カウンター攻撃!\n";
                    if (counterJudge)
                    {
                        message += $"{target}は{damageValue}のダメージを受けた";
                    }
                    else
                    {
                        message += "しかし失敗に終わった。";
                    }
                    break;
                default:
                    message += "行動値が不正です";
                    break;
            }
            return message;
        }
    }
}

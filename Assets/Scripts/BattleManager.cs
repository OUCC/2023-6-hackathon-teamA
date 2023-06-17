using System.Collections;
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
        private Player playerData;
        private Enemy enemyData;

        // Start is called before the first frame update
        void Start()
        {
            battleSystem = new BattleSystem();
        }

        /*
         actionText
         ・攻撃時、ダメージのテキストを返す。
             */
        public void StateUpdate(int action)
        {
            BattleData battleData = battleSystem.GetBattleData(action);
            playerData = battleData.player;
            enemyData = battleData.enemy;
        }
        public string GetText()
        {
            string sendMessage = "";
            var charactorList = new List<string>();
            if (battleData.actionJudge)
            {
                sendMessage += GenerateMessage(playerData);
                sendMessage += GenerateMessage(enemyData);
            }
            else
            {
                sendMessage += GenerateMessage(enemyData);
                sendMessage += GenerateMessage(playerData);
            }
            return sendMessage;
            
        }
        public int GetHp(string charactorName)
        {
            int hp;
            switch (charactorName)
            {
                case "Player":
                    hp = playerData.HP;
                    break;
                case "Enemy":
                    hp = enemyData.HP;
                    break;
                default:
                    hp = -1;
                    break;
            }
            return hp;
        }
        private string GenerateMessage(ICharactor charactor)
        {
            string message = "";
            string actor, target;
            if (charactor.GetType() == "Player")
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

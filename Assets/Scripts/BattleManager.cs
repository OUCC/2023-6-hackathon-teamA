using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

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
         StateUpdateAsync
         引数: プレイヤーの行動
         返り値: (文章・プレイヤーのHP・敵のHP)の順のタプル
             */
        public async UniTask<(string, int, int)> StateUpdateAsync(int playerAction)
        {
            BattleData battleData = await battleSystem.BattleProcessAsync(playerAction);

            int playerHp = battleData.Player.HP;
            int enemyHp = battleData.Enemy.HP;

            string sendMessage = "";
            if (battleData.actionJudge)
            {
                sendMessage += GenerateMessage(battleData.Player);
                sendMessage += GenerateMessage(battleData.Enemy);
            }
            else
            {
                sendMessage += GenerateMessage(battleData.Enemy);
                sendMessage += GenerateMessage(battleData.Player);
            }
            return (sendMessage, playerHp, enemyHp);
        }
        private string GenerateMessage(ICharacter character)
        {
            string message = "";
            string actor, target;
            if (character is Player)
            {
                actor = "Player";
                target = "Enemy";
            }
            else
            {
                actor = "Enemy";
                target = "Player";
            }
            int damageValue = character.DamageValue;
            bool counterJudge = character.CounterJudge;
            switch (character.ActionType)
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

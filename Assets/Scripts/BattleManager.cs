using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace HackathonA
{
    public class BattleManager : MonoBehaviour
    {
        private const int Attacking = 0;
        private const int MagicAttacking = 1;
        private const int Healing = 2;
        private const int Counter = 3;
        private const int MagicCounter = 4;
        private BattleSystem battleSystem;

        // Start is called before the first frame update
        void Start()
        {
            battleSystem = new BattleSystem();
        }

        /// <summary>
        /// プレイヤーの選択を受け取って、戦闘結果を返す
        /// </summary>
        /// <param name="playerAction">プレイヤーの行動</param>
        /// <returns> (文章・プレイヤーのHP・敵のHP・カウンター判定・勝敗判定)の順のタプル</returns>
        public async UniTask<(string, string, int, int, bool, int)> StateUpdateAsync(int playerAction)
        {
            BattleData battleData = await battleSystem.BattleProcessAsync(playerAction);

            int playerHp = battleData.Player.Hp;
            int enemyHp = battleData.Enemy.Hp;

            string playerSendMessage, enemySendMessage;

            playerSendMessage = GenerateMessage(battleData.Player);
            enemySendMessage = GenerateMessage(battleData.Enemy);

            return (playerSendMessage, enemySendMessage, playerHp, enemyHp, battleData.ActionJudge, battleData.BattleJudge);
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
                case Attacking:
                    message += $"{actor}の攻撃!\n";
                    message += $"{target}は{damageValue}のダメージを受けた";
                    break;
                case MagicAttacking:
                    message += $"{actor}の魔法攻撃!\n";
                    message += $"{target}は{damageValue}のダメージを受けた";
                    break;
                case Healing:
                    message += $"{actor}は{damageValue}回復した";
                    break;
                case Counter:
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
                case MagicCounter:
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

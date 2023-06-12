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
        private BattleData battleData;
        // Start is called before the first frame update
        void Start()
        {
            battleSystem = new BattleSystem();
        }

        /*
         actionText
         ・攻撃時、ダメージのテキストを返す。
             */
        public string ActionText(int action)
        {
            battleData = battleSystem.GetBattleData(action);
            string message = "";
            var charactorList = new List<string>();
            if (battleData.actionJudge)
            {
                charactorList.Add("Player");
                charactorList.Add("Enemy");
            }
            else
            {
                charactorList.Add("Enemy");
                charactorList.Add("Player");
            }
            for (int i = 0; i < 2; i++)
            {
                string actor = charactorList[i];
                string target = charactorList[i ^ 1];
                switch (action)
                {
                    case attacking:
                        message += $"{actor}の攻撃!\n";
                        message += $"{target}は{battleData.damageValue[actor]}のダメージを受けた";
                        break;
                    case magicAttacking:
                        message += $"{actor}の魔法攻撃!\n";
                        message += $"{target}は{battleData.damageValue[actor]}のダメージを受けた";
                        break;
                    case healing:
                        message += $"{actor}は{battleData.damageValue[actor]}回復した";
                        break;
                    case counter:
                        message += $"{actor}のカウンター攻撃!\n";
                        if (battleData.counterJudge[actor])
                        {
                            
                            message += $"{target}は{battleData.damageValue[actor]}のダメージを受けた";
                        }
                        else
                        {
                            message += "しかし失敗に終わった。";
                        }
                        break;
                    case magicCounter:
                        message += $"{actor}の魔法カウンター攻撃!\n";
                        if (battleData.counterJudge[actor])
                        {
                            message += $"{target}は{battleData.damageValue[actor]}のダメージを受けた";
                        }
                        else
                        {
                            message += "しかし失敗に終わった。";
                        }
                        break;
                }
                if (i == 1)
                {
                    message += "\n";
                }
            }
            return message;
        }
    }
}

using System.Collections;
using System.Collections.Generic;

namespace HackathonA
{
    public class BattleData
    {
        //HP
        public readonly int hp;

        //行動（０：物理攻撃、１：魔法攻撃、２：回復、３：物理カウンター、４：魔法カウンター）
        public readonly int action;

        //ダメージ
        public readonly int damageValue;

        //カウンター成功判定（成功：１、失敗：０）

        public readonly int counterJudge;

        public BattleData(int _hp,int _action,int _damageValue, int _counterJudge)
        {
            this.hp = _hp;
            this.action = _action;
            this.damageValue = _damageValue;
            this.counterJudge = _counterJudge;

        }
    }
}


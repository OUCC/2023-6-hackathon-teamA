using System;

namespace HackathonA
{
    public class BattleData
    {
        //勝敗判定（１：player勝利、０：継戦、ー１：enemy勝利）
        public readonly int BattleJudge;
        //先行判定（true：player先行）
        public readonly bool MoveJudge;
        public readonly Player Player;
        public readonly Enemy Enemy;

        public BattleData(int battleJudge, bool moveJudge, Player player, Enemy enemy)
        {
            this.BattleJudge = battleJudge;
            this.MoveJudge = moveJudge;
            this.Player = player;
            this.Enemy = enemy;
        }
    }
}


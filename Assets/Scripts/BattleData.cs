using System.Collections;
using System.Collections.Generic;

namespace HackathonA
{
    interface ICharactar
    {
        int Hp { get; set; }
        int ActionType { get; set; }
        int DamageValue { get; set; }
        bool CounterJudge { get; set; }
    }

    public class Player : ICharactar
    {
        public Player(int hp = 100, int actionType = 0, int damageValue = 0, bool counterJudge = false)
        {
            Hp = hp;
            ActionType = actionType;
            DamageValue = damageValue;
            CounterJudge = counterJudge;
        }

        public int Hp
        {
            get => _hp;
            set => _hp = value > 0 ? value : 0;
        }
        private int _hp;

        public int ActionType { get; set; }
        public int DamageValue { get; set; }
        public bool CounterJudge { get; set; }

    }
    public class Enemy : ICharactar
    {
        public Enemy(int hp = 100, int actionType = 0, int damageValue = 0, bool counterJudge = false)
        {
            Hp = hp;
            ActionType = actionType;
            DamageValue = damageValue;
            CounterJudge = counterJudge;
        }

        public int Hp
        {
            get => _hp;
            set => _hp = value > 0 ? value : 0;
        }
        private int _hp;

        public int ActionType { get; set; }
        public int DamageValue { get; set; }
        public bool CounterJudge { get; set; }

    }


    public class BattleData
    {
        //勝敗判定（１：player勝利、０：継戦、ー１：enemy勝利）
        public readonly int battleJudge;
        //先行判定（true：player先行）
        public readonly bool moveJudge;

        public BattleData(int battleJudge, bool moveJudge)
        {
            this.battleJudge = battleJudge;
            this.moveJudge = moveJudge;
        }
    }
}


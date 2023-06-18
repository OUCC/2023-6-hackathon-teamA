using System;

namespace HackathonA
{
    public class Player : ICharacter
    {
        public Player(int hp = 100, int actionType = 0, int damageValue = 0, bool counterJudge = false)
        {
            Hp = hp;
            ActionType = actionType;
            DamageValue = damageValue;
            CounterJudge = counterJudge;
        }

        public int Hp { get; set; }
        public int ActionType { get; set; }
        public int DamageValue { get; set; }
        public bool CounterJudge { get; set; }

    }
}
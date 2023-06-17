using System;

namespace HackathonA
{
    public class Enemy : ICharactar
    {
        public Enemy(int hp = 100, int actionType = 0, int damageValue = 0, bool counterJudge = false, int enemyType = 0)
        {
            Hp = hp;
            ActionType = actionType;
            DamageValue = damageValue;
            CounterJudge = counterJudge;
            this.EnemyType = enemyType;
        }

        public int Hp { get; set; }
        public int ActionType { get; set; }
        public int DamageValue { get; set; }
        public bool CounterJudge { get; set; }
        public int EnemyType { get; set; }

    }
}
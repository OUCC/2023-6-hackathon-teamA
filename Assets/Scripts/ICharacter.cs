using System;

namespace HackathonA
{
    interface ICharacter
    {
        int Hp { get; set; }
        int ActionType { get; set; }
        int DamageValue { get; set; }
        bool CounterJudge { get; set; }
    }
}
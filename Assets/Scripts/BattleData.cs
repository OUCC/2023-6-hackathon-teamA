using System.Collections;
using System.Collections.Generic;

namespace HackathonA
{
    public class BattleData
    {
        //playerのHP
        public readonly int playerHp;

        //敵のHP
        public readonly int enemyHp;

        //playerの行動（０：物理攻撃、１：魔法攻撃、２：回復、３：物理カウンター、４：魔法カウンター）
        public readonly int playerAction;

        //敵の行動（０：物理攻撃、１：魔法攻撃、２：回復、３：物理カウンター、４：魔法カウンター）
        public readonly int enemyAction;

        //playerのダメージ
        public readonly int playerDamageValue;

        //敵のダメージ
        public readonly int enemyDamageValue;

        //終了判定（TrueがPlayer先行、Falseが敵先行）
        public readonly bool actionJudge;

        public BattleData(int _playerHp,int _enemyHp, int _playerAction, int _enemyAction, int _playerDamageValue, int _enemyDamageValue, bool _actionJudge)
        {
            this.playerHp = _playerHp;
            this.enemyHp = _enemyHp;
            this.playerAction = _playerAction;
            this.enemyAction = _enemyAction;
            this.playerDamageValue = _playerDamageValue;
            this.enemyDamageValue = _enemyDamageValue;
            this.actionJudge = _actionJudge;

        }
    }
}


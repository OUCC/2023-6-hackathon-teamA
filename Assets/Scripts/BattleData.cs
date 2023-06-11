using System.Collections;
using System.Collections.Generic;

namespace HackathonA{
    public readonly struct BattleData{
    //playerのHP
    public int playerHp;
    
    //敵のHP
    public int enemyHp;

    //playerの行動（０：物理攻撃、１：魔法攻撃、２：回復、３：物理カウンター、４：魔法カウンター）
    public int playerAction;

    //敵の行動（０：物理攻撃、１：魔法攻撃、２：回復、３：物理カウンター、４：魔法カウンター）
    public int enemyAction;

    //playerのダメージ
    public int playerDamageValue;

    //敵のダメージ
    public int enemyDamageValue;

    //終了判定
    public bool actionJudge;
    }
}


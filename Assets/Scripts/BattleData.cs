using System.Collections;
using System.Collections.Generic;

struct BattleData{
    //playerのHP
    int playerHp;
    
    //敵のHP
    int enemyHp;

    //playerの行動（０：物理攻撃、１：魔法攻撃、２：回復、３：物理カウンター、４：魔法カウンター）
    int playerAction;

    //敵の行動（０：物理攻撃、１：魔法攻撃、２：回復、３：物理カウンター、４：魔法カウンター）
    int enemyAction;

    //playerのダメージ
    int playerDamageValue;

    //敵のダメージ
    int enemyDamageValue;

    //終了判定
    bool actionJudge;
}

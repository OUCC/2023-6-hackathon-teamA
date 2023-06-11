using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    const int attacking = 0;
    const int magicAttacking = 1;
    const int healing = 2;
    const int counter = 3;
    const int magicCounter = 4;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    /*
     actionText
     ・攻撃時、ダメージのテキストを返す。
         */
    string actionText(int action, int absDamage, string actor)
    {
        string message = "";
        string target;
        if (actor == "player")
        {
            target = "enemy";
        }
        else
        {
            target = "player";
        }
        switch (action)
        {
            case attacking:
                message += actor + "の攻撃!\n";
                message += target + "は" + absDamage.ToString() + "のダメージを受けた";
                break;
            case magicAttacking:
                message += actor + "の魔法攻撃!\n";
                message += target + "は" + absDamage.ToString() + "のダメージを受けた";
                break;
            case healing:
                message += actor + "は" + absDamage.ToString() + "回復した";
                break;
            case counter:
                message += actor + "のカウンター攻撃!\n";
                message += target + "は" + absDamage.ToString() + "のダメージを受けた";
                break;
            case magicCounter:
                message += actor + "の魔法カウンター攻撃!\n";
                message += target + "は" + absDamage.ToString() + "のダメージを受けた";
                break;
        }
        return message;
    }
}

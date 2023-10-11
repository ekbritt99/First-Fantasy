using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public Slider hpSlider;

    //Sets the battle HUD's sliders to their max values and the slider's position according to the hp of the player and the enemy.
    public void setHUD(int maxHP, int currentHP)
    {
        hpSlider.maxValue = maxHP;
        hpSlider.value = currentHP;
    }
    //Sets the slider's position to the fighter's current HP
    public void setHP(int hp)
    {
        hpSlider.value = hp;
    }
}

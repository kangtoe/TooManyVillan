using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VillainDBEntity
{
    public int      id;
    public string   Vil_Name;
    public int      Vil_Grade;
    public int      Vil_AttackType;
    public int      Range_Normal;
    public int      Range_Escape;
    public int      Vil_Synergy_1;
    public int      Vil_Synergy_2;
    public int      Vil_Skill;
    public float    Vil_Cooltime;
    public int      Vil_Hp_Min;
    public int      Vil_Hp_Max;
    public int      Vil_Str_Min;
    public int      Vil_Str_Max;
    public bool     Vil_Demon;
}

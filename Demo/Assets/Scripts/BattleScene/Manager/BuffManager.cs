using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    public static BuffManager instance; // 싱글톤을 할당할 전역 변수

    public int count_Newbie = 0;
    public int count_Saibi = 0;

    private void Awake()
    {

        if (null == instance)
        {
            instance = this;

        }
        else
        {
            Destroy(this.gameObject);
        }


    }

    private void Update()
    {

    }


    // 시너지 체크 후 버프 발동
    public void SynergyCheck()
    {
        Buff_Newbie();
        Buff_Saibi();
    }

    #region Buff_Synergy
    // 뉴비 시너지 버프
    private void Buff_Newbie()
    {

        int GroupCount = BattleSceneManager.instance.playerGroup.characterGroup.Count;

        // 팀의 모든 인원이 뉴비일 경우
        if (count_Newbie >= GroupCount)
        {
            for (int i = 0; i < GroupCount; i++)
            {
                BattleSceneManager.instance.playerGroup.characterGroup[i].synergyAttackMult *= 1.2f;
            }
        }

        // 시너지 발동 유무에 상관없이 카운트를 다시 초기화 해준다.
        count_Newbie = 0;
    }

    // 사이비 시너지 버프
    private void Buff_Saibi()
    {
        int GroupCount = BattleSceneManager.instance.playerGroup.characterGroup.Count;

        switch (count_Saibi)
        {
            case 0:
                break;

            case 1:
                {
                    for (int i = 0; i < GroupCount; i++)
                    {
                        BattleSceneManager.instance.playerGroup.characterGroup[i].healthAdd += 10;
                    }
                }
                break;

            case 2:
                {
                    for (int i = 0; i < GroupCount; i++)
                    {
                        BattleSceneManager.instance.playerGroup.characterGroup[i].healthAdd += 30;
                    }
                }
                break;

            case 3:
                {
                    for (int i = 0; i < GroupCount; i++)
                    {
                        BattleSceneManager.instance.playerGroup.characterGroup[i].healthAdd += 50;
                    }
                }
                break;

            // 4명이거나 그 이상일 경우
            default:
                {
                    for (int i = 0; i < GroupCount; i++)
                    {
                        BattleSceneManager.instance.playerGroup.characterGroup[i].healthAdd += 100;
                    }
                }
                break;
        }

        // 시너지 발동 유무에 상관없이 카운트를 다시 초기화 해준다.
        count_Saibi = 0;
    }
    #endregion Buff_Synergy
}
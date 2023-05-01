using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    public static BuffManager instance; // 싱글톤을 할당할 전역 변수

    public int count_Newbie = 0;
    public int count_Saibi = 0;
    public int count_NightVil = 0;
    public int count_Vampire = 0;
    public int count_Ace = 0;
    public int count_Interest = 0;
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


    public void SynergyCheck()
    {
        Buff_Newbie();
        Buff_Saibi();
        Buff_NightVil();
        Buff_Vampire();
        Buff_Ace();
        Buff_Interest();
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

        count_Saibi = 0;
    }

    private void Buff_NightVil()
    {
        int GroupCount = BattleSceneManager.instance.playerGroup.characterGroup.Count;

        if (count_NightVil == 2)
        {
            for (int i = 0; i < GroupCount; i++) // 전체 캐릭터들 중에서
            {
                BaseCharacterController character = BattleSceneManager.instance.playerGroup.characterGroup[i];

                for (int j = 0; j < character.synergys.Count; j++) // 각 캐릭터가 가지고 있는 시너지들 중에서
                {
                    if (character.synergys[j].mType == SynergyBase.ESynergyType.NightVil) // 어둠의 자식이 있으면 해당 캐릭터 버프
                    {
                        for (int l = 0; l < character.attackBehaviours.Count; l++)
                        {
                            character.attackBehaviours[l].coolTime *= 0.7f; // 각각의 스킬 쿨타임 30퍼 감소

                        }
                        character.synergyAttackMult *= 1.3f; // 공격력 30퍼 증가
                        break;
                    }

                }

            }

        }
        count_NightVil = 0;
    }

    private void Buff_Vampire()
    {
        int GroupCount = BattleSceneManager.instance.playerGroup.characterGroup.Count;

        int Rand = Random.Range(0, 2); // 0과 1을 무작위로 뽑아냄
        if (count_Vampire == 2)
        {
            for (int i = 0; i < GroupCount; i++)
            {
                BaseCharacterController character = BattleSceneManager.instance.playerGroup.characterGroup[i];

                for (int j = 0; j < character.synergys.Count; j++)
                {
                    if (character.synergys[j].mType == SynergyBase.ESynergyType.Vampire) // 만약 가지고 있는 시너지 중에 박쥐가 있으면 해당 캐릭터 버프
                    {
                        switch (Rand)
                        {
                            case 0:
                                character.synergyAttackMult *= 1.3f; // 0인 경우 공격력 1.3배 증가
                                Debug.Log("뱀파이어 synergyAttackMult ");
                                break;
                            case 1:
                                character.isVampire = true; // 1인 경우 흡혈 가능 여부 체크
                                Debug.Log("뱀파이어 isVampire ");
                                break;
                        }
                        break; // 박쥐 시너지를 활성화 시켜줬거나 없으면 바로 다음 캐릭터 검사
                    }


                }
            }
        }

        count_Vampire = 0;
    }

    private void Buff_Ace()
    {
        int GroupCount = BattleSceneManager.instance.playerGroup.characterGroup.Count;

        if (count_Ace == 1) // 열등감이 한명인 경우에만 실행
        {
            for (int i = 0; i < GroupCount; i++) // 전체 캐릭터들 중에서
            {
                BaseCharacterController character = BattleSceneManager.instance.playerGroup.characterGroup[i];

                for (int j = 0; j < character.synergys.Count; j++) // 각 캐릭터가 가지고 있는 시너지들 중에서
                {
                    if (character.synergys[j].mType == SynergyBase.ESynergyType.Ace) // 열등감이 있으면 해당 캐릭터가 제일 높은 공격력인지 검사
                    {
                        for (int l = 0; l < GroupCount; l++)
                        {
                            if (l != i) // 자신의 경우 검사 패스 
                            {
                                if (character.strength <= BattleSceneManager.instance.playerGroup.characterGroup[l].strength) //열듬감의 공격력이 팀원보다 작거나 같은 경우엔 공격력 감소
                                {
                                    character.synergyAttackMult *= 0.8f; // 20퍼만큼 감소
                                    break;
                                }
                            }

                            if (l == GroupCount - 1) // 끝까지 검사를 다했다면 자신의 공격력과 같거나 높은 사람이 없으므로 공격력 증가
                            {
                                character.synergyAttackMult *= 1.3f; // 30퍼만큼 증가
                            }
                        }

                    }

                }

            }

        }
        count_Ace = 0;
    }

    private void Buff_Interest()
    {

        int GroupCount = BattleSceneManager.instance.playerGroup.characterGroup.Count;

        switch (count_Interest)
        {
            case 0:
                break;

            case 1:
                for(int i = 0; i < GroupCount; i ++)
                {
                    BaseCharacterController character = BattleSceneManager.instance.playerGroup.characterGroup[i];
                    character.synergyAttackMult *= 0.9f; // 데미지 10퍼만큼 감소
                    character.damageDecreaseMult *= 0.9f; // 받는 데미지도 10퍼만큼 감소
                }
                break;
            case 2:
                for (int i = 0; i < GroupCount; i++)
                {
                    BaseCharacterController character = BattleSceneManager.instance.playerGroup.characterGroup[i];
                    character.synergyAttackMult *= 0.95f; // 데미지 5퍼만큼 감소
                    character.damageDecreaseMult *= 0.8f; // 받는 데미지도 10퍼만큼 감소
                }
                break;

            default:
                break;
        }

        count_Interest = 0;
    }

    #endregion Buff_Synergy
}

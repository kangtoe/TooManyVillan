using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupObject : MonoBehaviour
{

    public List<BaseCharacterController> characterGroup = new List<BaseCharacterController>();

    //private float groupCenterX; // 생존한 캐릭터들의 평균 x축 위치

    public float interval = 1.0f; // 그룹내 캐릭터와 캐릭터 사이의 거리 

    public float groupSpeed = 1.0f;
    public bool AllCharactersWaiting => AllCharactersWaitingCheck();

    public bool AllCharactersAttacking => AllCharactersAttackingCheck();

    // Parameter : 중앙 지점, 일정 간격을 두려는 오브젝트수, 오브젝트 간의 거리
    // Return : 배치할려는 오브젝트들이 중앙 지점으로부터 얼마나 떨어져야 하는 지 반환


    

    public float[] GetPoints(float centerX, int objectCount, float interval) 
    {
        float[] xPosArray = new float[objectCount];
        
        for (int j = 0; j < objectCount; j++)
        {
            if (characterGroup.Count % 2 == 0) // 팀원이 짝수인 경우
            {
                xPosArray[j] = centerX + ((objectCount / 2) - j) * interval - (interval / (float)2.0); // groupPoint에서 상대적으로 3, 1, -1, -3인 float값
            }
            else // 팀원이 홀수인 경우
            {
                xPosArray[j] = centerX + (((objectCount - 1) / 2) - j) * interval;  // groupPoint에서 상대적으로 2, 1, 0, -1, -2인 float값
            }
        }

        return xPosArray;
    }

    //오브젝트들의 x축의 평균값을 반환
    public float GetCenter(Transform[] transforms)
    {
        float f = 0;

        for (int i = 0; i < transforms.Length; i++)
        {
            f += transforms[i].position.x;
        }

        return f / transforms.Length;

    }

    // BattleSceneManager의 BattleEnd 시 호출한다.
    // 캐릭터 정렬
    public void AlignCharacters() 
    {
        //캐릭터 그룹의 중앙 지점을 구한다.

        Transform[] characters = new Transform[characterGroup.Count];

        for (int i = 0; i < characterGroup.Count; i++) 
        {
            characters[i] = characterGroup[i].gameObject.transform;
        }

        float centerX = GetCenter(characters);

        // 각 캐릭터의 배치지점을 구한다.

        float[] movePoints = GetPoints(centerX, characterGroup.Count, interval);

        //구한 배치지점대로 캐릭터 이동
        for (int i = 0; i < movePoints.Length; i++)
        {
            characterGroup[i].allignPoint = movePoints[i];
            characterGroup[i].allignTrigger = true;
        }

        
    }

    //정렬 후 캐릭터 이동
    public void AfterAllign_CharacterMove()
    {
        for (int i = 0; i < characterGroup.Count; i++)
        {
            characterGroup[i].waitingToMoveTrigger = true;
        }
    }

    // 정렬 후 모두 대기중인가?
    private bool AllCharactersWaitingCheck()
    {
        for (int i = 0; i < characterGroup.Count; i++) 
        {
            if (characterGroup[i].waitingTrigger != true)
            {
                return false;
            }
        }

        return true;
    }

    private bool AllCharactersAttackingCheck()
    {
        return true;
    }




    public void RemoveCharacterInGroup(BaseCharacterController character) // 플레이어 구성원들이 사망 시 초기화, DeadState 전환시 호출
    {
        characterGroup.Remove(character); 
    }

    
    


}

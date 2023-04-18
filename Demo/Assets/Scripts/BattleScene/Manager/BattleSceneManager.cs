using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSceneManager : MonoBehaviour
{
    public static BattleSceneManager instance; // 싱글톤을 할당할 전역 변수

    public bool isEngaging = false; // 교전 중인가?
    
    public GroupObject playerGroup; // 플레이어 그룹, 게임 매니저에서 공격대 배치 씬에서 넘어올 때 초기화시켜준다.

    public GameObject enemyTrigger; // 이 오브젝트와 트리거되면 적이 생성되도록 한다.

    public GroupObject enemyGroup; // 교전 그룹

    public float playerGroupRespawnPosX; // 우리 팀 생성 위치

    public float enemyGroupRespawnPosx; // 적들 생성 위치

    public float bossRespawnPosX; // 보스 생성 위치


    //첫번째, prefab을 둬서 캐릭터를 생성하게 한다.

    //두번째, DataTable로 캐릭터의 정보를 담는다.

    //세번째, 클래스를 선언해서 Static하게 가지고 있는다.

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

        playerGroup.characterGroup.Clear();
        enemyGroup.characterGroup.Clear();

        Invoke("CreatePlayer", 0f);

        Invoke("CreateEnemy", 3f);

        Invoke("CreateBoss", 33f);

    }

    private void Update()
    {
        if (playerGroup.characterGroup.Count != 0) // 캐릭터가 생존해 있는 경우에만
        {
            BattleStart();
            BattleEnd();
        }
       
    }

    // playerGroup 전투 시작
    public void BattleStart() 
    {
        if (enemyGroup.characterGroup.Count != 0 && !isEngaging) // 적 그룹이 있고, 상태가 비교전 중이었을 때,
        {
            isEngaging = true;
        }
        
        
    }

    // playerGroup 전투 종료

    public void BattleEnd()  
    {
        // 플레이어 그룹 정렬

        if (enemyGroup.characterGroup.Count == 0 && isEngaging) // 적 그룹이 없고, 상태가 교전 중이었을 때,
        {
            isEngaging = false;

            playerGroup.AlignCharacters();

            enemyGroup.characterGroup.Clear(); 
         
        }

        // 정렬 완료 시 플레이어 이동

        if (playerGroup.AllCharactersWaiting && !isEngaging) // 생존한 캐릭터가 있고, 모두 대기중이며, 교전이 아닌 상태에서
        {
            playerGroup.AfterAllign_CharacterMove();
        }
        
    }

    

    public void CreatePlayer() // 스테이지 입장 시 위치에 맞게 캐릭터 생성
    {

        // 플레이어 생성 전에 그룹을 비워준다.

        if (playerGroup.characterGroup != null)
        {
            playerGroup.characterGroup.Clear();
        }

        GameObject[] gameObjects = GameManager.instance.GetCharacterList();

        // 지정한 groupCenterX에 플레이어들이 리스폰 할 위치들을 저장

        float[] characPos = playerGroup.GetPoints(playerGroupRespawnPosX, gameObjects.Length, playerGroup.interval);

        
        for (int i = 0; i < gameObjects.Length; i++)
        {
            
            GameObject playerClone = Instantiate(gameObjects[i], new Vector3 (characPos[i],-2 + (0.00001f * i)), gameObjects[i].transform.rotation); //  new Vector3 (characPos[i], -2) = 임시 위치
            BaseCharacterController character = playerClone.GetComponent<BaseCharacterController>(); // 캐릭터에서 컨트롤러를 가져와 그룹에 넣어준다
            playerGroup.characterGroup.Add(character);
            character.MyGroup = playerGroup;

            for (int j = 0; j < character.synergys.Count; j++)
            {
                // 각 캐릭터들의 시너지들을 버프 매니저에 전달한다. 
                character.synergys[j].AddSynergyCount();
            }
        }

        // 버프 매니저쪽의 시너지 체크해주는 함수 호출 
        BuffManager.instance.SynergyCheck();
    }

    public void CreateEnemy() // 적 트리거 발동 시, 적 생성
    {
        //적 생성 전에 그룹을 비워준다.

        if (enemyGroup.characterGroup != null)
        {
            enemyGroup.characterGroup.Clear();
        }

        GameObject[] gameObjects = GameManager.instance.GetEnemyList();

        // 지정한 groupCenterX에 적들이 리스폰 할 위치들을 저장

        float[] enemyPos = enemyGroup.GetPoints(enemyGroupRespawnPosx, gameObjects.Length, enemyGroup.interval);

        
        for (int i = 0; i < gameObjects.Length; i++)
        {
            //float yPos = Random.Range(0, 1.0f);
            GameObject enemyClone = Instantiate(gameObjects[gameObjects.Length - i -1], new Vector3(enemyPos[i], -2 + (0.00001f * i)), gameObjects[i].transform.rotation); //  new Vector3 (characPos[i], -2) = 임시 위치
            BaseCharacterController enemy = enemyClone.GetComponent<BaseCharacterController>(); // 캐릭터에서 컨트롤러를 가져와 그룹에 넣어준다
            enemyGroup.characterGroup.Add(enemy);
            enemy.MyGroup = enemyGroup;
        }

        BattleStart(); // 적 생성과 동시에 전투 진입
    }

    public void CreateBoss() // 보스 생성
    {
        //적 생성 전에 그룹을 비워준다.

        if (enemyGroup.characterGroup != null)
        {
            enemyGroup.characterGroup.Clear();
        }

        GameObject gameObject = GameManager.instance.GetBoss();

        // 지정한 groupCenterX에 적들이 리스폰 할 위치들을 저장


        GameObject enemyClone = Instantiate(gameObject, new Vector3(bossRespawnPosX, -2 + (0.00001f)), gameObject.transform.rotation); //  new Vector3 (characPos[i], -2) = 임시 위치
        BaseCharacterController enemy = enemyClone.GetComponent<BaseCharacterController>(); // 캐릭터에서 컨트롤러를 가져와 그룹에 넣어준다
        enemyGroup.characterGroup.Add(enemy);
        enemy.MyGroup = enemyGroup;


        BattleStart(); // 적 생성과 동시에 전투 진입
    }





}

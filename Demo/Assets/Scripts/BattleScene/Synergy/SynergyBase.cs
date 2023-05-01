using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SynergyBase : MonoBehaviour
{
    public enum EActivateTime { Always, Day, Night };


    public enum ESynergyType
    {
        None,
        Newbie,
        Saibi,
        NightVil, 
        Vampire, // 뱀파이어인지 아닌지는 isVampire로 BaseCharacterController 쪽에 구현함
        Ace,
        Interest,
        Wolf
    }



    #region Variables


    // 시너지가 켜지는 인게임 타임 , 버프가 켜지는 것으로 따지면 버프 쪽에서 관리할수도 
    public EActivateTime mAtivateTime;
    public ESynergyType mType;
    #endregion Variables
    protected virtual void Awake()
    {

    }

    public abstract void AddSynergyCount();

}

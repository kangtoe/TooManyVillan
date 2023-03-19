using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SynergyBase : MonoBehaviour
{
    public enum ActivateTime { Always, Day, Night };


    #region Variables

    // 시너지의 종류

    // 시너지가 켜지는 인게임 타임 , 버프가 켜지는 것으로 따지면 버프 쪽에서 관리할수도 
    public ActivateTime mAtivateTime;

    #endregion Variables

    public abstract void AddSynergyCount();

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager : MonoBehaviour
{
    [SerializeField]
    GameObject employTab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenTab_employ()
    {
        employTab.SetActive(true);
    }

    public void CloseAllTabs()
    {
        employTab.SetActive(false);
        // 다른 탭 추가되면 해당 탭에 대해서도 동일하게 동작할 것
    }
}

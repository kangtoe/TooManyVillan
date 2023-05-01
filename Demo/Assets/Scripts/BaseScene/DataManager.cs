using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public List<Characters> CharacterPool;
    public int gold;
    public int poolSize = 20;

    [SerializeField]
    private int InitialGold;

    private void Start() {
        Init();
    }

    void Init()
    {
        gold = InitialGold;
        CharacterPool = new List<Characters>();
    }
}

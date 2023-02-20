using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region load scene

    public void MoveScene(int i)
    {
        if (i < 0 || i > SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log("scene index is not available! : " + i);
            return;
        }

        SceneManager.LoadScene(i);
    }
    public void MoveScene(string str)
    {
        SceneManager.LoadScene(str);
    }

    #endregion load scene


}

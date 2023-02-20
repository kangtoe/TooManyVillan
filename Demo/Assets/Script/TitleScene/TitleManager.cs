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
        Scene scene = SceneManager.GetSceneByName(str);
        if (scene == null)
        {
            Debug.Log("scene name is not available! : " + str);
            return;
        }

        int i = scene.buildIndex;
        MoveScene(i);
    }

    #endregion load scene


}

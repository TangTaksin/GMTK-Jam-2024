using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
{
    public void SceneLoad(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }
}

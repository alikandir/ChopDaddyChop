using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneSceneChanger : MonoBehaviour
{
    public string sceneName;
    public float cutSceneTime;
    private void Start()
    {
        StartCoroutine(ChangeScene());
    }
    private IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(cutSceneTime);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}

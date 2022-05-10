using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScript : MonoBehaviour
{
    private static LoadingScript instance;
    private static bool needOpenSceneAnimation = false;
    private AsyncOperation operation;

    [SerializeField]
    private Sprite[] pullOfSpaces;
    [SerializeField]
    private Image space;
    [SerializeField]
    private Animator animator;

    void Start()
    {
        instance = this;

        if(needOpenSceneAnimation) instance.animator.SetTrigger("OpenScene");
    }

    public static void LoadScene(string sceneName)
    {
        instance.animator.SetTrigger("CloseScene");

        instance.operation = SceneManager.LoadSceneAsync(sceneName);
        instance.operation.allowSceneActivation = false;

    }

    public void CloseScene()
    {
        int SpriteIndex = Random.Range(0, pullOfSpaces.Length);
        if(SpriteIndex < pullOfSpaces.Length)
        {
            space.color = Color.white;
            space.sprite = pullOfSpaces[SpriteIndex];
        }
        else
        {
            space.color = Color.black;
        }
    }

    public void OpenScene()
    {
        needOpenSceneAnimation = true;
        instance.operation.allowSceneActivation = true;
    }
}

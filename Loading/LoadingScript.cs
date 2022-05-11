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
    private static Sprite spaceSprite;
    [SerializeField]
    private Animator animator;

    void Start()
    {
        instance = this;
        space = GameObject.Find("LoadingSpace").GetComponent<Image>();
        space.sprite = spaceSprite;
        if (needOpenSceneAnimation) instance.animator.SetTrigger("OpenScene");
    }

    public static void LoadScene(string sceneName)
    {
        instance.CloseSceneImage();
        instance.animator.SetTrigger("CloseScene");

        instance.operation = SceneManager.LoadSceneAsync(sceneName);
        instance.operation.allowSceneActivation = false;

    }

    public void CloseSceneImage()
    {
        int SpriteIndex = Random.Range(0, pullOfSpaces.Length);

        space.color = Color.white;
        spaceSprite = pullOfSpaces[SpriteIndex];
        space.sprite = spaceSprite;
    }

    public void OpenScene()
    {
        needOpenSceneAnimation = true;
        instance.operation.allowSceneActivation = true;
    }
}

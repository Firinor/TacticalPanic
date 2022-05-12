using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneScript : MonoBehaviour
{
    private static LoadingSceneScript instance;
    private static bool needOpenSceneAnimation = false;
    private AsyncOperation operation;
    private static bool CloseSceneFlag = false;
    private static bool OpenSceneFlag = false;

    [SerializeField]
    private Sprite[] pullOfSpaces;
    [SerializeField]
    private Image space;
    private static Sprite spaceSprite;
    [SerializeField]
    private Material loadingImageMaterial;
    [SerializeField]
    private AnimationCurve curve;
    [SerializeField]
    private float playTime;
    private float currentPlayTime;
    [SerializeField]
    private float endPortalPosition = 2f;

    void Start()
    {
        instance = this;
        space = GetComponentInChildren<Image>();
        space.sprite = spaceSprite;
        if (needOpenSceneAnimation)
            OpenScene();
    }

    void Update()
    {
        if (CloseSceneFlag)
        {
            currentPlayTime += Time.deltaTime;
            float presentage = currentPlayTime / playTime;
            float Diameter = Mathf.Lerp(0f, endPortalPosition, curve.Evaluate(presentage));
            loadingImageMaterial.SetFloat("InPortal", 1f);
            loadingImageMaterial.SetFloat("Diameter", Diameter);
            if (Diameter >= endPortalPosition)
            {
                CloseSceneFlag = false;
                needOpenSceneAnimation = true;
                instance.operation.allowSceneActivation = true;
            }
        }
        if (OpenSceneFlag)
        {
            currentPlayTime += Time.deltaTime;
            float presentage = currentPlayTime / playTime;
            float Diameter = Mathf.Lerp(0f, endPortalPosition, curve.Evaluate(presentage));
            loadingImageMaterial.SetFloat("InPortal", 0f);
            loadingImageMaterial.SetFloat("Diameter", Diameter);
            if (Diameter >= endPortalPosition)
            {
                OpenSceneFlag = false;
            }
        }
    }

    public static void LoadScene(string sceneName)
    {
        instance.SetSceneImage();
        instance.CloseScene();
        instance.operation = SceneManager.LoadSceneAsync(sceneName);
        instance.operation.allowSceneActivation = false;
        
    }

    public void SetSceneImage()
    {
        int SpriteIndex = Random.Range(0, pullOfSpaces.Length);
        spaceSprite = pullOfSpaces[SpriteIndex];
        space.sprite = spaceSprite;
    }

    public void CloseScene()
    {
        CloseSceneFlag = true;
        currentPlayTime = 0f;
    }

    public void OpenScene()
    {
        OpenSceneFlag = true;
        currentPlayTime = 0f;
    }
}

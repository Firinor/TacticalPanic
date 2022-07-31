using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TacticalPanicCode
{
    public class LoadingTransitionOperator : SinglBehaviour<LoadingTransitionOperator>
    {
        private static SceneManager sceneManager;
        //private static bool needOpenSceneAnimation = false;
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

        void Awake()
        {
            SingletoneCheck(this);
            space = GetComponentInChildren<Image>();
            space.sprite = spaceSprite;
            loadingImageMaterial.SetFloat("InPortal", 0f);
            loadingImageMaterial.SetFloat("Diameter", endPortalPosition);
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
                    OpenScene();
                    SceneManager.SetAllowSceneActivation(true);
                }
            }
            else if (OpenSceneFlag)
            {
                if (currentPlayTime == 0)
                    SceneManager.CheckingTheScene();
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

        public void LoadScene()
        {
            SetSceneImage();
            CloseScene();
        }

        public void SetSceneImage()
        {
            int SpriteIndex = Random.Range(0, pullOfSpaces.Length);
            if (SpriteIndex > 0)
            {
                spaceSprite = pullOfSpaces[SpriteIndex];
                space.sprite = spaceSprite;
            }
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
}

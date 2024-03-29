using System;
using UnityEngine;

namespace TacticalPanicCode
{
    public enum WorldMarks { options, map, briefing, home, magic, squad, blacksmith, social }

    public class WorldMenuManager : SinglBehaviour<WorldMenuManager>, IScenePanel
    {

        [SerializeField]
        private GameObject levels;
        [SerializeField]
        private GameObject squad;
        [SerializeField]
        private GameObject magic;
        [SerializeField]
        private GameObject blacksmith;
        [SerializeField]
        private GameObject briefing;
        [SerializeField]
        private GameObject home;
        [SerializeField]
        private GameObject social;

        private SquadCanvasOperator squadCanvasOperator;
        private BriefingCanvasOperator briefingCanvasOperator;
        private BriefingMapOperator briefingMapOperator;

        public void SetAllInstance()
        {
            if (instance == null)
                SingletoneCheck(this);
            SceneManager.ScenePanel = this;

            squadCanvasOperator = squad.GetComponent<SquadCanvasOperator>();
            //squadCanvasOperator is disabled. Awake & Start procedures are not suitable
            squadCanvasOperator.SingletoneCheck(squadCanvasOperator);//Singltone
            squadCanvasOperator.SetParentToAllUnits();

            briefingCanvasOperator = briefing.GetComponent<BriefingCanvasOperator>();
            briefingCanvasOperator.SingletoneCheck(briefingCanvasOperator);//Singltone

            briefingMapOperator = briefing.GetComponentInChildren<BriefingMapOperator>();
            briefingMapOperator.SingletoneCheck(briefingMapOperator);//Singltone
        }

        public void SwitchPanels(WorldMarks mark)
        {
            DiactiveAllPanels();
            switch (mark)
            {
                case WorldMarks.squad:
                    squad.SetActive(true);
                    break;
                case WorldMarks.home:
                    home.SetActive(true);
                    break;
                case WorldMarks.magic:
                    magic.SetActive(true);
                    break;
                case WorldMarks.options:
                    SceneManager.SwitchPanels(SceneDirection.options);
                    break;
                case WorldMarks.blacksmith:
                    blacksmith.SetActive(true);
                    break;
                case WorldMarks.map:
                    levels.SetActive(true);
                    break;
                case WorldMarks.briefing:
                    briefing.SetActive(true);
                    break;
                case WorldMarks.social:
                    social.SetActive(true);
                    break;
                default:
                    new Exception("Unrealized bookmark!");
                    break;
            }
        }

        public void SwitchPanels(int mark)
        {
            SwitchPanels((WorldMarks)mark);
        }

        public void DiactiveAllPanels()
        {
            squad.SetActive(false);
            levels.SetActive(false);
            briefing.SetActive(false);
            magic.SetActive(false);
            blacksmith.SetActive(false);
            home.SetActive(false);
            social.SetActive(false);
            SceneManager.DiactiveAllPanels();
        }

        public void BasicPanelSettings()
        {
            SwitchPanels(WorldMarks.map);
        }
    }
}

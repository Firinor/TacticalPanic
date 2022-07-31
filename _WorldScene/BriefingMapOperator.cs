using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

namespace TacticalPanicCode
{
    public class BriefingMapOperator : SinglBehaviour<BriefingMapOperator>
    {
        [SerializeField]
        private Text headerText;
        [SerializeField]
        private Text descriptionText;
        [SerializeField]
        private GameObject GridOfMap;

        private List<GameObject> spriteTiles = new List<GameObject>();

        internal void SetLevelInfo(LevelInformator level)
        {
            headerText.text = level.name;
            descriptionText.text = level.DescriptionText;
            GenerateMiniMap(level.GetMap());
        }

        private void GenerateMiniMap(List<List<int>> intMap)
        {
            if (spriteTiles.Count > 0)
            {
                foreach (GameObject sprite in spriteTiles)
                {
                    Destroy(sprite);
                }
                spriteTiles.Clear();
            }

            for (int x = 0; x < intMap.Count; x++)
            {
                for (int y = 0; y < intMap[x].Count; y++)
                {
                    GameObject newSprite = Instantiate(TileInformator.SpriteTiles[intMap[x][y]], GridOfMap.transform);
                    spriteTiles.Add(newSprite);
                }
            }
        }
    }
}

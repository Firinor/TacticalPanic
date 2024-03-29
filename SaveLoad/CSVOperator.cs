using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace TacticalPanicCode
{
    public class CSVOperator : MonoBehaviour
    {
        private static string pathToFile = "/Units/_UnitData.csv";

        public static List<UnitBasis> GetUnits()
        {
            string path = Application.dataPath + pathToFile;

            string[] AllText = File.ReadAllLines(path);

            List<UnitBasis> UnitBasisList = new List<UnitBasis>();

            //string titleString = AllText[0].Replace("\"", "");
            //string[] titles = titleString.Split(';');

            for (int i = 1; i < AllText.Length; i++)
            {
                string unitString = AllText[i].Replace("\"", "");
                string[] elements = unitString.Split(';');

                if (elements.Length == 0)
                    continue;

                int id = Convert.ToInt32(elements[0]);
                string name = elements[1];
                float mspeed = Convert.ToSingle(elements[2]);
                int gistCount = Convert.ToInt32(elements[3]);
                Gist DeathGist = (Gist)Enum.Parse(typeof(GistColors), elements[4]);

                GistBasis[] bodyGists = new GistBasis[gistCount];

                int index = 4;
                for (int j = 0; j < gistCount; j++)
                {
                    index++;
                    GistBasis resultBodyGist = new GistBasis();
                    resultBodyGist.gist = (Gist)Enum.Parse(typeof(GistColors), elements[index]);
                    resultBodyGist.attack = Convert.ToInt32(elements[index + gistCount]);
                    resultBodyGist.reattack = Convert.ToInt32(elements[index + gistCount * 2]);
                    resultBodyGist.defense = Convert.ToInt32(elements[index + gistCount * 3]);
                    resultBodyGist.points = Convert.ToInt32(elements[index + gistCount * 4]);
                    resultBodyGist.regen = Convert.ToInt32(elements[index + gistCount * 5]);
                    resultBodyGist.manaPrice = Convert.ToInt32(elements[index + gistCount * 6]);
                    bodyGists[j] = resultBodyGist;
                }

                UnitBasisList.Add(new UnitBasis(id, name, mspeed, DeathGist, bodyGists));
            }

            return UnitBasisList;
        }
    }
}

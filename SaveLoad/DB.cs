using System.Collections.Generic;
using UnityEngine;

namespace TacticalPanicCode
{
    public class DB : SinglBehaviour<DB>//Top-manager
    {
        [SerializeField]
        private List<UnitInformator> unitInformators;

        public static List<UnitBasis> Units { get; private set; }

        [SerializeField]
        public List<LevelInformator> Levels;

        void Awake()
        {
            SingletoneCheck(this);

            Units = CSVOperator.GetUnits();

            foreach (UnitBasis unit in Units)
            {
                UnitInformator unitInformator = GetUnitInformatorByName(unit.unitName);
                if (unitInformator == null)
                    continue;
                unit.unitInformator = unitInformator;
                unitInformator.unitBasis = unit;
            }
        }

        private UnitInformator GetUnitInformatorByName(string name)
        {
            return unitInformators.Find(x => x.Name == name);
        }

        public static UnitBasis GetUnitBasisByID(int id)
        {
            return Units.Find(x => x.id == id);
        }

        public static LevelInformator ReadLevel(int level)
        {
            //1 - first level
            //2 - second level
            PlayerManager.PickedLevel = instance.Levels[level - 1];
            return PlayerManager.PickedLevel;
        }
    }
}

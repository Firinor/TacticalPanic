using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TacticalPanicCode
{
    public class UnitsManager : SinglBehaviour<UnitsManager>
    {
        [SerializeField]
        private GameObject playerUnitsParent;
        public static GameObject PlayerUnitsParent { get { return instance.playerUnitsParent; } }
        [SerializeField]
        private GameObject enemyUnitsParent;
        public static GameObject EnemyUnitsParent { get { return instance.enemyUnitsParent; } }
        [SerializeField]
        private GameObject neutralsUnitsParent;
        public static GameObject NeutralsUnitsParent { get { return instance.neutralsUnitsParent; } }
    }
}

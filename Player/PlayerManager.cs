using System.Collections.Generic;

namespace TacticalPanicCode
{
    public static class PlayerManager
    {
        public static int Account { get; private set; }
        public static List<UnitBasis> Party { get; set; } = new List<UnitBasis>();
        public static LevelInformator PickedLevel { get; set; }

        public static void OnLoad()
        {
            Account = SaveManager.PlayerAccount();
            Party = GetUnitsByIDs(SaveManager.Data.Party);
        }

        private static List<UnitBasis> GetUnitsByIDs(int[] partyInInts)
        {
            List<UnitBasis> party = new List<UnitBasis>();
            for (int i = 0; i < partyInInts.Length; i++)
            {
                UnitBasis unit = DB.GetUnitBasisByID(partyInInts[i]);
                if (unit != null)
                    party.Add(unit);
            }

            return party;
        }

        public static void AddUnitToParty(UnitBasis unit)
        {
            if (unit != null && !Party.Contains(unit))
                Party.Add(unit);
        }

        public static void RemoveUnitToParty(UnitBasis unit)
        {
            Party.Remove(unit);
        }

        public static void ClearParty()
        {
            Party.Clear();
        }

        public static int[] GetPartyAsInts()
        {
            int[] result = new int[Party.Count];
            for (int i = 0; i < result.Length; i++)
                result[i] = Party[i].id;
            return result;
        }

        public static bool UnitInParty(UnitBasis unit)
        {
            return Party.Contains(unit);
        }
    }
}

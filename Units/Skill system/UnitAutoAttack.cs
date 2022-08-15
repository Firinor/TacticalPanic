namespace TacticalPanicCode
{
    public class UnitBasicAttack
    {
        private int cost;
        private int damage;
        private int range;
        private int targetCount;
        private TargetFilter targetFilter;

        //Attack states
        private int prepareWeight;
        private int effeckWeight;
        private int cooldownWeight;

        private int preparePart;
        private int effeckPart;
        private int cooldownPart;
    }
}

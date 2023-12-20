namespace Final_Project___Dungons_of_Equavar
{
    public class Enemy
    {
        float defense, magicDef;

        public float PhysicalDef { get { return defense; } }
        public float MagicDef { get { return magicDef; } }
        public int Weakness { get; private set; }
        public int Resist { get; private set; }
        public int Immune { get; private set; }
    }
}

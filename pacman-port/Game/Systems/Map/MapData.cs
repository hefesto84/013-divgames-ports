namespace pacman_port.Game.Systems.Map
{
    public struct MapData
    {
        public MapDataEntry[,] Data;
    }

    public struct MapDataEntry
    {
        public int X;
        public int Y;
        public int T;
    }
}
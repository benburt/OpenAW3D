namespace Assets.Scripts.Buildings
{
    [System.Serializable]
    public class BaseBuildingType
    {        
        public string Name { get; set; }
        public BuildingTypeEnum ID { get; set; }
        public int HealingPower { get; set; }
    }
}

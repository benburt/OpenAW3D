namespace Assets.Scripts.Buildings
{
    public class City : BaseBuildingType
    {
        public City()
        {
            Name = "City";
            ID = BuildingTypeEnum.CITY;
            HealingPower = 1;
        }
    }
}

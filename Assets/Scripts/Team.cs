using Assets.Scripts.Buildings;
using System.Collections.Generic;

public class Team
{
	public int TeamNo = -1;
    /// <summary>
    /// The amount of "Cash" the team has
    /// </summary>
	public int Resources = 8000;
    /// <summary>
    /// Units that belong to the team
    /// </summary>
	public List<Unit> Units = new List<Unit>();
    /// <summary>
    /// Buildings this team has captured
    /// </summary>
	public List<Building> Buildings = new List<Building>();
    /// <summary>
    /// How much each building brings in each round
    /// </summary>
	private const int INCOME_PER_BUILDING = 3000;

	public void GainIncome()
	{
		Resources += INCOME_PER_BUILDING * Buildings.Count;
	}

	public void ResetUnits()
	{
		for (int i = 0; i < Units.Count; i++)
		{
			Units[i].Reset();
		}
		for (int i = 0; i < Buildings.Count; i++)
		{
			Buildings[i].Reset();
		}
	}

	public void HealUnitsInCities()
	{
		for (int i = 0; i < Units.Count; i++)
		{
			if (Units[i].GetHitPoints() != 10 && Units[i].BuildingOn != null && Units[i].BuildingOn.Team == Units[i].Team)
			{
                Units[i].Heal();
			}
		}
	}
	public void HealUncontestedBuildings()
	{
		for (int i = 0; i < Buildings.Count; i++)
		{
			if (Buildings[i].HitPoints < Buildings[i].GetHitPointsMax() && (Buildings[i].UnitOnTop == null || Buildings[i].UnitOnTop.Team == TeamNo))
				Buildings[i].Heal(2);
		}
	}
}
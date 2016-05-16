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

    public string TeamColorName { get; set; }

    public Team(string TeamColor)
    {
        TeamColorName = TeamColor;
    }
    
	public void GainIncome()
	{
		Resources += Game.INCOME_PER_BUILDING * Buildings.Count;
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

    /// <summary>
    /// "Heals" buildings are not being captured
    /// </summary>
	public void HealUncontestedBuildings()
	{
		for (int i = 0; i < Buildings.Count; i++)
		{
			if (Buildings[i].HitPoints < Buildings[i].GetHitPointsMax() && (Buildings[i].UnitOnTop == null || Buildings[i].UnitOnTop.Team == TeamNo))
				Buildings[i].Heal(2);
		}
	}
}
using Assets.Scripts.Buildings;
using UnityEngine;

public class Building : MonoBehaviour
{
    protected Game Game;
	private bool Selected = false;
	public Unit UnitOnTop;
	
	private const int HitPointsDefault = 20;
	public int HitPoints { get; set; }

	private Color AlphaOffset = new Color(0, 0, 0, 0);

	public int Team = 0;

    // Temp. TODO: Create Unity Editor for this.
    [HideInInspector]
    public BaseBuildingType BuildingType = new BaseBuildingType();
    public int BuildingTypeID = 0;

	void Start ()
	{
		Game = GameObject.Find("Game").GetComponent<Game>();

        switch(BuildingTypeID)
        {
            case 1: BuildingType = new City(); break;
            case 2: BuildingType = new HQ(); break;
            case 3: BuildingType = new Base(); break;
        }
        // Set the HitPoints to the default
        HitPoints = HitPointsDefault;


        SetTeam(Team, true);
	}
	public void Reset()
	{
		Selected = false;
		UnitOnTop = null;
	}

	void OnMouseDown()
	{
		if (Game.HUD.ActionPopup.Visible)
			return;
		
		// On Left-MouseDown
		if (Game.Selector.CurrentUnit != null && (UnitOnTop == null || UnitOnTop == Game.Selector.CurrentUnit) && !Game.Selector.CurrentUnit.IsWaitingForMoveAccept())
			Game.Selector.CurrentUnit.MoveToTile(TilePosition());
		else if (!Selected)
			Game.Selector.SelectBuilding(this);
	}
	void OnMouseEnter()
	{
		if (Game.HUD.ActionPopup.Visible)
			return;
		
		Game.Selector.SelectTile(TilePosition());

		Game.HUD.SetTileInfo(BuildingType.Name, Team, HitPoints);
	}

	public void Select()
	{
		Selected = true;

		if (BuildingType.ID == BuildingTypeEnum.BASE)
		{
			Game.HUD.BuyMenu.SetBuilding(this);
			Game.HUD.BuyMenu.Show(false, transform.position);
		}
	}
	public void Unselect()
	{
		Selected = false;

		if (BuildingType.ID == BuildingTypeEnum.BASE)
			Game.HUD.BuyMenu.Hide();
	}

	public void OnUnitEnter(Unit unit)
	{
		UnitOnTop = unit;
		unit.BuildingOn = this;

		AlphaOffset = new Color(0, 0, 0, 0.2f);
		SetTeam();

		gameObject.GetComponent<Collider>().enabled = false;
	}
	public void OnUnitLeave()
	{
		if (UnitOnTop != null)
			UnitOnTop.BuildingOn = null;
		UnitOnTop = null;

		AlphaOffset = new Color(0, 0, 0, 0);
		SetTeam();

		gameObject.GetComponent<Collider>().enabled = true;
	}

	public void Capture(int hitPoints, int team)
	{
		if (HitPoints - hitPoints <= 0)
		{
			int previousTeam = Team;

			SetTeam(team);
			HitPoints = GetHitPointsMax();

			if (previousTeam != 0)
				Game.CheckWinLoseConditions();
		}
		else
			HitPoints -= hitPoints;
	}
	public void Heal(int hitPoints)
	{
		if (HitPoints + hitPoints > GetHitPointsMax())
			HitPoints = GetHitPointsMax();
		else
			HitPoints += hitPoints;
	}

	public int GetHitPointsMax() { return HitPointsDefault; }

	public void SetTeam(int team = -1, bool init = false)
	{
		if (team == -1)
			team = Team;

		if (Team != team || init)
		{
			if (Team != 0 && Game.Teams[Team - 1].Buildings.IndexOf(this) != -1)
				Game.Teams[Team - 1].Buildings.Remove(this);
			
			Team = team;
			
			if (Team != 0 && Game.Teams[Team - 1].Buildings.IndexOf(this) == -1)
				Game.Teams[Team - 1].Buildings.Add(this);
		}

		// Set Colour
		GetComponentInChildren<TeamColour>().SetTeam(team, AlphaOffset);

//		switch (Team)
//		{
//			case 1:
//			{
//				for (int i = 0; i < transform.childCount; i++)
//					transform.GetChild(i).renderer.material.SetColor("_Color", Color.red - AlphaOffset);
//				if (renderer != null)
//					renderer.material.SetColor("_Color", Color.red);
//				break;
//			}
//			case 2:
//			{
//				for (int i = 0; i < transform.childCount; i++)
//					transform.GetChild(i).renderer.material.SetColor("_Color", Color.blue - AlphaOffset);
//				if (renderer != null)
//					renderer.material.SetColor("_Color", Color.blue);
//				break;
//			}
//			default:
//			{
//				for (int i = 0; i < transform.childCount; i++)
//					transform.GetChild(i).renderer.material.SetColor("_Color", Color.white - AlphaOffset);
//				if (renderer != null)
//					renderer.material.SetColor("_Color", Color.white);
//				break;
//			}
//		}
	}

	public Point TilePosition() { return new Point(Mathf.RoundToInt(this.gameObject.transform.position.x), Mathf.RoundToInt(this.gameObject.transform.position.z)); }
}

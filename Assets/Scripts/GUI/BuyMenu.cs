using Assets.Scripts.GUI;
using System.Collections.Generic;
using UnityEngine;

public class BuyMenu : Menu
{
    public Texture2D Icon_Tank;

	private Building Building;

	private Rect Price_Rect;
	private GUIStyle Price_Style;

    string CurrentTeamColor;

    /// <summary>
    /// List of Items for sale
    /// </summary>
    protected new List<BuyMenuItem> Items { get; set; }
    
	protected override void Init()
	{
		base.Init();

		BoxWidth = 200;
        
		Price_Style = new GUIStyle(ButtonStyle);
		Price_Style.alignment = TextAnchor.UpperRight;
		Price_Style.contentOffset = new Vector2(-2, 0);
	}

    protected override void OnGUI()
    {
        if (!Visible)
            return;

        // Back
        GUI.Box(Rect, GUIContent.none, Style);

        // Red/Blue Strips
        GUI.Box(TopStripe_Rect, GUIContent.none, TopStripe_Style);
        GUI.Box(BottomStripe_Rect, GUIContent.none, BottomStripe_Style);

        // Buttons
        Button_Rect.y = Rect.y + 4;
        for (int i = 0; i < Items.Count; i++)
        {
            DrawButton(i);
        }
    }

    public void SetBuilding(Building building)
	{
		Building = building;
        
        CurrentTeamColor = Game.GetCurrentTeam().TeamColorName;

        // Load the TankIcon for the current Team
        Icon_Tank = Resources.Load(CurrentTeamColor + "_tank") as Texture2D;
        
        if (Items == null || Items.Count == 0)
        {
            // Only build the Items list once.
            BuyMenuItem bmi = new BuyMenuItem("Tank", 6000, "Tank");
            AddItem(bmi);
            bmi = new BuyMenuItem("Mega Tank", 8000, "Tank");
            AddItem(bmi);
        }

    }

    public override void Show(bool middleOfScreen, Vector3 position, int? ItemCount = null)
	{
		for (int i = 0; i < Items.Count; i++)
		{
            if (Game.GetCurrentTeam().Resources < Items[i].Price)
			{
				ButtonStyle.normal.textColor = Color.grey;
				IconColors[i] = new Color(1, 1, 1, 0.4f);
			}
			else
			{
				ButtonStyle.normal.textColor = Color.black;
				IconColors[i] = default(Color);
			}
		}
		Price_Style.normal.textColor = ButtonStyle.normal.textColor;

		base.Show(middleOfScreen, position, Items.Count);

		Price_Rect = new Rect();
		Price_Rect.height = ButtonHeight;
		Price_Rect.x = Rect.x + Rect.width - 2;
	}

    protected override void DrawButtonIcon(int i)
    {
        if (IconColors[i] != default(Color))
            GUI.color = IconColors[i];

        Texture2D ButtonIcon = Resources.Load<Texture2D>(CurrentTeamColor + Items[i].IconName);
        
        GUI.DrawTexture(new Rect(Rect.x + 4, Button_Rect.y, IconSize, IconSize), ButtonIcon);

        if (IconColors[i] != default(Color))
            GUI.color = new Color(1, 1, 1, 1);
    }

    protected override void DrawButton(int i)
	{
		Price_Rect.y = Button_Rect.y + 5;
		GUI.TextArea(Price_Rect, Items[i].Price.ToString(), Price_Style);
        DrawButtonIcon(i);

        // button
        bool clicked = GUI.Button(Button_Rect, new GUIContent(Items[i].Name), ButtonStyle);

        // click event
        if (clicked)
            OnButtonPress(Items[i]);

        Button_Rect.y += ButtonHeight;
    }

    /// <summary>
    /// Adds a new Item to the list of possible purchases
    /// </summary>
    /// <param name="NewItem">The item to add</param>
    private void AddItem(BuyMenuItem item)
    {
        if (this.Items == null)
            this.Items = new List<BuyMenuItem>();

        this.Items.Add(item);
        IconColors.Add(default(Color));
    }

    /// <summary>
    /// Fired when an item is "bought"
    /// </summary>
    /// <param name="item">The selected item</param>
    protected void OnButtonPress(BuyMenuItem item)
	{
		if (Building == null)
			return;
        
        // Make sure team has enough cash.
        if(Game.GetCurrentTeam().Resources >= item.Price)
        {
            // Update team cash.
            Game.GetCurrentTeam().Resources -= item.Price;
            Game.HUD.SetResources(Game.GetCurrentTeam().Resources);
            
            Transform unitObject = Instantiate(Resources.Load<Transform>("prefabs/tank"), Building.transform.position, Quaternion.identity) as Transform;
            unitObject.parent = GameObject.Find("Units").transform;
            Unit unit = unitObject.GetComponent<Unit>();
            unit.Init();
            unit.SetTeam(Building.Team);
            unit.AcceptMove();
        }

		Game.Selector.UnselectCurrentBuilding();

		Hide();
	}
}
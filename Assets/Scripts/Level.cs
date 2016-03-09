using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts;

public class Level : MonoBehaviour
{
	public TileMap Tiles;
    public Rect Bounds = new Rect();

	// Use this for initialization
	void Start()
	{
		// Setup Tile Selection
		Transform TileObjects = this.gameObject.transform.FindChild("Tiles");

        GetMapDimensions(TileObjects);
        // 18 ROWS in SCENE 01
        // 17 COLUMNS in SCENE 01 (0-16)... -1 to 15... Problem is "16" isn't there...

        Tiles = new TileMap();

        // Z = ROW
        for(int z = (int) Bounds.y; z < Bounds.height + 1; z++)
        {
            Tiles.AddRow(new TileRow(z));
            // X = COLUMN
            for(int x = (int)Bounds.x; x < Bounds.width + 1; x++)
            {
                Tiles[z].SetTile(x, null);
            }
        }

		// Add Tiles into Array
		for (int i = 0; i < TileObjects.childCount; i++)
		{
			if (TileObjects.GetChild(i).gameObject.GetComponent<Tile>() == null)
				continue;

			Vector3 pos = TileObjects.GetChild(i).gameObject.transform.position;

            if (Tiles[Mathf.RoundToInt(pos.z)][Mathf.RoundToInt(pos.x)] != null && TileObjects.GetChild(i).gameObject.GetComponent<Tile>().Type != Tile.BRIDGE)
                continue;

            Tiles[Mathf.RoundToInt(pos.z)].SetTile(Mathf.RoundToInt(pos.x), TileObjects.GetChild(i).gameObject.GetComponent<Tile>());

        }

		// Let Tiles know about buildings that are on top of them
		Transform Buildings = this.gameObject.transform.FindChild("Buildings");
		for (int i = 0; i < Buildings.childCount; i++)
		{
			Building building = Buildings.GetChild(i).GetComponent<Building>();
			GetTile(building.TilePosition()).BuildingOnTop = building;
		}
	}

    void GetMapDimensions(Transform TileObjects)
    {
        for (int i = 0; i < TileObjects.childCount; i++)
        {
            Vector3 pos = TileObjects.GetChild(i).gameObject.transform.position;

            if (pos.x < Bounds.x)
                Bounds.x = pos.x;
            else if (pos.x > Bounds.width)
                Bounds.width = pos.x;

            if (pos.z < Bounds.y)
                Bounds.y = pos.z;
            else if (pos.z > Bounds.height)
                Bounds.height = pos.z;
        }
    }

	// Update is called once per frame
	void Update()
	{
	
	}


	public Tile GetTile(Point tilePosition)
    {
        TileRow tr = Tiles[tilePosition.y];
        try
        {
            return tr[tilePosition.x];
        } catch(System.Exception)
        {
            Debug.Log("X: " + tilePosition.x + "Y: " + tilePosition.y);
        }

        //return Tiles[tilePosition.y][tilePosition.x];
        return null;
    }

	public Tile GetTile(int x, int y) { return Tiles[y][x]; }
	
	public bool ValidTile(int x, int y) { return x >= 0 && y >= 0 && x <= Bounds.width && y <= Bounds.height; }
	public bool ValidTile(Point tilePosition) { return ValidTile(tilePosition.x, tilePosition.y); }


	public IEnumerable<Point> AllTilePositions()
	{
        foreach(TileRow tr in Tiles.TileRows)
        {
            foreach(Tile t in tr.Columns.Values)
            {
                yield return t.TilePosition();
            }
        }
	} 
}

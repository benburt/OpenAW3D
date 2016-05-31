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

        GetMapDimensions(TileObjects.FindChild("GrassTiles"));
        GetMapDimensions(TileObjects.FindChild("RoadTiles"));
        GetMapDimensions(TileObjects.FindChild("WaterTiles"));
        GetMapDimensions(TileObjects.FindChild("RampTiles"));

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
        BuildTileMap(TileObjects.FindChild("GrassTiles"));
        BuildTileMap(TileObjects.FindChild("RoadTiles"));
        BuildTileMap(TileObjects.FindChild("WaterTiles"));
        BuildTileMap(TileObjects.FindChild("RampTiles"));

        // Let Tiles know about buildings that are on top of them
        // Make tiles aware of bases on top
        SetBuildingsOnTiles(this.gameObject.transform.FindChild("Buildings").FindChild("Bases"));
        // Make tiles aware of cities on top
        SetBuildingsOnTiles(this.gameObject.transform.FindChild("Buildings").FindChild("Cities"));
    }

    private void BuildTileMap(Transform TileObjects)
    {
        for (int i = 0; i < TileObjects.childCount; i++)
        {
            if (TileObjects.GetChild(i).gameObject.GetComponent<Tile>() == null)
                continue;

            Vector3 pos = TileObjects.GetChild(i).gameObject.transform.position;

            // What is this doing with bridge?
            if (Tiles[Mathf.RoundToInt(pos.z)][Mathf.RoundToInt(pos.x)] != null && TileObjects.GetChild(i).gameObject.GetComponent<Tile>().Type != 1)
                continue;

            Tiles[Mathf.RoundToInt(pos.z)].SetTile(Mathf.RoundToInt(pos.x), TileObjects.GetChild(i).gameObject.GetComponent<Tile>());
        }
    }

    private void SetBuildingsOnTiles(Transform Buildings)
    {
        for (int i = 0; i < Buildings.childCount; i++)
        {
            Building building = Buildings.GetChild(i).GetComponent<Building>();
            GetTile(building.TilePosition()).BuildingOnTop = building;
        }
    }

    /// <summary>
    /// Dynamically works out the maximum width and height of the map
    /// </summary>
    /// <param name="TileObjects"></param>
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

    /// <summary>
    /// Gets the tile at tilePosition
    /// </summary>
    /// <param name="tilePosition">The point of the tile we are interested in</param>
    /// <returns>The tile at tilePosition if it exists, else null</returns>
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

        return null;
    }

	public Tile GetTile(int x, int y)
    {
        return Tiles[y][x];
    }

    /// <summary>
    /// Checks to see if the tile at given co-ordinates is a valid tile
    /// </summary>
    /// <param name="x">The X location of the tile</param>
    /// <param name="y">The Y location of the tile</param>
    /// <returns>True if tile is valid. False if tile is invalid</returns>
    public bool ValidTile(int x, int y)
    {
        // Why not just check tilerow and column is not null? 
        // Wouldn't that be more flexible?
        TileRow tr = Tiles[y];
        if (tr == null)
            return false;
        else if (tr[x] == null)
            return false;
        else
            return true;
    }

	public bool ValidTile(Point tilePosition)
    {
        return ValidTile(tilePosition.x, tilePosition.y);
    }
    
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

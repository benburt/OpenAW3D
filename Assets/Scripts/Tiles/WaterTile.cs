using UnityEngine;

namespace Assets.Scripts.Tiles
{
    public class WaterTile : TileType
    {
        public WaterTile()
        {
            IsPassable = false;
            TileTypeName = "Sea";
            TileInfoIcon_Scale = 1;
        }
        
        public void OnEnable()
        {
            TileInfoIcon_Texture = Resources.Load<Texture2D>("Textures/water");
        }
    }
}

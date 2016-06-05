using UnityEngine;

namespace Assets.Scripts.Tiles
{
    public class RoadTile : TileType
    {
        public RoadTile()
        {
            IsPassable = true;
            TileTypeName = "Road";
            TileInfoIcon_Scale = 1;
        }

        public void OnEnable()
        {
            TileInfoIcon_Texture = Resources.Load<Texture2D>("Textures/road");
        }
    }
}

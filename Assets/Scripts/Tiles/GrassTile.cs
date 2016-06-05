using UnityEngine;

namespace Assets.Scripts.Tiles
{
    public class GrassTile : TileType
    {
        public GrassTile()
        {
            IsPassable = true;
            TileTypeName = "Plain";
            TileInfoIcon_Scale = 1;
        }

        public void OnEnable()
        {
            TileInfoIcon_Texture = Resources.Load<Texture2D>("Textures/grass");
        }
    }
}

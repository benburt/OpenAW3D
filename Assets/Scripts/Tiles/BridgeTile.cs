using UnityEngine;

namespace Assets.Scripts.Tiles
{
    public class BridgeTile : TileType
    {
        public BridgeTile()
        {
            IsPassable = true;
            TileTypeName = "Bridge";
            TileInfoIcon_Scale = 2;
        }
        
        public void OnEnable()
        {
            TileInfoIcon_Texture = Resources.Load<Texture2D>("Textures/Icons/buildings/bridge");
        }
    }
}

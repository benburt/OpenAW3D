using UnityEngine;

namespace Assets.Scripts.Tiles
{
    public abstract class TileType : ScriptableObject
    {
        /// <summary>
        /// Defines whether units can walk on this tile
        /// </summary>
        public bool IsPassable { get; set; }

        public string TileTypeName { get; set; }

        public Texture2D TileInfoIcon_Texture { get; set; } 

        public int TileInfoIcon_Scale { get; set; }

        public TileType()
        {

        }
    }
}
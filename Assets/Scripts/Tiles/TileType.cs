﻿namespace Assets.Scripts.Tiles
{
    public abstract class TileType : UnityEngine.ScriptableObject
    {
        /// <summary>
        /// Defines whether units can walk on this tile
        /// </summary>
        public bool IsPassable { get; set; }

        public string TileTypeName { get; set; }

        public TileType()
        {

        }
    }
}
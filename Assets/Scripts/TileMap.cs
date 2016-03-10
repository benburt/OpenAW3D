using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class TileMap
    {
        public List<TileRow> TileRows;

        public TileRow this[int index]
        {
            get
            {
                if (index < TileRows.Count)
                    return TileRows.Where(i => i.Index == index).FirstOrDefault();
                else
                    return null;
            }
        }

        public int Count
        {
            get
            {
                return TileRows.Count;
            }
            
        }

        public TileMap()
        {
            TileRows = new List<TileRow>();
        }

        public void AddRow(TileRow row)
        {
            TileRows.Add(row);
        }

    }
}

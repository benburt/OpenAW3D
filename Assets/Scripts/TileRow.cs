using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class TileRow
    {
        public int Index { get; set; }
        public Dictionary<int, Tile> Columns { get; set; }

        public int Count
        {
            get
            {
                return Columns.Count;
            }
        }

        public Tile this[int index]
        {
            get
            {
                if (index < Columns.Count)
                {
                    if (Columns.ContainsKey(index))
                        return Columns[index];
                    else
                        return null;
                }
                else
                    throw new IndexOutOfRangeException();
            }
        }

        public TileRow(int Index)
        {
            this.Index = Index;
            this.Columns = new Dictionary<int, Tile>();
        }

        public void SetTile(int xLoc, Tile tile)
        {
            Columns[xLoc] = tile;
        }

        public Tile GetTile(int column)
        {
            return Columns[column];
        }
    }
}

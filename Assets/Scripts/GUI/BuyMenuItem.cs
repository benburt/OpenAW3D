using UnityEngine;

namespace Assets.Scripts.GUI
{
    public class BuyMenuItem
    {
        /// <summary>
        /// Item display text
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// How many resources this item costs
        /// </summary>
        public int Price { get; set; }
        
        public Texture2D Icon { get; set; }

        public BuyMenuItem(string Name, int Price, Texture2D Icon)
        {
            this.Name = Name;
            this.Price = Price;
            this.Icon = Icon;
        }


    }
}

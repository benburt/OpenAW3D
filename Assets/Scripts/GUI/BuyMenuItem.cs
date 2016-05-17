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
        
        public string IconName { get; set; }

        public BuyMenuItem(string Name, int Price, string IconName)
        {
            this.Name = Name;
            this.Price = Price;
            this.IconName = "_" + IconName;
        }


    }
}

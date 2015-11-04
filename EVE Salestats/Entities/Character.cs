using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.UI.Xaml.Media.Imaging;

namespace EVE_SaleTools.Entities
{
    /// <summary>
    /// A pilot
    /// </summary>
    public class Character
    {
        private String name;
        /// <summary>
        /// Name of the pilot
        /// </summary>
        public String Name
        {
            get { return name; }
            private set { name = value; }
        }

        private String charID;
        /// <summary>
        /// Charcater ID
        /// </summary>
        public String CharID
        {
            get { return charID; }
            private set { charID = value; }
        }

        private String corp;
        /// <summary>
        /// Corporation this pilot belongs to
        /// </summary>
        public String Corp
        {
            get { return corp; }
            private set { corp = value; }
        }

        private double ballance;
        /// <summary>
        /// Pilots current ballance
        /// </summary>
        public double Ballance
        {
            get { return ballance; }
            private set { ballance = value; }
        }

        private BitmapImage image;
        /// <summary>
        /// Pilot image 
        /// </summary>
        public BitmapImage Image
        {
            get { return image; }
            private set { image = value; }
        }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="name">Pilot name</param>
        /// <param name="image">Pilot image</param>
        /// <param name="charID">Character ID</param>
        /// <param name="corp">Corporation Name</param>
        /// <param name="balance">Wallet ballance</param>
        public Character(String name, BitmapImage image, String charID, String corp, double balance)
        {
            this.Name = name;
            this.Image = image;
            this.CharID = charID;
            this.Corp = corp;
            this.Ballance = balance;
        }
    }
}

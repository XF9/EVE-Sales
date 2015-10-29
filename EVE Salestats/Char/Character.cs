using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.UI.Xaml.Media.Imaging;

namespace EVE_Salestats.Char
{
    public class Character
    {
        private String name;
        public String Name
        {
            get { return name; }
            private set { name = value; }
        }

        private String charID;
        public String CharID
        {
            get { return charID; }
            private set { charID = value; }
        }

        private String corp;
        public String Corp
        {
            get { return corp; }
            private set { corp = value; }
        }

        private float ballance;
        public float Ballance
        {
            get { return ballance; }
            private set { ballance = value; }
        }

        private BitmapImage image;
        public BitmapImage Image
        {
            get { return image; }
            private set { image = value; }
        }


        public Character(String name, BitmapImage image, String charID, String corp, float balance)
        {
            this.Name = name;
            this.Image = image;
            this.CharID = charID;
            this.Corp = corp;
            this.Ballance = balance;
        }
    }
}

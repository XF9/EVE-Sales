using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private String imagePath;
        public String ImagePath
        {
            get { return imagePath; }
            private set { imagePath = value; }
        }


        public Character(String name, String charID, String corp, float balance)
        {
            this.Name = name;
            this.CharID = charID;
            this.Corp = corp;
            this.Ballance = balance;
        }
    }
}

using System;

using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Xml;
using System.Xml.Linq;
using System.Threading.Tasks;

using Windows.System;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using System.Runtime.InteropServices.WindowsRuntime;

using System.Linq;


namespace EVE_Salestats.Char
{
    class CharacterLoader
    {
        private static String apiBaseUrl = "https://api.eveonline.com/";

        /// <summary>
        /// Load character data
        /// <param name="apiKey">the api key</param>
        /// <param name="vCode">the verification code</param>
        /// <returns>all characters</returns>
        /// </summary>
        static async public Task<Character[]> LoadCharacters(string apiKey, string vCode)
        {
            // build request string
            String request = CharacterLoader.apiBaseUrl + "account/Characters.xml.aspx?keyID=" + apiKey + "&vCode=" + vCode;

            // load data as xml string
            HttpResponseMessage response = await new HttpClient().GetAsync(request);
            string xml_data = await response.Content.ReadAsStringAsync();

            // parse as xml and fetch data
            XDocument characterdata = XDocument.Parse(xml_data);
            var characters = from character in characterdata.Descendants("row")
                                select new
                                {
                                    Name = character.Attribute("name").Value,
                                    CharID = character.Attribute("characterID").Value,
                                    Corp = character.Attribute("corporationName").Value
                                };

            //load data for each character
            Character[] charlist = new Character[characters.Count()];
            int index = 0;

            foreach (var character in characters)
            {
                Character charcter = new Character(
                    character.Name, 
                    await CharacterLoader.FetchImage(character.CharID, 128), 
                    character.CharID, 
                    character.Corp, 
                    await CharacterLoader.FetchWalletData(apiKey, vCode, character.CharID));
                charlist[index++] = charcter;
            }

            return charlist;
        }

        /// <summary>
        /// Load wallet ballance for one character
        /// </summary>
        /// <param name="apiKey">the api key</param>
        /// <param name="vCode">the api verification code</param>
        /// <param name="charID">the character ID</param>
        /// <returns>the wallet ballance</returns>
        static async private System.Threading.Tasks.Task<double> FetchWalletData(string apiKey, string vCode, string charID)
        {
            // build request string
            String request = CharacterLoader.apiBaseUrl + "char/AccountBalance.xml.aspx";
            request += "?keyID=" + apiKey;
            request += "&vCode=" + vCode;
            request += "&characterID=" + charID;

            // load xml from request string
            HttpResponseMessage response = await new HttpClient().GetAsync(request);
            string xml_data = await response.Content.ReadAsStringAsync();

            // parse string as xml and fetch data
            XDocument characterdata = XDocument.Parse(xml_data);
            var balance_data = from character in characterdata.Descendants("row")
                               select new
                               {
                                   balance = character.Attribute("balance").Value,
                               };
            var f = balance_data.First();
            // return wallet ballance
            return double.Parse(balance_data.First().balance);
        }

        /// <summary>
        /// loads the character image as BitmapImage
        /// </summary>
        /// <param name="charID">the character ID</param>
        /// <param name="size">the size of the image - needs to be an exponent of two!</param>
        /// <returns>the character image</returns>
        static async private System.Threading.Tasks.Task<BitmapImage> FetchImage(string charID, int size)
        {
            // build request string and load image data
            string url = "https://image.eveonline.com/Character/" + charID + "_" + size.ToString() + ".jpg";
            byte[] contentBytes = await new HttpClient().GetByteArrayAsync(url);

            // stream image data and store in bitmap image
            InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream();
            await stream.WriteAsync(contentBytes.AsBuffer());
            stream.Seek(0);

            BitmapImage image = new BitmapImage();
            image.SetSource(stream);

            return image;
        }
    }
}

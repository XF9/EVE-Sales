namespace EVE_Salestats.Entities
{
    /// <summary>
    /// A short summary of the account information
    /// </summary>
    public class AccountInfo
    {
        /// <summary>
        /// the api key belonging to this account
        /// </summary>
        private string apiKey;
        /// <summary>
        /// the api key belonging to this account
        /// </summary>
        public string ApiKey
        {
            get { return apiKey; }
            private set { apiKey = value; }
        }

        /// <summary>
        /// the verification code for the given api key
        /// </summary>
        private string vCode;
        /// <summary>
        /// the verification code for the given api key
        /// </summary>
        public string VCode
        {
            get { return vCode; }
            private set { vCode = value; }
        }

        public AccountInfo(string apiKey, string vCode)
        {
            this.apiKey = apiKey;
            this.vCode = vCode;
        }
    }
}
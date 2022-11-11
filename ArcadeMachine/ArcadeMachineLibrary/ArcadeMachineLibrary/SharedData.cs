namespace ArcadeMachineLibrary
{
    [Serializable]
    public class SharedData
    {
        private String key { get; } // represents the title of the game 
        private String value { get; } // represents the path to the games .exe file

        public SharedData(String key, String value)
        {
            this.key = key; 
            this.value = value;
        }
        public SharedData()
        {

        }
        public String getKey()
        {
            if (this.key != null)
            {
                return this.key;
            }
            else return null;
        }
        public String getValue()
        {
            if (this.value != null)
            {
                return this.value;
            }
            else return null;
        }
    }
}
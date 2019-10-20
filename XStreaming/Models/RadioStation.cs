namespace XStreaming.Models
{
    public class RadioStation
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string TextFirst
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Name))
                {
                    char FirstChar = Name.Trim()[0];
                    return FirstChar.ToString().ToUpper();
                }
                else
                {
                    return "X";
                }
            }
        }
    }
}

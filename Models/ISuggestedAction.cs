namespace MyTeamsApp2.Models
{
    public interface ISuggestedAction
    {
        public string Message { get; set; }
        public string[] Actions { get; set; }
    }
}

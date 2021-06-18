namespace Util.Model
{
    public class EChart
    {
        public Title title { get; set; }

        public Tooltip tooltip { get; set; }
    }

    public class Title
    {
        public string text { get; set; }

        public string subtext { get; set; }
    }

    public class Tooltip
    {
        public string trigger { get; set; }
    }
}
using System.Xml.Serialization;

//Clase usada para definir como será el XML
[XmlRoot("Level")]
public class structureXML
{
    [XmlElement("Time")]
    public Time time;
    public class Time
    {
        [XmlAttribute("TimeLevel")]
        public int seconds;
        [XmlAttribute("AlarmTime")]
        public int alarm;
    }

    [XmlElement("Prefabs")]
    public Prefabs prefabs;
    public class Prefabs
    {
        [XmlElement("Item")]
		public Item[] items;
    }

    public class Item
    {
        [XmlAttribute("prefab")]
        public string prefab;

        [XmlAttribute("x")]
        public string x;

        [XmlAttribute("y")]
        public string y;

        [XmlAttribute("rotZ")]
        public string rotZ;

        [XmlAttribute("scaleX")]
        public string scaleX;

        [XmlAttribute("scaleY")]
        public string scaleY;
    }
}

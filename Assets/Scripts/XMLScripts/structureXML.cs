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
    }

    [XmlElement("SpawnPointPlayer")]
    public SpawnPoint spawnPoint;
    public class SpawnPoint
    {
        [XmlAttribute("x")]
        public float x;
        [XmlAttribute("y")]
        public float y;
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
        public float x;

        [XmlAttribute("y")]
        public float y;

        [XmlAttribute("rotZ")]
        public float rotZ;

        [XmlAttribute("scaleX")]
        public float scaleX;

        [XmlAttribute("scaleY")]
        public float scaleY;
    }
}

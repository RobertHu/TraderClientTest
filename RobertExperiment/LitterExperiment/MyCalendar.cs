using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Drawing;
using System.Xml.Schema;
using System.Xml;

namespace LitterExperiment
{
    public class MyCalendar : IXmlSerializable
    {
        private string _name;
        private bool _enabled;
        private Color _color;
        private List<MyEvent> _events = new List<MyEvent>();

        public void Initialize(string name,bool enabled,Color color,List<MyEvent> events)
        {
            this._name = name;
            this._enabled = enabled;
            this._color = color;
            this._events = events;
        }



        public XmlSchema GetSchema() { return null; }

        public void ReadXml(XmlReader reader)
        {
            if (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == "MyCalendar")
            {
                _name = reader["Name"];
                _enabled = Boolean.Parse(reader["Enabled"]);
                _color = Color.FromArgb(Int32.Parse(reader["Color"]));

                if (reader.ReadToDescendant("MyEvent"))
                {
                    while (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == "MyEvent")
                    {
                        MyEvent evt = new MyEvent();
                        evt.ReadXml(reader);
                        _events.Add(evt);
                    }
                }
                reader.Read();
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("Name", _name);
            writer.WriteAttributeString("Enabled", _enabled.ToString());
            writer.WriteAttributeString("Color", _color.ToArgb().ToString());

            foreach (MyEvent evt in _events)
            {
                writer.WriteStartElement("MyEvent");
                evt.WriteXml(writer);
                writer.WriteEndElement();
            }
        }
    }

    public class MyEvent : IXmlSerializable
    {
        private string _title;
        private DateTime _start;
        private DateTime _stop;
        public void Initialize(string title,DateTime start,DateTime stop)
        {
            this._title = title;
            this._start = start;
            this._stop = stop;
        }

        public XmlSchema GetSchema() { return null; }

        public void ReadXml(XmlReader reader)
        {
            if (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == "MyEvent")
            {
                _title = reader["Title"];
                _start = DateTime.FromBinary(Int64.Parse(reader["Start"]));
                _stop = DateTime.FromBinary(Int64.Parse(reader["Stop"]));
                reader.Read();
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("Title", _title);
            writer.WriteAttributeString("Start", _start.ToBinary().ToString());
            writer.WriteAttributeString("Stop", _stop.ToBinary().ToString());
        }
    }
}

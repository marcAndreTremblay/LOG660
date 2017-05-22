using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using System.Xml;
using System.IO;
using System.Text;

namespace LOG660_InsertionProgram
{


    class Program
    {
        static void Main(string[] args)
        {
            OSQLConnection my_connection = new OSQLConnection();



            FileStream fs = File.Open("./clients_latin1.xml",FileMode.Open);
            XmlReader r = XmlReader.Create(fs);
            while (r.Read())
            {
                if (r.NodeType == XmlNodeType.Element)
                {
                    Console.WriteLine();
                    Console.WriteLine("<" + r.Name + ">");
                    if (r.HasAttributes)
                    {
                        for (int i = 0; i < r.AttributeCount; i++)
                        {
                            Console.WriteLine("\tATTRIBUTE: " + r.GetAttribute(i));
                        }
                    }
                }
                else if (r.NodeType == XmlNodeType.Text)
                {
                    Console.WriteLine("\tVALUE: " + r.Value);
                }
            }

            System.Console.ReadKey();

        }
    }
}

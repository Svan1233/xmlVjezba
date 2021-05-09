using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using System.Xml;

namespace xmlVjezba
{
    public class Klijent_class
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string EmailAdresa { get; set; }
    }

    public class Tester
    {
        private static XDocument CreateCustomerListXml()
        {
            List<Klijent_class> lista_klijenata = CreateCustomerList();

            var dokumentXmlKlijenti = new XDocument(new XElement("lista_klijenata",
                from Klijent_class in lista_klijenata
                select new XElement("Klijent",
                    new XAttribute("Ime", Klijent_class.Ime),
                    new XAttribute("Prezime", Klijent_class.Prezime),
                    new XElement("EmailAdresa", Klijent_class.EmailAdresa)
                    )));
            return dokumentXmlKlijenti;
        }

        private static List<Klijent_class> CreateCustomerList()
        {
            List<Klijent_class> lista_klijenata = new List<Klijent_class>
            {
                new Klijent_class
                {
                    Ime = "Konstantin",
                    Prezime = "Horvatin",
                    EmailAdresa = "konstantin.horvatin@email.com"
                },
                new Klijent_class
                {
                    Ime = "Marko",
                    Prezime = "Civić",
                    EmailAdresa = "marko.civic@email.com"
                },
                new Klijent_class
                {
                    Ime = "Josip",
                    Prezime = "Bratulić",
                    EmailAdresa = "josip.bratulic@email.com"
                },
                new Klijent_class
                {
                    Ime = "John",
                    Prezime = "Black",
                    EmailAdresa = "john.black@email.com"
                },
                new Klijent_class
                {
                    Ime = "Mladen",
                    Prezime = "Marković",
                    EmailAdresa = "mladen.markovic@email.com"
                },
                new Klijent_class
                {
                    Ime = "Josip",
                    Prezime = "Kukuljan",
                    EmailAdresa = "josip.kukuljan@email.com"
                },
                new Klijent_class
                {
                    Ime = "Ivan",
                    Prezime = "Crnković",
                    EmailAdresa = "ivan.crnkovic@email.com"
                },
                new Klijent_class
                {
                    Ime = "Bosko",
                    Prezime = "Karlautić",
                    EmailAdresa = "bosko.karlautic@email.com"
                },
                new Klijent_class
                {
                    Ime = "Luka",
                    Prezime = "Uremović",
                    EmailAdresa = "luka.uremovic@email.com"
                },
            };
            return lista_klijenata;
        }


        static void Main(string[] args)
        {
            string dir = @"C:/Documents/xmlDocument";
            if (Directory.Exists(dir) == false)
            {
                Directory.CreateDirectory(dir);
            }
            XDocument dokumentXmlKlijenti = CreateCustomerListXml();

            Console.WriteLine(dokumentXmlKlijenti.ToString());



            Console.WriteLine("\n\nPretraga za jednim elementom (Ime = Konstantin)...");

            var query =
                from Klijent_class in
                    dokumentXmlKlijenti.Element("lista_klijenata").Elements("Klijent")
                where Klijent_class.Attribute("Ime").Value == "Konstantin"
                select Klijent_class;
            XElement oneCustomer = query.SingleOrDefault();

            if (oneCustomer != null)
            {
                Console.WriteLine(oneCustomer);
            }
            else
            {
                Console.WriteLine("Ne postoji takav zapis.");
            }
            oneCustomer.Save("C:/Documents/xmlDocument/prvaPretraga.xml");


            Console.WriteLine("\nPretraga elemenata potomaka(Ime = Marko)...");

            query =
                from Klijent_class in
                    dokumentXmlKlijenti.Descendants("Klijent")
                where Klijent_class.Attribute("Ime").Value == "Marko"
                select Klijent_class;
            oneCustomer = query.SingleOrDefault();
            if (oneCustomer != null)
            {
                Console.WriteLine(oneCustomer);
            }
            else
            {
                Console.WriteLine("Ne postoji takav zapis.");
            }
            oneCustomer.Save("C:/Documents/xmlDocument/drugaPretraga.xml");


            Console.WriteLine("\nPretraga korištenjem vrijednosti elemenata(EmailAdresa = marko.civic@mail.com)...");
            query =
                from EmailAdresa in
                    dokumentXmlKlijenti.Descendants("EmailAdresa")
                where EmailAdresa.Value == "marko.civic@email.com"
                select EmailAdresa;

            XElement oneEmail = query.SingleOrDefault();
            if (oneEmail != null)
            {
                Console.WriteLine(oneEmail);
            }
            else
            {
                Console.WriteLine("Ne postoji takav zapis.");
            }
            oneCustomer.Save("D:/Documents/xmlDocument/trecaPretraga.xml");


            Console.WriteLine("\nPretraga korištenjem vrijednosti elemenata potomaka(EmailAdresa = marko.civic@mail.com)...");
            query =
                from Klijent_class in
                    dokumentXmlKlijenti.Descendants("Klijent")
                where Klijent_class.Element("EmailAdresa").Value == "marko.civic@email.com"
                select Klijent_class;

            oneCustomer = query.SingleOrDefault();
            if (oneEmail != null)
            {
                Console.WriteLine(oneCustomer);
            }
            else
            {
                Console.WriteLine("Ne postoji takav zapis.");
            }
            oneCustomer.Save("C:/Documents/xmlDocument/cetvrtaPretraga.xml");
            Console.Read();

        }
    }
}



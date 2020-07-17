using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Mobility.Passport {
    public class PassportManager {
        /* https://www.ad.nl/binnenland/ouders-kiezen-deze-babynamen-het-vaakst~a0bd398a/ */
        private List<Passport> passports { get; } = new List<Passport>() {
            // Zuid-Holland
            new Passport("Sara", "Dordrecht"),
            new Passport("Noah", "Rotterdam"),
            new Passport("James", "Den Haag"),
            // Utrecht
            new Passport("Finn", "Utrecht"),
            new Passport("Sophie", "Amersfoort"),
            // Noord-Brabant
            new Passport("Daan", "'s-Hertogenbosch"),
            new Passport("Tess", "Tilburg"),
            new Passport("Dex", "Breda"),
            new Passport("Fleur", "Eindhoven"),
            new Passport("Mila", "Bergen op Zoom"),
            // Noord-Holland
            new Passport("Emma", "Haarlem"),
            // Zeeland
            new Passport("Johanna", "Vlissingen"),
            // Gelderland
            new Passport("Levi", "Arnhem"),
            new Passport("Finn", "Apeldoorn"),
            new Passport("Lynn", "Nijmegen"),
            new Passport("Sophie", "Doetinchem"),
            // Limburg
            new Passport("Sem", "Maastricht"),
            new Passport("Fenna", "Venlo"),
            new Passport("Saar", "Heerlen"),
            // Friesland
            new Passport("Jesse", "Leeuwarden"),
            new Passport("Lieke", "Drachten"),
            // Groningen
            new Passport("Luuk", "Groningen"),
            new Passport("Jasmijn", "Eemshaven"),
            // Drenthe
            new Passport("Milan", "Assen"),
            // Overijssel
            new Passport("Bram", "Zwolle"),
            new Passport("Lauren", "Enschede"),
            // Flevoland
            new Passport("Liam", "Almere"),
            new Passport("Julia", "Lelystad")
        };

        public Passport currentPassport { get; private set; }

        public Passport GetNewPassport() {
            var index = Random.Range(0, passports.Count);
            currentPassport = passports.ElementAt(index);
            passports.RemoveAt(index);
            return currentPassport;
        }
    }
}

using BlaBlaRunProject.Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Spatial;

namespace BlaBlaRunProject.DataPopulator
{
    public static class WorkoutsOldInitializer
    {
        const int DataNumber = 500;
        public static List<WorkoutsOld> PopulateData()
        {
            Random rnd = new Random();
            List<WorkoutsOld> WorkoutsOldList = new List<WorkoutsOld>();
            for (int i = 0; i < DataNumber; i++)
            {
                WorkoutsOld oWorkoutsOld = new WorkoutsOld();
                oWorkoutsOld.StartDateTime = RandomDay(rnd);
                RandomLocationCity(oWorkoutsOld, rnd);
                //RandomLocation(oWorkoutsOld, rnd);
                oWorkoutsOld.AVGPace = RandomTime(rnd);
                oWorkoutsOld.Distance = RandomDistance(rnd);
                RandomCircular(oWorkoutsOld, rnd);
                oWorkoutsOld.MaxNumberPeople = RandomMaxNumberPeople(rnd);
                oWorkoutsOld.ElevationGain = RandomElevation(rnd);
                oWorkoutsOld.UsersId = 1;
                WorkoutsOldList.Add(oWorkoutsOld);
            }
                

            return WorkoutsOldList;
        }
        
        private static double? RandomDistance(Random rnd)
        {
            int MinMetersStart = 3000;
            int MaxMetersEnd = 15000;
            int kms = (MaxMetersEnd - MinMetersStart) / 1000;
            int r = rnd.Next(kms);

            return MinMetersStart + (r * 1000);
        }

        private static short? RandomMaxNumberPeople(Random rnd)
        {
            int r = rnd.Next(3);
            return (short) (r + 2);
        }

        private static double? RandomElevation(Random rnd)
        {
            int MaxMeters = 1000;
            int r = rnd.Next(MaxMeters);
            return r;
        }

        private static void RandomCircular(WorkoutsOld oWorkoutsOld, Random rnd)
        {
            bool result = rnd.Next(2) == 0;

            oWorkoutsOld.Circular = result;

            if (!oWorkoutsOld.Circular)
            {

                //oWorkoutsOld.EndLocation = RandomLocationEnd(oWorkoutsOld);
            }
        }

        private static DbGeography RandomLocationEnd(WorkoutsOld oWorkoutsOld)
        {
            throw new NotImplementedException();
        }

        private static TimeSpan? RandomTime(Random rnd)
        {
            TimeSpan start = TimeSpan.FromMinutes(3.5);
            TimeSpan end = TimeSpan.FromMinutes(7);

            int maxSeconds = (int)((end - start).TotalSeconds);

            int numPosible = ((int)Math.Round((double)maxSeconds / 10)) + 1 + 1;
            int random = rnd.Next(1, numPosible);
            
            int seconds = (random - 1) * 10;
            TimeSpan t = start.Add(TimeSpan.FromSeconds(seconds));
            return t;
        }

        private static void RandomLocationCity(WorkoutsOld oWorkoutsOld, Random rnd)
        {
            string[] citiesList = new string[] { "Madrid", "Madrid", "Madrid", "Madrid", "Madrid", "Barcelona", "Barcelona", "Barcelona", "Valencia", "Valencia", "Bilbao", "Coruña", "Sevilla", "Santader", "Badajoz", "León", "Murcia", "Albacete", "Toledo", "Zaragoza" };

           
            int r = rnd.Next(citiesList.Length);
            string city = (string)citiesList[r];

            string zone = string.Empty;
            if(city == "Madrid")
            {
                zone = RandomZoneMadrid(rnd);
            }
            else
            {
                if(city == "Barcelona")
                {
                    zone = RandomZoneBarcelona(rnd);
                }

            }

            oWorkoutsOld.Zone = zone.ToUpper();
            oWorkoutsOld.City = city;
            oWorkoutsOld.Region = city;
            oWorkoutsOld.Country = "Spain";

        }


        private static string RandomZoneMadrid(Random rnd)
        {
            string[] zoneList = new string[] { "ACACIAS",
            "ATOCHA",
            "CHOPERA",
            "DELICIAS",
            "IMPERIAL",
            "LEGAZPI",
            "PALOS DE MOGUER",
            "AEROPUERTO",
            "ALAMEDA DE OSUNA",
            "CASCO H.BARAJAS",
            "CORRALEJOS",
            "TIMON",
            "ABRANTES",
            "BUENAVISTA",
            "COMILLAS",
            "OPAÑEL",
            "PUERTA BONITA",
            "SAN ISIDRO",
            "VISTA ALEGRE",
            "CORTES",
            "EMBAJADORES",
            "JUSTICIA",
            "PALACIO",
            "SOL",
            "UNIVERSIDAD",
            "CASTILLA",
            "CIUDAD JARDIN",
            "EL VISO",
            "HISPANOAMERICA",
            "NUEVA ESPAÑA",
            "PROSPERIDAD",
            "ALMAGRO",
            "ARAPILES",
            "GAZTAMBIDE",
            "RIOS ROSAS",
            "TRAFALGAR",
            "VALLEHERMOSO",
            "ATALAYA",
            "COLINA",
            "CONCEPCION",
            "COSTILLARES",
            "PUEBLO NUEVO",
            "QUINTANA",
            "SAN JUAN BAUTISTA",
            "SAN PASCUAL",
            "VENTAS",
            "EL GOLOSO",
            "EL PARDO",
            "EL PILAR",
            "FUENTELARREINA",
            "LA PAZ",
            "MIRASIERRA",
            "PEÑA GRANDE",
            "VALVERDE",
            "APOSTOL SANTIAGO",
            "CANILLAS",
            "PALOMAS",
            "PINAR DEL REY",
            "PIOVERA",
            "VALDEFUENTES",
            "ALUCHE",
            "CAMPAMENTO",
            "CUATRO VIENTOS",
            "LAS AGUILAS",
            "LOS CARMENES",
            "LUCERO",
            "PUERTA DEL ANGEL",
            "ARAVACA",
            "ARGUELLES",
            "CASA DE CAMPO",
            "CIUDAD UNIVERSITARIAEL PLANTIO          ",
            "VALDEMARIN",
            "VALDEZARZA",
            "FONTARRON",
            "HORCAJO",
            "MARROQUINA",
            "MEDIA LEGUA",
            "PAVONES",
            "VINATEROS",
            "ENTREVIAS",
            "NUMANCIA",
            "PALOMERAS BAJAS",
            "PALOMERAS SURESTE",
            "PORTAZGO",
            "SAN DIEGO",
            "ADELFAS",
            "ESTRELLA",
            "IBIZA",
            "LOS JERONIMOS",
            "NIÑO JESUS",
            "PACIFICO",
            "CASTELLANA",
            "FUENTE DEL BERRO",
            "GOYA",
            "GUINDALERA",
            "LISTA",
            "RECOLETOS",
            "AMPOSTA",
            "ARCOS",
            "CANILLEJAS",
            "EL SALVADOR",
            "HELLIN",
            "REJAS",
            "ROSAS",
            "SIMANCAS",
            "ALMENARA",
            "BELLAS VISTAS",
            "BERRUGUETE",
            "CASTILLEJOS",
            "CUATRO CAMINOS",
            "VALDEACEDERAS",
            "ALMENDRALES",
            "MOSCARDO",
            "ORCASITAS",
            "ORCASUR",
            "PRADOLONGO",
            "SAN FERMIN",
            "ZOFIO",
            "AMBROZ",
            "CASCO H.VICALVARO",
            "CASCO H.VALLECAS",
            "SANTA EUGENIA",
            "BUTARQUE",
            "LOS ANGELES",
            "LOS ROSALES",
            "SAN ANDRES",
            "SAN CRISTOBAL"
             };


            int r = rnd.Next(zoneList.Length);
            string zone = (string)zoneList[r];
            return zone;
        }


        private static string RandomZoneBarcelona(Random rnd)
        {
            string[] zoneList = new string[] { "el Raval",
            "el Barri Gòtic",
            "la Barceloneta",
            "Sant Pere, Santa Caterina i la Ribera",
            "el Fort Pienc",
            "la Sagrada Família",
            "la Dreta de l Eixample",
            "l Antiga Esquerra de l Eixample",
            "la Nova Esquerra de l Eixample",
            "Sant Antoni",
            "el Poble Sec-AEI Parc Montjuïc",
            "la Marina del Prat Vermell-AEI Zona Franca",
            "la Marina de Port",
            "la Font de la Guatlla",
            "Hostafrancs",
            "la Bordeta",
            "Sants-Badal",
            "Sants",
            "les Corts",
            "la Maternitat i Sant Ramon",
            "Pedralbes",
            "Vallvidrera, el Tibidabo i les Planes",
            "Sarrià",
            "les Tres Torres",
            "Sant Gervasi-la Bonanova",
            "Sant Gervasi-Galvany",
            "el Putxet i el Farró",
            "Vallcarca i els Penitents",
            "el Coll",
            "la Salut",
            "la Vila de Gràcia",
            "el Camp d en Grassot i Gràcia Nova",
            "el Baix Guinardó",
            "Can Baró",
            "el Guinardó",
            "la Font d en Fargues",
            "el Carmel",
            "la Teixonera",
            "Sant Genís dels Agudells (1)",
            "Montbau (1)",
            "la Vall d Hebron",
            "la Clota",
            "Horta",
            "Vilapicina i la Torre Llobeta",
            "Porta",
            "el Turó de la Peira (2)",
            "Can Peguera (2)",
            "la Guineueta",
            "Canyelles (3)",
            "les Roquetes (3)",
            "Verdun",
            "la Prosperitat",
            "la Trinitat Nova",
            "Torre Baró (4)",
            "Ciutat Meridiana (4)",
            "Vallbona (4)",
            "la Trinitat Vella",
            "Baró de Viver",
            "el Bon Pastor",
            "Sant Andreu",
            "la Sagrera",
            "el Congrés i els Indians",
            "Navas",
            "el Camp de l Arpa del Clot",
            "el Clot",
            "el Parc i la Llacuna del Poblenou",
            "la Vila Olímpica del Poblenou",
            "el Poblenou",
            "Diagonal Mar i el Front Marítim del Poblenou",
            "el Besòs i el Maresme",
            "Provençals del Poblenou",
            "Sant Martí de Provençals",
            "la Verneda i la Pau"
             };


            int r = rnd.Next(zoneList.Length);
            string zone = (string)zoneList[r];
            return zone;
        }

        

        private static void RandomLocation(WorkoutsOld oWorkoutsOld, Random rnd)
        {
            string[] citiesList = new string[] { "Madrid", "Madrid", "Madrid", "Madrid", "Madrid", "Barcelona", "Barcelona", "Barcelona", "Valencia", "Valencia", "Bilbao", "Coruña", "Sevilla", "Santader", "Badajoz", "León", "Murcia", "Albacete", "Toledo", "Zaragoza"};

            Dictionary<string, DbGeography> DbGeographyList = new Dictionary<string, DbGeography>();
            DbGeographyList.Add("Madrid", DbGeography.FromText("POINT(-3.703479 40.417241)"));
            DbGeographyList.Add("Barcelona", DbGeography.FromText("POINT(2.175297 41.395208)"));
            DbGeographyList.Add("Valencia", DbGeography.FromText("POINT(-3.703479 40.417241)"));
            DbGeographyList.Add("Bilbao", DbGeography.FromText("POINT(-3.703479 40.417241)"));
            DbGeographyList.Add("Coruña", DbGeography.FromText("POINT(-3.703479 40.417241)"));
            DbGeographyList.Add("Sevilla", DbGeography.FromText("POINT(-3.703479 40.417241)"));
            DbGeographyList.Add("Santader", DbGeography.FromText("POINT(-3.703479 40.417241)"));
            DbGeographyList.Add("Badajoz", DbGeography.FromText("POINT(-3.703479 40.417241)"));
            DbGeographyList.Add("León", DbGeography.FromText("POINT(-3.703479 40.417241)"));
            DbGeographyList.Add("Murcia", DbGeography.FromText("POINT(-3.703479 40.417241)"));
            DbGeographyList.Add("Albacete", DbGeography.FromText("POINT(-3.703479 40.417241)"));
            DbGeographyList.Add("Toledo", DbGeography.FromText("POINT(-3.703479 40.417241)"));
            DbGeographyList.Add("Zaragoza", DbGeography.FromText("POINT(-3.703479 40.417241)"));
            
            int r = rnd.Next(citiesList.Length);
            string city = (string)citiesList[r];

            
            DbGeography oDbGeography = (DbGeography)DbGeographyList[city];

            double EarthRadius = 6371000;

            int MaxMeters = 10000;
            Random rndMeters = new Random();
            int rMeters = rndMeters.Next(MaxMeters);

            int MaxBearing = 360;
            Random rndBearing = new Random();
            double rBearing = rndMeters.NextDouble() * MaxBearing;

            var omega1 = oDbGeography.Latitude.Value;
            var lampda1 = oDbGeography.Longitude.Value;

            double sigma = (double) rMeters / EarthRadius;


            var omega2 = Math.Asin(Math.Sin(omega1) * Math.Cos(sigma) +
                    Math.Cos(omega1) * Math.Sin(sigma) * Math.Cos(rBearing));
            var lampda2 = lampda1 + Math.Atan2(Math.Sin(rBearing) * Math.Sin(sigma) * Math.Cos(omega1),
                                     Math.Cos(sigma) - Math.Sin(omega1) * Math.Sin(omega2));

            oWorkoutsOld.StartLocation = DbGeography.FromText("POINT(" + lampda2 + " " + omega2 + ")");

            oWorkoutsOld.City = city;
            oWorkoutsOld.Region = city;
            oWorkoutsOld.Country = "Spain";

        }

        private static DateTime RandomDay(Random rnd)
        {
            //DateTime start = new DateTime(2010, 1, 1);
            DateTime start = DateTime.Today;
            DateTime end = start.AddMonths(1);

            //int range = (DateTime.Today - start).Days;
            int range = (end - start).Days;
            int days = rnd.Next(range);

            
            TimeSpan startHour = TimeSpan.FromHours(9);
            TimeSpan endHour = TimeSpan.FromHours(23);
            int maxMinutes = (int)((endHour - startHour).TotalMinutes);

            int minutes = rnd.Next(maxMinutes);
            TimeSpan t = startHour.Add(TimeSpan.FromMinutes(minutes));

            var returnDate = start.AddDays(days);
            returnDate = returnDate.AddMinutes(t.TotalMinutes);


            return returnDate;
        }
        
    }
}

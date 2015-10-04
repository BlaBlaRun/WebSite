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

            int seconds = rnd.Next(maxSeconds);
            TimeSpan t = start.Add(TimeSpan.FromSeconds(seconds));
            return t;
        }

        private static void RandomLocationCity(WorkoutsOld oWorkoutsOld, Random rnd)
        {
            string[] citiesList = new string[] { "Madrid", "Madrid", "Madrid", "Madrid", "Madrid", "Barcelona", "Barcelona", "Barcelona", "Valencia", "Valencia", "Bilbao", "Coruña", "Sevilla", "Santader", "Badajoz", "León", "Murcia", "Albacete", "Toledo", "Zaragoza" };

           
            int r = rnd.Next(citiesList.Length);
            string city = (string)citiesList[r];

            
            oWorkoutsOld.City = city;
            oWorkoutsOld.Region = city;
            oWorkoutsOld.Country = "Spain";

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

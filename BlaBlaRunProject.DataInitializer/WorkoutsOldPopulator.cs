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
        const int DataNumber = 200;
        public static List<WorkoutsOld> PopulateData()
        {
            List<WorkoutsOld> WorkoutsOldList = new List<WorkoutsOld>();
            for (int i = 0; i < DataNumber; i++)
            {
                WorkoutsOld oWorkoutsOld = new WorkoutsOld();
                oWorkoutsOld.StartDateTime = RandomDay();
                RandomLocation(oWorkoutsOld);
                oWorkoutsOld.AVGPace = RandomTime();
                oWorkoutsOld.Distance = RandomDistance();
                RandomCircular(oWorkoutsOld);
                oWorkoutsOld.MaxNumberPeople = RandomMaxNumberPeople();
                oWorkoutsOld.ElevationGain = RandomElevation();
                oWorkoutsOld.UsersId = 0;
            }
                

            return WorkoutsOldList;
        }
        
        private static double? RandomDistance()
        {
            int MinMetersStart = 3000;
            int MaxMetersEnd = 15000;
            Random rnd = new Random();
            int r = rnd.Next(MaxMetersEnd - MinMetersStart);
            return r + MinMetersStart;
        }

        private static short? RandomMaxNumberPeople()
        {
            Random rnd = new Random();
            int r = rnd.Next(5);
            return (short) r;
        }

        private static double? RandomElevation()
        {
            int MaxMeters = 1000;
            Random rnd = new Random();
            int r = rnd.Next(MaxMeters);
            return r;
        }

        private static void RandomCircular(WorkoutsOld oWorkoutsOld)
        {
            Random rand = new Random();
            bool result = rand.Next(2) == 0;

            oWorkoutsOld.Circular = result;

            if (!oWorkoutsOld.Circular)
            {

                oWorkoutsOld.EndLocation = RandomLocationEnd(oWorkoutsOld);
            }
        }

        private static DbGeography RandomLocationEnd(WorkoutsOld oWorkoutsOld)
        {
            throw new NotImplementedException();
        }

        private static TimeSpan? RandomTime()
        {
            Random random = new Random();
            TimeSpan start = TimeSpan.FromHours(9);
            TimeSpan end = TimeSpan.FromHours(23);
            int maxMinutes = (int)((end - start).TotalMinutes);

            int minutes = random.Next(maxMinutes);
            TimeSpan t = start.Add(TimeSpan.FromMinutes(minutes));
            return t;
        }

        private static void RandomLocation(WorkoutsOld oWorkoutsOld)
        {
            string[] citiesList = new string[] { "Madrid", "Madrid", "Madrid", "Madrid", "Madrid", "Barcelona", "Barcelona", "Barcelona", "Valencia", "Valencia", "Bilbao", "Coruña", "Sevilla", "Santader", "Badajoz", "León", "Murcia", "Albacete", "Toledo", "Zaragoza"};
            
            Random rnd = new Random();
            int r = rnd.Next(citiesList.Length);
            string city = (string)citiesList[r];

            DbGeography[] GeographyList = new DbGeography[] { };
            DbGeography oDbGeography = (DbGeography)GeographyList[r];


            int MaxMeters = 10000;
            Random rndMeters = new Random();
            int rMeters = rndMeters.Next(MaxMeters);

            int MaxBearing = 360;
            Random rndBearing = new Random();
            double rBearing = rndMeters.NextDouble() * MaxBearing;

            var omega1 = oDbGeography.Latitude;
            var lampda1 = oDbGeography.Longitude;




            //var omega2 = Math.Asin(Math.Sin(omega1) * Math.Cos(d / R) +
            //        Math.Cos(omega1) * Math.Sin(d / R) * Math.Sincos(brng));
            //var lampda2 = λ1 + Math.atan2(Math.sin(brng) * Math.sin(d / R) * Math.cos(φ1),
            //                         Math.cos(d / R) - Math.sin(φ1) * Math.sin(φ2));

            oWorkoutsOld.City = city;
            oWorkoutsOld.Region = city;
            oWorkoutsOld.Country = "Spain";

        }

        private static DateTime RandomDay()
        {
            DateTime start = new DateTime(2010, 1, 1);
            Random gen = new Random();

            int range = (DateTime.Today - start).Days;
            return start.AddDays(gen.Next(range));
        }
        
    }
}

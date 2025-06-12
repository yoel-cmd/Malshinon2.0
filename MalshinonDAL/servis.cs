using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Malshinon2._0.Models;

namespace Malshinon2._0.MalshinonDAL
{
    internal class servis
    {
        
        private malshinonDAL malshinonDAL;

        public servis(malshinonDAL malshinonDAL)
        {
            this.malshinonDAL= malshinonDAL;
        }

        public void run()
        {
             malshinonDAL = new malshinonDAL("server=localhost;username=root;password=;database=malshinon2.0");


            string firsNameInformers, lastNameInformers, respons, allRespons;
            string[] strArr;
            int InformerId, ReportedId;

            Console.WriteLine("enter first mame");
            firsNameInformers = Console.ReadLine();
            Console.WriteLine("enter last mame");
            lastNameInformers = Console.ReadLine();

            if (!malshinonDAL.FindPersonByName(firsNameInformers, lastNameInformers))
            {
                malshinonDAL.createPeople(firsNameInformers, lastNameInformers, "Informers");
            }
            Console.WriteLine("enter full name target and your respons");
            respons = Console.ReadLine();

            strArr = malshinonDAL.parseReport(respons);

            if (!malshinonDAL.FindPersonByName(strArr[0], strArr[1]))
            {
                malshinonDAL.createPeople(strArr[0], strArr[1], "Target");
            }

            if (malshinonDAL.returnStatus(firsNameInformers, lastNameInformers) == "Target")
            {
                malshinonDAL.UpdateStatus(firsNameInformers, lastNameInformers, "Both");
            }
            if (malshinonDAL.returnStatus(strArr[0], strArr[1]) == "Informers")
            {
                malshinonDAL.UpdateStatus(strArr[0], strArr[1], "Both");
            }

            InformerId = malshinonDAL.returnIdByName(firsNameInformers, lastNameInformers);
            ReportedId = malshinonDAL.returnIdByName(strArr[0], strArr[1]);

            allRespons = String.Join(" ", strArr.Skip(2));

            malshinonDAL.UpdateReport(InformerId, ReportedId, allRespons);
        }
        
         public  void returnRiskPeopel()
        {
            List<people> peoples = new List<people>();
            peoples = malshinonDAL.Highisk();
            foreach (people item in peoples)
            {
                item.PrintInfo();
            }
        }
        public void reternPotentialAgent()

        {
            malshinonDAL.UpdateStatusForHighActivity();
            List<people> peoples = new List<people>();
            peoples = malshinonDAL.returnByStatus("PotentialAgent");
            foreach (people item in peoples)
            {
                item.PrintInfo();
            }
        }

    }

}

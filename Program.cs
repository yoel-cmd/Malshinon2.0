using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Malshinon2._0.MalshinonDAL;
using MySql.Data.MySqlClient;

namespace Malshinon2._0
{
    internal class Program
            //int num;
            //string str;
            //bool Exsist;

            ////malshinonDAL.createPeople("yoel", "ider");
            ////num= malshinonDAL.returnIdByName("yoel", "ider");
            //// Console.WriteLine(num);

            ////Exsist= malshinonDAL.checkIfSecretCodeExists("5,_|xq");
            ////Console.WriteLine(Exsist);
            ////string[]strArr= malshinonDAL.ParseReport("yoel ider him food");

            ////string strr = String.Join(" ",strArr.Skip(2));
            ////Console.WriteLine(strArr[0] + strArr[1]);
            ////Console.WriteLine(strr);

            ////Exsist= malshinonDAL.FindPersonByName("yoel", "ider");
            //// Console.WriteLine(Exsist);
            //malshinonDAL.deleteByName("yoel", "ider");
            //malshinonDAL.deleteByName("yoel", "ider");
    {
        static void Main(string[] args)
        {

            malshinonDAL malshinonDAL = new malshinonDAL("server=localhost;username=root;password=;database=malshinon2.0");
            servis servis = new servis(malshinonDAL);

            servis.run();
            //servis.returnRiskPeopel();

            //malshinonDAL.UpdateStatusForHighActivity();

            //servis.reternPotentialAgent();
            //malshinonDAL.readToLog("Test log entry");


            // double d;
            //d= malshinonDAL.returnLength(7);
            // Console.WriteLine(d);














        }
    }
}

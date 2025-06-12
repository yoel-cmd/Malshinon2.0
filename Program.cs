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
            
    {
        static void Main(string[] args)
        {

            Menu menu = new Menu();
            servis servis = new servis();


            menu.Run();
            //servis.reternPotentialAgent();
















        }
    }
}

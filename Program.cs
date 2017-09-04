using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Timers;

namespace ConsoleApplication14
{
    public class Attendance
    {
        private bool direction;
        private string command;
        
        public void Dochadzka()
        {
            Console.WriteLine("Type Employee ID");
            int eid = Int32.Parse(Console.ReadLine());
            int res;
            Console.WriteLine("IN or OUT ?");
            string input = Console.ReadLine();
            bool contains = input.IndexOf("out", StringComparison.OrdinalIgnoreCase) >= 0;
            Console.WriteLine("Contains = " + contains);
            if ((input.IndexOf("out", StringComparison.OrdinalIgnoreCase) >= 0) == true)
            {
                Console.WriteLine("1. Koniec PD");
                Console.WriteLine("2. Obed");
                Console.WriteLine("3. Lekar");
                Console.WriteLine("4. Sluzobne");
                Console.WriteLine("Enter code for a reason");
                res = Int32.Parse(Console.ReadLine());
                direction = true;
                command = "INSERT INTO Attendance(idEmployee,dateAtt,direction,idReason) VALUES (@val1,@val2,@val3,@val4)";
            }
            else if ((input.IndexOf("in", StringComparison.OrdinalIgnoreCase) >= 0) == true)
            {
                direction = false;
                res = 0;
                command = "INSERT INTO Attendance(idEmployee,dateAtt,direction) VALUES (@val1,@val2,@val3)";
            }
            else
            {
                Console.WriteLine("Nespravne zadany smer ");
                Environment.Exit(-1);
            }

            DateTime neww = DateTime.Now;
            String timestamp = neww.ToString("yyyyMMdd HH:mm:ss");
            Console.WriteLine("EmployeeID = "+ eid + "Date = "+timestamp+" Direction = " + direction);
            /*using (SqlConnection con = new SqlConnection("Data Source=ZASO\\SQLEXPRESS;Initial Catalog=MojaFirma;Integrated Security=True"))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = command;
                    cmd.Parameters.AddWithValue("@val1", eid);
                    cmd.Parameters.AddWithValue("@val2", timestamp);
                    cmd.Parameters.AddWithValue("@val3", direction);
                    cmd.Parameters.AddWithValue("@val4", res);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }*/
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            new Attendance().Dochadzka();
        }
    }
}

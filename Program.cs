using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;

namespace attendanceSimulation
{
    class Entry
    {
        public List<int> input = new List<int>();
        public List<int> output = new List<int>();

        public void pause(int dur)
        {
            DateTime noww = DateTime.Now;
            do { } while (noww.AddSeconds(dur) > DateTime.Now);
        }

        public void FillData()
        {
            int j = 0;
            for (int i = 1; i < 39; i++)
            {
                if (i != 6 && i != 7 && i != 12)
                {
                    input.Add(i);
                    j++;
                }
            }
            var rnd = new Random();
            input = input.OrderBy(x => rnd.Next()).ToList();
        }

        public void intoJob(List<DateTime> ts)
        {
            foreach (DateTime item in ts)
            {;
                sendQ(input[0], item, false, 0);
                //pause(1);
                if (!output.Contains(input[0]))
                {
                    output.Add(input[0]);
                    input.Remove(input[0]);
                }

            }
        }

        public void outJob(List<DateTime> ts, int reason)
        {
            foreach (DateTime item in ts)
            {;
                sendQ(output[0], item, false, reason);
                //pause(1);
                if (!input.Contains(output[0]))
                {
                    input.Add(output[0]);
                    output.Remove(output[0]);
                }
            }
        }

        public List<DateTime> cas(int day,int hours,int fromD, int toD,int count)
        {
            List<DateTime> final = new List<DateTime>();
            var rnd = new Random();
            DateTime ts = new DateTime();
            for (int i = 0; i < count; i++)
            {
                ts = new DateTime(2017, 01, day, hours, rnd.Next(fromD, toD), rnd.Next(0, 60));
                final.Add(ts);
            }           
            final.Sort();

            return final;
        }

        public void sendQ(int eid,DateTime timestamp, bool direction, int res)
        {
            using (SqlConnection con = new SqlConnection("Data Source=ZASO\\SQLEXPRESS;Initial Catalog=MojaFirma;Integrated Security=True"))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    String timef = timestamp.ToString("yyyyMMdd HH:mm:ss");
                    if (res != 0)
                    {
                        cmd.CommandText = "INSERT INTO Attendance(idEmployee,dateAtt,direction,idReason) VALUES (@val1,@val2,@val3,@val4)";
                        cmd.Parameters.AddWithValue("@val1", eid);
                        cmd.Parameters.AddWithValue("@val2", timef);
                        cmd.Parameters.AddWithValue("@val3", direction);
                        cmd.Parameters.AddWithValue("@val4", res);
                    }
                    else 
                    {
                        cmd.CommandText = "INSERT INTO Attendance(idEmployee,dateAtt,direction) VALUES (@val1,@val2,@val3)";
                        cmd.Parameters.AddWithValue("@val1", eid);
                        cmd.Parameters.AddWithValue("@val2", timef);
                        cmd.Parameters.AddWithValue("@val3", direction);
                    }
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        
    }
    
    class Program
    {

        static void Main(string[] args)
        {
            Entry newEntry = new Entry();
            List<DateTime> tmp = new List<DateTime>();
            for (int i = 0; i < 30; i += 5)
            {
                // MONDAY
                newEntry.FillData();
                tmp = newEntry.cas(i + 1, 7, 20, 60, 33);
                newEntry.intoJob(tmp);
                tmp = newEntry.cas(i + 1, 8, 0, 30, 2);
                newEntry.intoJob(tmp);
                tmp = newEntry.cas(i + 1, 11, 30, 60, 10);
                newEntry.outJob(tmp, 2);
                tmp = newEntry.cas(i + 1, 12, 0, 20, 5);
                newEntry.outJob(tmp, 2);
                tmp = newEntry.cas(i + 1, 12, 20, 40, 10);
                newEntry.intoJob(tmp);
                tmp = newEntry.cas(i + 1, 12, 40, 60, 5);
                newEntry.intoJob(tmp);
                tmp = newEntry.cas(i + 1, 16, 50, 60, 4);
                newEntry.outJob(tmp, 1);
                tmp = newEntry.cas(i + 1, 17, 0, 40, 31);
                newEntry.outJob(tmp, 1);

                newEntry.input.Clear();
                newEntry.output.Clear();
                // TUESDAY
                newEntry.FillData();
                tmp = newEntry.cas(i + 2, 7, 20, 60, 31);
                newEntry.intoJob(tmp);
                tmp = newEntry.cas(i + 2, 8, 0, 30, 4);
                newEntry.intoJob(tmp);
                tmp = newEntry.cas(i + 2, 11, 30, 60, 15);
                newEntry.outJob(tmp, 2);
                tmp = newEntry.cas(i + 2, 12, 0, 20, 4);
                newEntry.outJob(tmp, 2);
                tmp = newEntry.cas(i + 2, 12, 20, 40, 15);
                newEntry.intoJob(tmp);
                tmp = newEntry.cas(i + 2, 12, 40, 60, 4);
                newEntry.intoJob(tmp);
                tmp = newEntry.cas(i + 2, 14, 0, 30, 1);
                newEntry.outJob(tmp, 3);
                tmp = newEntry.cas(i + 2, 15, 0, 30, 1);
                newEntry.intoJob(tmp);
                tmp = newEntry.cas(i + 2, 16, 50, 60, 3);
                newEntry.outJob(tmp, 1);
                tmp = newEntry.cas(i + 2, 17, 0, 40, 32);
                newEntry.outJob(tmp, 1);

                newEntry.input.Clear();
                newEntry.output.Clear();
                // WEDNESDAY
                newEntry.FillData();
                tmp = newEntry.cas(i + 3, 7, 20, 60, 30);
                newEntry.intoJob(tmp);
                tmp = newEntry.cas(i + 3, 8, 0, 30, 5);
                newEntry.intoJob(tmp);
                tmp = newEntry.cas(i + 3, 11, 30, 60, 12);
                newEntry.outJob(tmp, 2);
                tmp = newEntry.cas(i + 3, 12, 0, 20, 10);
                newEntry.outJob(tmp, 2);
                tmp = newEntry.cas(i + 3, 12, 20, 40, 12);
                newEntry.intoJob(tmp);
                tmp = newEntry.cas(i + 3, 12, 40, 60, 10);
                newEntry.intoJob(tmp);
                tmp = newEntry.cas(i + 3, 15, 0, 60, 3);
                newEntry.outJob(tmp, 4);
                tmp = newEntry.cas(i + 3, 16, 0, 30, 3);
                newEntry.intoJob(tmp);
                tmp = newEntry.cas(i + 3, 16, 50, 60, 1);
                newEntry.outJob(tmp, 1);
                tmp = newEntry.cas(i + 3, 17, 0, 40, 34);
                newEntry.outJob(tmp, 1);

                newEntry.input.Clear();
                newEntry.output.Clear();
                // THURSDAY
                newEntry.FillData();
                tmp = newEntry.cas(i + 4, 7, 20, 60, 29);
                newEntry.intoJob(tmp);
                tmp = newEntry.cas(i + 4, 8, 0, 30, 6);
                newEntry.intoJob(tmp);
                tmp = newEntry.cas(i + 4, 11, 30, 60, 12);
                newEntry.outJob(tmp, 2);
                tmp = newEntry.cas(i + 4, 12, 0, 20, 10);
                newEntry.outJob(tmp, 2);
                tmp = newEntry.cas(i + 4, 12, 20, 40, 12);
                newEntry.intoJob(tmp);
                tmp = newEntry.cas(i + 4, 12, 40, 60, 10);
                newEntry.intoJob(tmp);
                tmp = newEntry.cas(i + 4, 15, 0, 30, 1);
                newEntry.outJob(tmp, 3);
                tmp = newEntry.cas(i + 4, 15, 30, 60, 2);
                newEntry.outJob(tmp, 4);
                tmp = newEntry.cas(i + 4, 16, 30, 50, 3);
                newEntry.intoJob(tmp);
                tmp = newEntry.cas(i + 4, 16, 50, 60, 6);
                newEntry.outJob(tmp, 1);
                tmp = newEntry.cas(i + 4, 17, 0, 40, 29);
                newEntry.outJob(tmp, 1);

                newEntry.input.Clear();
                newEntry.output.Clear();
                // FRIDAY
                newEntry.FillData();
                tmp = newEntry.cas(i + 5, 7, 20, 60, 25);
                newEntry.intoJob(tmp);
                tmp = newEntry.cas(i + 5, 8, 0, 30, 10);
                newEntry.intoJob(tmp);
                tmp = newEntry.cas(i + 5, 11, 30, 60, 9);
                newEntry.outJob(tmp, 2);
                tmp = newEntry.cas(i + 5, 12, 0, 20, 11);
                newEntry.outJob(tmp, 2);
                tmp = newEntry.cas(i + 5, 12, 20, 40, 9);
                newEntry.intoJob(tmp);
                tmp = newEntry.cas(i + 5, 12, 40, 60, 11);
                newEntry.intoJob(tmp);
                tmp = newEntry.cas(i + 5, 14, 0, 30, 1);
                newEntry.outJob(tmp, 4);
                tmp = newEntry.cas(i + 5, 14, 30, 60, 1);
                newEntry.outJob(tmp, 4);
                tmp = newEntry.cas(i + 5, 15, 30, 60, 3);
                newEntry.outJob(tmp, 4);
                tmp = newEntry.cas(i + 5, 16, 0, 50, 5);
                newEntry.intoJob(tmp);
                tmp = newEntry.cas(i + 5, 16, 50, 60, 12);
                newEntry.outJob(tmp, 1);
                tmp = newEntry.cas(i + 5, 17, 0, 40, 23);
                newEntry.outJob(tmp, 1);

            }
        }
    }
}

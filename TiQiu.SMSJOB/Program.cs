using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using TiQiu.Common.Util;
using TiQiu.DAL;


namespace TiQiu.SMSJOB
{
    class Program
    {

        static System.Timers.Timer timer;
        private static Timer timerforQuery;
        static SMSTask sTask;
        static void Main(string[] args)
        {

            Console.WriteLine("短信JOB启动！");
            sTask = new SMSTask();
            sTask.Register();
            timer = new System.Timers.Timer();
            timer.Interval = 2 * 1000;
            timer.Elapsed += timer_Elapsed;
            timer.Start();

            timerforQuery = new Timer();
            timerforQuery.Interval = 1000*60*60*24;
            timerforQuery.Elapsed += TimerforQueryOnElapsed;
            timerforQuery.Start();
            Console.ReadLine();
        }

        private static void TimerforQueryOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            timerforQuery.Stop();
            try
            {
                
                Console.WriteLine("Balance: " + sTask.GetBalance());
                //sTask.getMO();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex.Message);
                Console.WriteLine(ex.Message);
            }
            finally
            {
                timerforQuery.Start();
            }
        }

        static void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            timer.Stop();
            try
            {
                sTask.SendSMS();
                //sTask.getMO();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex.Message);
                Console.WriteLine(ex.Message);
            }
            finally
            {
                timer.Start();
            }
        }
    }
}

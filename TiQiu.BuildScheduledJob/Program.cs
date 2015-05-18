using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiQiu.Biz;
using TiQiu.Common.Util;
using TiQiu.DAL;


namespace TiQiu.BuildScheduledJob
{
    class Program
    {
        static System.Timers.Timer timer;
        
        static void Main(string[] args)
        {

            Console.WriteLine("场次初始化JOB启动！");
            
            timer = new System.Timers.Timer();
            
            timer.Interval = 1000 * 60 * 60;
            timer.Elapsed += timer_Elapsed;
            timer.Start();

           
            Console.ReadLine();
        }

        private static void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            timer.Stop();
            try
            {

                DateTime now = DateTime.Now;
                if (now.Hour == 0)
                {
                    Stopwatch watch = new Stopwatch();
                    watch.Start();

                    List<FIELD> fields = new List<FIELD>();
                    using (TIQIUEntities context = new TIQIUEntities())
                    {
                        fields = context.FIELDs.Where(f => f.STATUS == 1).ToList();
                    }
                    fields.ForEach(f => FieldManager.CreateFieldScheduled(f.ID, DateTime.Now, DateTime.Now.AddDays(14)));

                    watch.Stop();
                    Console.WriteLine(string.Format("执行场次初始化，有效球场{0},执行时间{1}, at {2}",fields.Count,watch.ElapsedMilliseconds,now.ToLocalTime()));
                }
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

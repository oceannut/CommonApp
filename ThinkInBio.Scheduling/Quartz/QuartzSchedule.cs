﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Q = Quartz;

namespace ThinkInBio.Scheduling.Quartz
{

    public class QuartzSchedule : ISchedule
    {

        private Q.IScheduler scheduler;

        /// <summary>
        /// Job开始执行的时间
        /// </summary>
        public DateTime JobStartTime { get; set; }

        /// <summary>
        /// 重复间隔时间（秒）。
        /// </summary>
        public int RepeatInterval { get; set; }

        /// <summary>
        /// 重复次数。
        /// </summary>
        public int RepeatCount { get; set; }

        /// <summary>
        /// Cron表达式是一个由7个子表达式组成的字符串。每个子表达式都描述了一个单独的日程细节。这些子表达式用空格分隔，分别表示
        /// 1. Seconds 秒
        /// 2. Minutes 分钟
        /// 3. Hours 小时
        /// 4. Day-of-Month 月中的天
        /// 5. Month 月
        /// 6. Day-of-Week 周中的天
        /// 7. Year (optional field) 年（可选的域）
        /// 一个cron表达式的例子字符串为"0 0 12 ? * WED",这表示“每周三的中午12：00”。
        /// 单个子表达式可以包含范围或者列表。例如：前面例子中的周中的天这个域（这里是"WED"）可以被替换为"MON-FRI", "MON, WED, FRI"或者甚至"MON-WED,SAT"。
        /// 通配符（'*'）可以被用来表示域中“每个”可能的值。因此在"Month"域中的*表示每个月，而在Day-Of-Week域中的*则表示“周中的每一天”。
        /// 所有的域中的值都有特定的合法范围，这些值的合法范围相当明显，例如：秒和分域的合法值为0到59，小时的合法范围是0到23，Day-of-Month中值得合法凡范围是0到31，但是需要注意不同的月份中的天数不同。月份的合法值是0到11。或者用字符串JAN,FEB MAR, APR, MAY, JUN, JUL, AUG, SEP, OCT, NOV 及DEC来表示。Days-of-Week可以用1到7来表示（1=星期日）或者用字符串SUN, MON, TUE, WED, THU, FRI 和SAT来表示.
        /// '/'字符用来表示值的增量，例如, 如果分钟域中放入'0/15'，它表示“每隔15分钟，从0开始”，如果在份中域中使用'3/20'，则表示“小时中每隔20分钟，从第3分钟开始”或者另外相同的形式就是'3,23,43'。
        /// '?'字符可以用在day-of-month及day-of-week域中，它用来表示“没有指定值”。这对于需要指定一个或者两个域的值而不需要对其他域进行设置来说相当有用。
        /// 'L'字符可以在day-of-month及day-of-week中使用，这个字符是"last"的简写，但是在两个域中的意义不同。例如，在day-of-month域中的"L"表示这个月的最后一天，即，一月的31日，非闰年的二月的28日。如果它用在day-of-week中，则表示"7"或者"SAT"。但是如果在day-of-week域中，这个字符跟在别的值后面，则表示"当月的最后的周XXX"。例如："6L" 或者 "FRIL"都表示本月的最后一个周五。当使用'L'选项时，最重要的是不要指定列表或者值范围，否则会导致混乱。
        /// 'W' 字符用来指定距离给定日最接近的周几（在day-of-week域中指定）。例如：如果你为day-of-month域指定为"15W",则表示“距离月中15号最近的周几”。
        /// '#'表示表示月中的第几个周几。例如：day-of-week域中的"6#3" 或者 "FRI#3"表示“月中第三个周五”。
        /// </summary>
        public string CronExpression { get; set; }

        public QuartzSchedule(Q.ISchedulerFactory schedulerFactory)
        {
            if (schedulerFactory == null)
            {
                throw new ArgumentNullException();
            }
            scheduler = schedulerFactory.GetScheduler();

            DateTimeOffset runTime = Q.DateBuilder.EvenMinuteDate(DateTimeOffset.UtcNow);

            Q.IJobDetail job = Q.JobBuilder.Create<QuartzJob>()
                .WithIdentity("job1", "group1")
                .Build();

            Q.ITrigger trigger = Q.TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .StartAt(runTime)
                .Build();

            scheduler.ScheduleJob(job, trigger);
        }

        public void Start()
        {
            scheduler.Start();
        }

        public void Stop()
        {
            scheduler.Shutdown(true);
        }

    }

}

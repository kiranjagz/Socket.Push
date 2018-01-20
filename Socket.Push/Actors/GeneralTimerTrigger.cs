﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Socket.Push.Actors
{
    public class GeneralTimerTrigger
    {
        public class Start
        {
            public DateTime TriggerDateTime { get; private set; }
            public Start(DateTime triggerDateTime)
            {
                TriggerDateTime = triggerDateTime;
            }
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MemoryReader.Handler
{
    public static class ExitHandler
    {
        [DllImport("Kernel32")]
        private static extern bool SetConsoleCtrlHandler(EventHandler handler, bool add);

        private delegate bool EventHandler(CtrlType sig);
        static EventHandler _handler;

        enum CtrlType
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT = 1,
            CTRL_CLOSE_EVENT = 2,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT = 6
        }

        public static event Action OnConsloeExit;

        static ExitHandler()
        {
            _handler += Handler;
            SetConsoleCtrlHandler(_handler, true);
        }

        private static bool Handler(CtrlType sig)
        {
            if(sig == CtrlType.CTRL_CLOSE_EVENT)
            {
                OnConsloeExit?.Invoke();
                return true;
            }
            return false;
        }
    }
}

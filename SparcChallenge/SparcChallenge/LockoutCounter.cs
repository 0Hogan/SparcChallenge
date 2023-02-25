﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparcChallenge
{
    class LockoutCounter
    {
        private int counter = 0;

        /// <summary>
        /// Resets the lockout counter to 0. Should be called when an attempt to load a profile is authorized.
        /// </summary>
        public void Reset()
        {
            counter = 0;
        }

        /// <summary>
        /// Returns whether the LockoutCounter is locked (i.e. whether there have been 3 or more unsuccessful attempts in a row)
        /// </summary>
        public bool IsLocked { get { return counter > 2; } }
    }
}
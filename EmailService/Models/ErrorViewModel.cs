/* ------------------------------------------------------------------
 * The GNU General Public License v3.0
 * Copyright (C) 2007 Free Software Foundation, Inc. <https://fsf.org/>
 * Asif Bhat
   ------------------------------------------------------------------ */
using System;

namespace EmailService.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}

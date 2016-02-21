﻿/*
 * Copyright (c) 2007-2012, Masahiko Kamo (mkamo@mkamo.com).
 * All Rights Reserved.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting.Channels;
using Mkamo.Memopad.Remote;
using System.Diagnostics;
using System.Threading;

namespace Mkamo.ConfidanteClientW {
    class Program {
        [STAThread]
        static void Main(string[] args) {
            MemopadClient.Start(args, false);
        }
    }
}

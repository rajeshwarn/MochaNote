﻿/*
 * Copyright (c) 2007-2012, Masahiko Kamo (mkamo@mkamo.com).
 * All Rights Reserved.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mkamo.Editor.Core;
using Mkamo.StyledText.Core;
using Mkamo.Common.Forms.Descriptions;

namespace Mkamo.Editor.Requests {
    public class SetStyledTextFontRequest: AbstractRequest {
        // ========================================
        // field
        // ========================================
        public Func<Flow, FontDescription> FontProvider;
        public FontModificationKinds Kinds;

        // ========================================
        // constructor
        // ========================================

        // ========================================
        // property
        // ========================================
        public override string Id {
            get { return RequestIds.SetStyledTextFont; }
        }

        // ========================================
        // method
        // ========================================
    }
}

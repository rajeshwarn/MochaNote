﻿/*
 * Copyright (c) 2007-2012, Masahiko Kamo (mkamo@mkamo.com).
 * All Rights Reserved.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mkamo.Figure.Core;
using Mkamo.Editor.Handles;

namespace Mkamo.Memopad.Internal.Handles {
    internal class CompositeCommentAnchorHandle: CompositeEdgePointHandle {
        // ========================================
        // method
        // ========================================
        protected override void UpdateHandles(IFigure hostFigure) {
            var edge = hostFigure as IEdge;
            var conn = hostFigure as IConnection;
            if (edge == null || conn == null) {
                return;
            }

            Children.Clear();
            // todo: First()，Last()とforeachで書きなおす
            for (int i = 0; i < edge.EdgePointRefs.Count(); ++i) {
                if (i == 0) {
                    //Children.Add(new EdgeAnchorHandle(conn.SourceAnchor) { Cursor = Cursor });
                    Children.Add(new EdgeCommentAnchorHandle(conn.SourceAnchor) { Cursor = Cursor });
                } else if (i == edge.EdgePointRefs.Count() - 1) {
                    //Children.Add(new EdgeAnchorHandle(conn.TargetAnchor) { Cursor = Cursor });
                    Children.Add(new EdgeCommentAnchorHandle(conn.TargetAnchor) { Cursor = Cursor });
                //} else {
                //    Children.Add(new MoveEdgePointHandle(edge.EdgePointRefs.ElementAt(i)) { Cursor = Cursor });
                }
            }
        }

    }
}

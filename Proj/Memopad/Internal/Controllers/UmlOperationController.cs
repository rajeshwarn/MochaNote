﻿/*
 * Copyright (c) 2007-2012, Masahiko Kamo (mkamo@mkamo.com).
 * All Rights Reserved.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mkamo.Editor.Controllers;
using Mkamo.Figure.Core;
using Mkamo.Editor.Core;
using Mkamo.Figure.Figures;
using System.Drawing;
using Mkamo.Common.Forms.Descriptions;
using Mkamo.Common.Forms.Drawing;
using Mkamo.Editor.Roles;
using Mkamo.Memopad.Internal.Utils;
using Mkamo.Editor.Handles;
using Mkamo.Editor.Focuses;
using Mkamo.Model.Uml;
using Mkamo.Memopad.Properties;
using Mkamo.Common.Externalize;
using Mkamo.Memopad.Internal.Core;
using Mkamo.Memopad.Internal.Controllers.UIProviders;
using Mkamo.Memopad.Internal.Helpers;
using Mkamo.StyledText.Core;
using Mkamo.Model.Utils;
using Mkamo.Common.Core;
using Mkamo.Memopad.Core;

namespace Mkamo.Memopad.Internal.Controllers {
    internal class UmlOperationController: AbstractModelController<UmlOperation, SimpleRect> {
        // ========================================
        // field
        // ========================================
        private Lazy<IUIProvider> _uiProvider;

        // ========================================
        // constructor
        // ========================================
        public UmlOperationController() {
            _uiProvider = new Lazy<IUIProvider>(() => new UmlOperationUIProvider(this, Model));
        }

        // ========================================
        // property
        // ========================================
        public override IUIProvider UIProvider {
            get { return _uiProvider.Value; }
        }
    
        // ========================================
        // method
        // ========================================
        public override void ConfigureEditor(IEditor editor) {
            var editorHandle = new SelectEditorHandle();
            editor.InstallEditorHandle(editorHandle);
            var facade = MemopadApplication.Instance;
            editorHandle.KeyMap = facade.KeySchema.UmlFeatureEditorKeyMap;

            editor.InstallHandle(new SelectionIndicatingHandle());
            editor.InstallRole(new SelectRole());
            editor.InstallRole(
                new FocusRole(
                    UmlFeatureEditorHelper.InitUmlOperationFocus,
                    UmlFeatureEditorHelper.CommitUmlOperationFocus
                )
            );
            editor.InstallRole(new ReorderRole());
            editor.InstallRole(new RemoveRole());
            //editor.InstallRole(new SetPlainTextFontRole(FontModificationKinds.Name));
            var editorFocus = new StyledTextFocus();
            editorFocus.KeyMap = facade.KeySchema.MemoContentSingleLineFocusKeyMap;
            editor.InstallFocus(editorFocus);
        }

        protected override SimpleRect CreateFigure(UmlOperation model) {
            return new SimpleRect() {
                IsForegroundEnabled = false,
                IsBackgroundEnabled = false,
                AutoSizeKinds = AutoSizeKinds.FitWidth,
                Font = MemopadApplication.Instance.Settings.GetDefaultUmlFont(),
                Padding = MemopadConsts.UmlFeaturePadding,
            };
        }

        protected override void RefreshEditor(RefreshContext context, SimpleRect figure, UmlOperation model) {
            var text = UmlTextUtil.GetOperationText(model);
            var run = new Run(text);
            var para = new Paragraph(run) {
                Padding = Insets.Empty,
                HorizontalAlignment = Mkamo.Common.DataType.HorizontalAlignment.Left,
            };
            var st = new StyledText.Core.StyledText(para) {
                Font = MemopadApplication.Instance.Settings.GetDefaultUmlFont(),
                VerticalAlignment = Mkamo.Common.DataType.VerticalAlignment.Center,
            };

            if (model.IsStatic) {
                run.Font = new FontDescription(run.Font, run.Font.Style | FontStyle.Underline);
            }
            if (model.IsAbstract) {
                run.Font = new FontDescription(run.Font, run.Font.Style | FontStyle.Italic);
            }

            figure.StyledText = st;
            figure.AdjustSize();

            var clsFig = Host.Parent.Parent.Figure as INode;
            if (clsFig != null) {
                clsFig.AdjustSize();
            }
        }

        public override IMemento GetModelMemento() {
            var externalizer = new Externalizer();
            return externalizer.Save(Model, (key, obj) => false);
        }

        public override string GetText() {
            var ret = Model.Name + ": " + Model.TypeName;
            if (!string.IsNullOrEmpty(Model.Stereotype)) {
                ret += Environment.NewLine + Model.Stereotype;
            }
            if (!string.IsNullOrEmpty(Model.TypeName)) {
                ret += Environment.NewLine + Model.TypeName;
            }
            if (!string.IsNullOrEmpty(Model.Parameters)) {
                ret += Environment.NewLine + Model.Parameters;
            }
            if (Model.Keywords != null) {
                ret = ret + Environment.NewLine + Model.Keywords;
            }
            return ret;
        }
    }
}

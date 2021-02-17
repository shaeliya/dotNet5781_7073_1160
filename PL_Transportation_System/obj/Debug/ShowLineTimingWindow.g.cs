﻿#pragma checksum "..\..\ShowLineTimingWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "28062A2B7FA8822A0BA2D85D2151BDAE0C931F49EE5FF30AB6505EA4099B23E6"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using BO;
using PL_Transportation_System;
using PL_Transportation_System.Utils;
using RadialProgressbar;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace PL_Transportation_System {
    
    
    /// <summary>
    /// ShowLineTimingWindow
    /// </summary>
    public partial class ShowLineTimingWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 32 "..\..\ShowLineTimingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbLineStations;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\ShowLineTimingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lvLineTimings;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\ShowLineTimingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblCurrentTime;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\ShowLineTimingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnStopSimulation;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\ShowLineTimingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnRestartSimulation;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/PL_Transportation_System;component/showlinetimingwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ShowLineTimingWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.cbLineStations = ((System.Windows.Controls.ComboBox)(target));
            
            #line 32 "..\..\ShowLineTimingWindow.xaml"
            this.cbLineStations.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cbLineStations_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.lvLineTimings = ((System.Windows.Controls.ListView)(target));
            return;
            case 3:
            this.lblCurrentTime = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.btnStopSimulation = ((System.Windows.Controls.Button)(target));
            
            #line 54 "..\..\ShowLineTimingWindow.xaml"
            this.btnStopSimulation.Click += new System.Windows.RoutedEventHandler(this.StopSimulation_Clicked);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnRestartSimulation = ((System.Windows.Controls.Button)(target));
            
            #line 55 "..\..\ShowLineTimingWindow.xaml"
            this.btnRestartSimulation.Click += new System.Windows.RoutedEventHandler(this.RestartSimulation_Clicked);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}


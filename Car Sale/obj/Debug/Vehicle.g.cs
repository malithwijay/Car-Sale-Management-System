﻿#pragma checksum "..\..\Vehicle.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "6B0C924C889D8C0F73D600607DD493A8FD8C503AAA3A5EC0D77C77751649098E"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Car_Sale;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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


namespace Car_Sale {
    
    
    /// <summary>
    /// Vehicle
    /// </summary>
    public partial class Vehicle : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 34 "..\..\Vehicle.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAddcar;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\Vehicle.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnUpdatecar;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\Vehicle.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnViewcar;
        
        #line default
        #line hidden
        
        
        #line 84 "..\..\Vehicle.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnManufacturer;
        
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
            System.Uri resourceLocater = new System.Uri("/Car Sale;component/vehicle.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Vehicle.xaml"
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
            this.btnAddcar = ((System.Windows.Controls.Button)(target));
            
            #line 34 "..\..\Vehicle.xaml"
            this.btnAddcar.Click += new System.Windows.RoutedEventHandler(this.BtnAddcar_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btnUpdatecar = ((System.Windows.Controls.Button)(target));
            
            #line 52 "..\..\Vehicle.xaml"
            this.btnUpdatecar.Click += new System.Windows.RoutedEventHandler(this.BtnUpdatecar_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnViewcar = ((System.Windows.Controls.Button)(target));
            
            #line 68 "..\..\Vehicle.xaml"
            this.btnViewcar.Click += new System.Windows.RoutedEventHandler(this.BtnViewcar_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnManufacturer = ((System.Windows.Controls.Button)(target));
            
            #line 84 "..\..\Vehicle.xaml"
            this.btnManufacturer.Click += new System.Windows.RoutedEventHandler(this.BtnManufacturer_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}


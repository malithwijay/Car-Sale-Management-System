#pragma checksum "..\..\Invoice.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "F76DC92404E34224750C4ED23D618FED9A80711149823543D5A92B2946708B48"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
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
    /// Invoice
    /// </summary>
    public partial class Invoice : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 26 "..\..\Invoice.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid print;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\Invoice.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txt_Customer_Name;
        
        #line default
        #line hidden
        
        
        #line 78 "..\..\Invoice.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txt_Customer_ID;
        
        #line default
        #line hidden
        
        
        #line 89 "..\..\Invoice.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txt_Invoice_No;
        
        #line default
        #line hidden
        
        
        #line 100 "..\..\Invoice.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txt_Date;
        
        #line default
        #line hidden
        
        
        #line 130 "..\..\Invoice.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txt_box_car_details;
        
        #line default
        #line hidden
        
        
        #line 183 "..\..\Invoice.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txt_cash_payment;
        
        #line default
        #line hidden
        
        
        #line 195 "..\..\Invoice.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txt_cash_amount;
        
        #line default
        #line hidden
        
        
        #line 203 "..\..\Invoice.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txt_finance_payment;
        
        #line default
        #line hidden
        
        
        #line 215 "..\..\Invoice.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txt_finance_amount;
        
        #line default
        #line hidden
        
        
        #line 226 "..\..\Invoice.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txt_customer_amount;
        
        #line default
        #line hidden
        
        
        #line 234 "..\..\Invoice.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txt_paid_date;
        
        #line default
        #line hidden
        
        
        #line 241 "..\..\Invoice.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txt_total_paid;
        
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
            System.Uri resourceLocater = new System.Uri("/Car Sale;component/invoice.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Invoice.xaml"
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
            
            #line 23 "..\..\Invoice.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Print_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.print = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.txt_Customer_Name = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.txt_Customer_ID = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.txt_Invoice_No = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.txt_Date = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            this.txt_box_car_details = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.txt_cash_payment = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 9:
            this.txt_cash_amount = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 10:
            this.txt_finance_payment = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 11:
            this.txt_finance_amount = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 12:
            this.txt_customer_amount = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 13:
            this.txt_paid_date = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 14:
            this.txt_total_paid = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}


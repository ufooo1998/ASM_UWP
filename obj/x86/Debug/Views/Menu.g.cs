﻿#pragma checksum "C:\Users\LE DANG NHAN\source\repos\DemoUWP\ASM\Views\Menu.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "429B6F1B4C2BFE479FFB51519AD9DDEC"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ASM.Views
{
    partial class Menu : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.17.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // Views\Menu.xaml line 11
                {
                    this.MainMenu = (global::Windows.UI.Xaml.Controls.SplitView)(target);
                }
                break;
            case 3: // Views\Menu.xaml line 14
                {
                    global::Windows.UI.Xaml.Controls.Primitives.ToggleButton element3 = (global::Windows.UI.Xaml.Controls.Primitives.ToggleButton)(target);
                    ((global::Windows.UI.Xaml.Controls.Primitives.ToggleButton)element3).Click += this.Button_Click;
                }
                break;
            case 4: // Views\Menu.xaml line 18
                {
                    global::Windows.UI.Xaml.Controls.RadioButton element4 = (global::Windows.UI.Xaml.Controls.RadioButton)(target);
                    ((global::Windows.UI.Xaml.Controls.RadioButton)element4).Click += this.RadioButton_Click;
                }
                break;
            case 5: // Views\Menu.xaml line 25
                {
                    global::Windows.UI.Xaml.Controls.RadioButton element5 = (global::Windows.UI.Xaml.Controls.RadioButton)(target);
                    ((global::Windows.UI.Xaml.Controls.RadioButton)element5).Click += this.RadioButton_Click;
                }
                break;
            case 6: // Views\Menu.xaml line 36
                {
                    this.MainContent = (global::Windows.UI.Xaml.Controls.Frame)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.17.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

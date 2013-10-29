using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using Base2io.Ventanas.Logic;
using Base2io.Ventanas.Model;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;

namespace Base2io.Ventanas.Views
{
    /// <summary>
    /// Interaction logic for Preferences.xaml
    /// </summary>
    public partial class Preferences
    {
        #region Constructor

        public Preferences()
        {
            CustomizedHotkeys = new ObservableCollection<PositionHotkey>(WindowPlacement.Instance.PositionHotkeys);
            InitializeComponent();
        } 

        #endregion

        #region Properties

        public ObservableCollection<PositionHotkey> CustomizedHotkeys { get; set; }

        #endregion

        #region Event Handlers

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
        
        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            WindowPlacement.Instance.RegisterHotkeys(CustomizedHotkeys.ToList());
            Close();
        }

        #endregion

        private void RemoveButtonClick(object sender, RoutedEventArgs e)
        {
            // Get the selected hotkey item.
            PositionHotkey positionHotkey = HotkeyList.SelectedItem as PositionHotkey;

            if (positionHotkey != null)
            {
                positionHotkey.Clear();
            }
        }

        private void HotkeyEntryBox_OnKeyDown(object sender, KeyEventArgs e)
        {
            PositionHotkey entry = new PositionHotkey
                {
                    KeyCode = (Keys)KeyInterop.VirtualKeyFromKey(e.Key),
                    IsCtrlKeyUsed = Keyboard.Modifiers.HasFlag(ModifierKeys.Control),
                    IsAltKeyUsed = Keyboard.Modifiers.HasFlag(ModifierKeys.Alt),
                    IsShiftKeyUsed = Keyboard.Modifiers.HasFlag(ModifierKeys.Shift),
                    IsWinKeyUsed = Keyboard.Modifiers.HasFlag(ModifierKeys.Windows)
                };

            HotkeyEntryBox.Text = entry.KeyBindingString;
        }
    }
}

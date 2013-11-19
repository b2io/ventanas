using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using Base2io.Ventanas.Annotations;
using Base2io.Ventanas.Logic;
using Base2io.Ventanas.Model;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;

namespace Base2io.Ventanas.Views
{
    /// <summary>
    /// Interaction logic for Preferences.xaml
    /// </summary>
    public partial class Preferences : INotifyPropertyChanged
    {
        private const string HOTKEY_PROMPT = "Click to enter a new hotkey";

        private PositionHotkey hotkeyEntry;

        #region Constructor

        public Preferences()
        {
            CustomizedHotkeys = new ObservableCollection<PositionHotkey>(WindowPlacement.Instance.PositionHotkeys);
            InitializeComponent();
        } 

        #endregion

        #region Properties

        public ObservableCollection<PositionHotkey> CustomizedHotkeys { get; set; }

        private bool _isHotkeyEntered;
        public bool IsHotkeyEntered
        {
            get { return _isHotkeyEntered; }
            set
            {
                if (value != _isHotkeyEntered)
                {
                    _isHotkeyEntered = value;
                    OnPropertyChanged("IsHotkeyEntered");
                }
            }
        }

        private bool _isSelectedHotkeyValid;
        public bool IsSelectedHotkeyValid
        {
            get { return _isSelectedHotkeyValid; }
            set
            {
                if (value != _isSelectedHotkeyValid)
                {
                    _isSelectedHotkeyValid = value;
                    OnPropertyChanged("IsSelectedHotkeyValid");
                }
            }
        }


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

        private void RemoveButtonClick(object sender, RoutedEventArgs e)
        {
            // Get the selected hotkey item.
            PositionHotkey positionHotkey = HotkeyList.SelectedItem as PositionHotkey;

            if (positionHotkey != null)
            {
                positionHotkey.Clear();
            }
            IsSelectedHotkeyValid = false;
        }

        private void HotkeyEntryBox_OnKeyDown(object sender, KeyEventArgs e)
        {
            // Mark as handled so that the text control doesn't attempt to handle it.
            e.Handled = true;

            hotkeyEntry = new PositionHotkey
                {
                    KeyCode = (Keys)KeyInterop.VirtualKeyFromKey(e.Key),
                    IsCtrlKeyUsed = Keyboard.Modifiers.HasFlag(ModifierKeys.Control),
                    IsAltKeyUsed = Keyboard.Modifiers.HasFlag(ModifierKeys.Alt),
                    IsShiftKeyUsed = Keyboard.Modifiers.HasFlag(ModifierKeys.Shift),
                    IsWinKeyUsed = Keyboard.Modifiers.HasFlag(ModifierKeys.Windows)
                };

            HotkeyEntryBox.Text = hotkeyEntry.KeyBindingString;
            IsHotkeyEntered = true;
        }

        private void HotkeyList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ResetHotkeyEntry();

            PositionHotkey selectedHotkey = HotkeyList.SelectedItem as PositionHotkey;
            IsSelectedHotkeyValid = selectedHotkey != null && selectedHotkey.KeyCode != Keys.None;
        }

        private void HotkeyEntryBox_OnInitialized(object sender, EventArgs e)
        {
            ResetHotkeyEntry();
        }

        private void HotkeyEntryBox_OnGotFocus(object sender, RoutedEventArgs e)
        {
            HotkeyEntryBox.Text = string.Empty;
        }

        private void Apply_OnClick(object sender, RoutedEventArgs e)
        {
            // Get the selected position hotkey.
            PositionHotkey selectedHotkey = (PositionHotkey)HotkeyList.SelectedItem;

            // Update the selected hotkey to have the new values:
            selectedHotkey.KeyCode = hotkeyEntry.KeyCode;
            selectedHotkey.IsCtrlKeyUsed = hotkeyEntry.IsCtrlKeyUsed;
            selectedHotkey.IsAltKeyUsed = hotkeyEntry.IsAltKeyUsed;
            selectedHotkey.IsShiftKeyUsed = hotkeyEntry.IsShiftKeyUsed;
            selectedHotkey.IsWinKeyUsed = hotkeyEntry.IsWinKeyUsed;

            IsSelectedHotkeyValid = false;
        }

        #endregion

        #region Private Logic

        private void ResetHotkeyEntry()
        {
            // Set the prompt text in the hotkey entry box.
            if (HotkeyEntryBox != null)
            {
                HotkeyEntryBox.Text = HOTKEY_PROMPT;
            }
            IsHotkeyEntered = false;
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}

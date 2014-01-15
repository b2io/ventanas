using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Shapes;
using Base2io.Ventanas.Annotations;
using Base2io.Ventanas.Enums;
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

        private PositionHotkey _hotkeyEntry;

        #region Constructor

        public Preferences()
        {
            // Disable the hotkeys temporarily so that the preference window doesn't move around.
            WindowPlacement.Instance.DisableHotkeys();

            CustomizedHotkeys = new ObservableCollection<PositionHotkey>(WindowPlacement.Instance.PositionHotkeys);

            IsWindowsStartup = Properties.Settings.Default.WindowsStartup;

            InitializeComponent();

            // Initialize the visualizer selection.
            PositionHotkey selectedHotkey = HotkeyList.SelectedItem as PositionHotkey;
            if (selectedHotkey != null)
            {
                UpdateVisualizer(selectedHotkey);
            }
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

        private bool _isWindowsStartup;
        public bool IsWindowsStartup
        {
            get { return _isWindowsStartup; }
            set
            {
                if (value != _isWindowsStartup)
                {
                    _isWindowsStartup = value;
                    OnPropertyChanged("IsWindowsStartup");
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

            if (IsWindowsStartup)
            {
                ApplicationSettings.SetWindowsStartup();
            }
            else
            {
                ApplicationSettings.RemoveWindowsStartup();
            }

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

            _hotkeyEntry = new PositionHotkey
                {
                    KeyCode = (Keys)KeyInterop.VirtualKeyFromKey(e.Key),
                    IsCtrlKeyUsed = Keyboard.Modifiers.HasFlag(ModifierKeys.Control),
                    IsAltKeyUsed = Keyboard.Modifiers.HasFlag(ModifierKeys.Alt),
                    IsShiftKeyUsed = Keyboard.Modifiers.HasFlag(ModifierKeys.Shift),
                    IsWinKeyUsed = Keyboard.Modifiers.HasFlag(ModifierKeys.Windows)
                };

            HotkeyEntryBox.Text = _hotkeyEntry.KeyBindingString;
            IsHotkeyEntered = true;
        }

        private void HotkeyList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ResetHotkeyEntry();

            PositionHotkey selectedHotkey = HotkeyList.SelectedItem as PositionHotkey;
            IsSelectedHotkeyValid = selectedHotkey != null && selectedHotkey.KeyCode != Keys.None;

            UpdateVisualizer(selectedHotkey);
        }

        private void UpdateVisualizer(PositionHotkey selectedHotkey)
        {
            if (Visualizer == null) return;

            switch (selectedHotkey.WindowPosition)
            {
                case WindowPosition.LeftOneThird:
                    LeftThird.Visibility = Visibility.Visible;
                    break;
                case WindowPosition.LeftHalf:
                    LeftHalf.Visibility = Visibility.Visible;
                    break;
                case WindowPosition.LeftTwoThirds:
                    LeftTwoThirds.Visibility = Visibility.Visible;
                    break;
                case WindowPosition.RightOneThird:
                    RightThird.Visibility = Visibility.Visible;
                    break;
                case WindowPosition.RightHalf:
                    RightHalf.Visibility = Visibility.Visible;
                    break;
                case WindowPosition.RightTwoThirds:
                    RightTwoThirds.Visibility = Visibility.Visible;
                    break;
                case WindowPosition.TopHalf:
                    TopHalf.Visibility = Visibility.Visible;
                    break;
                case WindowPosition.BottomHalf:
                    BottomHalf.Visibility = Visibility.Visible;
                    break;
                case WindowPosition.Center:
                    Center.Visibility = Visibility.Visible;
                    break;
                case WindowPosition.Fill:
                    Fill.Visibility = Visibility.Visible;
                    break;
            }
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
            selectedHotkey.KeyCode = _hotkeyEntry.KeyCode;
            selectedHotkey.IsCtrlKeyUsed = _hotkeyEntry.IsCtrlKeyUsed;
            selectedHotkey.IsAltKeyUsed = _hotkeyEntry.IsAltKeyUsed;
            selectedHotkey.IsShiftKeyUsed = _hotkeyEntry.IsShiftKeyUsed;
            selectedHotkey.IsWinKeyUsed = _hotkeyEntry.IsWinKeyUsed;

            IsSelectedHotkeyValid = false;
        }

        private void Preferences_OnClosing(object sender, CancelEventArgs e)
        {
            WindowPlacement.Instance.EnableHotkeys();
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

            // Clear selection rectangles:
            if (Visualizer != null)
            {
                foreach (Rectangle rectangle in Visualizer.Children)
                {
                    rectangle.Visibility = Visibility.Hidden;
                }
            }
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

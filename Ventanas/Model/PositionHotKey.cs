using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Base2io.Ventanas.Annotations;
using Base2io.Ventanas.Enums;

namespace Base2io.Ventanas.Model
{
    [Serializable]
    public class PositionHotkey : INotifyPropertyChanged
    {
        #region Properties

        private WindowPosition _windowPosition;
        public WindowPosition WindowPosition
        {
            get { return _windowPosition; }
            set
            {
                if (value != _windowPosition)
                {
                    _windowPosition = value;
                    OnPropertyChanged("WindowPosition");
                    OnPropertyChanged("KeyBindingString");
                }
            }
        }

        private Keys _keyCode;
        public Keys KeyCode
        {
            get { return _keyCode; }
            set
            {
                if (value != _keyCode)
                {
                    _keyCode = value;
                    OnPropertyChanged("KeyCode");
                    OnPropertyChanged("KeyBindingString");
                }
            }
        }

        private bool _isCtrlKeyUsed;
        public bool IsCtrlKeyUsed
        {
            get { return _isCtrlKeyUsed; }
            set
            {
                if (value != _isCtrlKeyUsed)
                {
                    _isCtrlKeyUsed = value;
                    OnPropertyChanged("IsCtrlKeyUsed");
                    OnPropertyChanged("KeyBindingString");
                }
            }
        }

        private bool _isAltKeyUsed;
        public bool IsAltKeyUsed
        {
            get { return _isAltKeyUsed; }
            set
            {
                if (value != _isAltKeyUsed)
                {
                    _isAltKeyUsed = value;
                    OnPropertyChanged("IsAltKeyUsed");
                    OnPropertyChanged("KeyBindingString");
                }
            }
        }

        private bool _isShiftKeyUsed;
        public bool IsShiftKeyUsed
        {
            get { return _isShiftKeyUsed; }
            set
            {
                if (value != _isShiftKeyUsed)
                {
                    _isShiftKeyUsed = value;
                    OnPropertyChanged("IsShiftKeyUsed");
                    OnPropertyChanged("KeyBindingString");
                }
            }
        }

        private bool _isWinKeyUsed;
        public bool IsWinKeyUsed
        {
            get { return _isWinKeyUsed; }
            set
            {
                if (value != _isWinKeyUsed)
                {
                    _isWinKeyUsed = value;
                    OnPropertyChanged("IsWinKeyUsed");
                    OnPropertyChanged("KeyBindingString");
                }
            }
        }

        public string KeyBindingString
        {
            get
            {
                List<string> keyCombination = new List<string>();

                if (IsCtrlKeyUsed)
                {
                    keyCombination.Add("Ctrl");
                }

                if (IsAltKeyUsed)
                {
                    keyCombination.Add("Alt");
                }

                if (IsShiftKeyUsed)
                {
                    keyCombination.Add("Shift");
                }

                if (IsWinKeyUsed)
                {
                    keyCombination.Add("Win");
                }

                keyCombination.Add(KeyCode.ToString());

                return string.Join("+", keyCombination);
            }
        }

        #endregion

        public void Clear()
        {
            KeyCode = Keys.None;
            IsCtrlKeyUsed = IsAltKeyUsed = IsShiftKeyUsed = IsWinKeyUsed = false;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

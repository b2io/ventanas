using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using Base2io.Ventanas.Model;

namespace Base2io.Ventanas.ValueConverters
{
    class PositionHotkeyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            PositionHotkey positionHotkey = value as PositionHotkey;

            if (positionHotkey == null)
            {
                return value;
            }

            List<string> keyCombination = new List<string>();

            if (positionHotkey.IsCtrlKeyUsed)
            {
                keyCombination.Add("Ctrl");
            }

            if (positionHotkey.IsAltKeyUsed)
            {
                keyCombination.Add("Alt");
            }

            if (positionHotkey.IsShiftKeyUsed)
            {
                keyCombination.Add("Shift");
            }

            if (positionHotkey.IsWinKeyUsed)
            {
                keyCombination.Add("Win");
            }

            keyCombination.Add(positionHotkey.KeyCode.ToString());

            return string.Join("+", keyCombination);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

using Microsoft.UI.Xaml.Data;
using System;
using System.Globalization;

namespace BMICalcWinUI.Converter
{
    public class FormattedStringConverter : IValueConverter
    {
        /// <summary>
        /// 変換してバインディング（バインディングソースが変換されるはず。つまり OneWay 方向）
        /// </summary>
        /// <param name="value">バインディングソースの値</param>
        /// <param name="targetType">バインディングターゲットのタイプ</param>
        /// <param name="parameter">Convert時の引数</param>
        /// <param name="culture">CultuerInfo、使用する暦とか、日付とか数値の書式がつまってる。</param>
        /// <returns>変換後の値</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            // ソースが書式変換できるやつで、
            // 書式が引数で指定されてて
            // ターゲットが文字列のとき
            if (value is IFormattable && parameter is string &&
                string.IsNullOrEmpty(parameter as string) == false &&
                targetType == typeof(string))
            {
                return (value as IFormattable).ToString(parameter as string, new CultureInfo(language));
            }
            return value;
        }
        /// <summary>
        /// 変換してバインディング（バインディングターゲットが変換されるはず。使ったことない！！）
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}

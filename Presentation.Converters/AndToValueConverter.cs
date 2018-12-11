﻿using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace PutridParrot.Presentation.Converters
{
    public abstract class AndToValueConverter<T> : MarkupExtension,
        IMultiValueConverter
    {
        public AndToValueConverter() :
            this(default(T), default(T))
        {
            // should be overriden is subclass to be
            // specific to the supported type
        }

        public AndToValueConverter(T whenTrue, T whenFalse)
        {
            WhenTrue = whenTrue;
            WhenFalse = whenFalse;
        }

        [ConstructorArgument("WhenTrue")]
        public T WhenTrue { get; set; }

        [ConstructorArgument("WhenFalse")]
        public T WhenFalse { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null)
                return DependencyProperty.UnsetValue;

            var booleans = values.Where(_ => _ is bool).ToArray();
            return booleans.All(_ => (bool)_) ? WhenTrue : WhenFalse;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
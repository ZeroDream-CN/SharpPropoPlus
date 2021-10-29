﻿using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace SharpPropoPlus.ExtensionMethods
{
    public static class ObjectExtensionMethods
    {
        public static string GetDescription(this object value)
        {
            return ((DescriptionAttribute) Attribute.GetCustomAttribute(
                       value.GetType().GetFields(BindingFlags.Public | BindingFlags.Static)
                           .Single(x => x.GetValue(null).Equals(value)),
                       typeof(DescriptionAttribute)))?.Description ?? value.ToString();
        }
    }
}
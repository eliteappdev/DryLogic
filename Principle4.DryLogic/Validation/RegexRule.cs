﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Principle4.DryLogic.Validation
{
  public class RegexRule : PropertyRule, IStaticErrorMessage
  {
    public string Pattern { get; private set; }
    public RegexRule(PropertyDefinition propertyDefinition, string pattern) : base(propertyDefinition)
    {
      Pattern = pattern;

      base.Assertion = oi =>
      {
        var stringValue = oi.GetUntypedValue(propertyDefinition).StringValue;
        if (stringValue == null)
        {
          return true;
        }

        return new Regex(pattern, RegexOptions.IgnoreCase).IsMatch(stringValue);
      };

      base.ErrorMessageGenerator = (oi) =>
        $"'{propertyDefinition.CurrentName}' must match the pattern {Pattern}.";
    }

		public string ErrorMessage
		{
			get
			{
				return ErrorMessageGenerator(null);
			}
		}
  }
}
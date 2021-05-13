using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEDOL_CaseStudy
{
    /// <summary>
    /// SEDOL Validation Result interface.
    /// </summary>
    public class SedolValidationResult : ISedolValidationResult
    {
        private string _InputString;

        private bool _IsValidSedol;

        private bool _IsUserDefined;

        private string _ValidationDetails;

        public string InputString { get => _InputString; }

        public bool IsValidSedol { get => _IsValidSedol; }

        public bool IsUserDefined { get => _IsUserDefined; }

        public string ValidationDetails { get => _ValidationDetails; }

        public SedolValidationResult(string inputString, bool isValidSedol, bool isUserDefined, string validationDetails)
        {
            _InputString = inputString;
            _IsValidSedol = isValidSedol;
            _IsUserDefined = isUserDefined;
            _ValidationDetails = validationDetails;
        }

    }

}
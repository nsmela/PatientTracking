using MudBlazor;
using System.Data;

namespace PatientTracking.Data.Patient {
    public class PatientTaskCalculation : IPatientTask {
        public object Type => typeof(double);
        public PatientTaskGroup Parent { get; set; }
        public string Icon => Icons.Material.Filled.TextSnippet;
        public string Label { get; set; }
        public string Formula { get; set; }
        public void SetValue(object value) {
            if (value is null) return;
            if (value.GetType() != typeof(string)) return;
            Formula = (string)value;
        }

        //subscribes to OnValueChanged on referenced number tasks 

        //convert string into a formula using DataTable
        // example: 3 + 4 * 7 / 2 - 1
        //recursively break string into groups of operators
        //multiplication and division first, then addition and subtraction
        // example: ((3 + ((4 * 7) / 2)) - 1)
        //          ((3 + ([group1] / 2)) - 1)  | group1 = 4 * 7
        //          ((3 + [group2]) - 1)        | group2 = group1 / 7
        //          ([group3] - 1)              | group3 = group2 + 3
        //          [group4]                    | group4 = group3 - 1
        //operators

        public string Calculate() {
            var dt = new DataTable();
            var result = dt.Compute(Formula, "");
            return result.ToString();
        }
    }
}

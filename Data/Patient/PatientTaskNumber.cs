using MudBlazor;

namespace PatientTracking.Data.Patient {
    public class PatientTaskNumber : IPatientTask {
        public static object Type => typeof(double);
        public static string Icon => Icons.Material.Filled.TextSnippet;
        public string Label { get; set; }
        public double Value { get; set; }
        public void SetValue(object value) {
            if (value is null) return;
            if (value.GetType() != typeof(double)) return;
            Value = (double)value;
        }
    }
}

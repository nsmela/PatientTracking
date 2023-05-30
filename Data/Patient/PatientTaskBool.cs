using MudBlazor;

namespace PatientTracking.Data.Patient {
    public class PatientTaskBool : IPatientTask {
        public static object Type => typeof(bool);
        public static string Icon => Icons.Material.Filled.CheckBox;
        public string Label { get; set; }
        public bool Checked { get; set; }



        public void SetValue(object value) {
            if (value is null) return;
            if (value.GetType() != typeof(bool)) return;
            Checked = (bool)value;
        }
    }
}

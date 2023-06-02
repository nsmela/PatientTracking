using MudBlazor;

namespace PatientTracking.Data.Patient {
    public class PatientTaskBool : IPatientTask {
        public object Type => typeof(bool);
        public PatientTaskGroup Parent { get; set; }
        public string Icon => Icons.Material.Filled.CheckBox;
        public string Label { get; set; }
        public bool Checked { get; set; } = false;
        public void SetValue(object value) {
            if (value is null) return;
            if (value.GetType() != typeof(bool)) return;
            Checked = (bool)value;
        }
    }
}

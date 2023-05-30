using MudBlazor;

namespace PatientTracking.Data.Patient {
    public class PatientTaskList : IPatientTask {
        public static object Type => typeof(List<string>);
        public static string Icon => Icons.Material.Filled.List;
        public string Label { get; set; }
        public List<string> Options { get; set; }
        public int SelectedOption { get; set; }
        public void SetValue(object value) {
            if (value is null) return;
            if (value.GetType() != typeof(int)) return;
            SelectedOption = (int)value;
        }
    }
}

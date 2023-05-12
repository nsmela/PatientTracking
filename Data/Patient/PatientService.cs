namespace PatientTracking.Data.Patient {
    public class PatientService {
        private List<Patient> patients = new List<Patient> {
            new Patient{
                FirstName = "Alex",
                LastName = "Trebek",
                Id = "01122",
                StartDate = new DateTime(2023, 01, 01),
                Status = 3,
                TaskGroups = new List<PatientTaskGroup> {
                    new PatientTaskGroup {Label = "Standard",
                        Tasks = new List<IPatientTask> { new PatientTaskBool{Label = "Requisition", Checked = true}, new PatientTaskText { Label = "Site", Text = "" } } },
                    new PatientTaskGroup {Label = "Setup",
                        Tasks = new List<IPatientTask> { new PatientTaskBool{Label = "SSD", Checked = true}, new PatientTaskBool{Label = "Tattoos" }, new PatientTaskBool{Label = "Tolerance tables" } } },
                    new PatientTaskGroup {Label = "Dose Distribution",
                        Tasks = new List<IPatientTask> { new PatientTaskBool{Label = "Calculation volume", Checked = true}, new PatientTaskBool{Label = "Grid Size", Checked = true}, new PatientTaskBool{Label = "Algorithm", Checked = false} } }
                } },
            new Patient{
                FirstName = "Vanna",
                LastName = "White",
                Id = "01123",
                StartDate = new DateTime(2023, 11, 01),
                Status = 0,
                TaskGroups = new List<PatientTaskGroup> {
                    new PatientTaskGroup {Label = "Standard",
                        Tasks = new List<IPatientTask> { new PatientTaskBool{Label = "Requisition", Checked = true}, new PatientTaskText { Label = "Site", Text = "" } } },
                    new PatientTaskGroup {Label = "Setup",
                        Tasks = new List<IPatientTask> { new PatientTaskBool{Label = "SSD", Checked = true}, new PatientTaskBool{Label = "Tattoos" } } },
                    new PatientTaskGroup {Label = "Dose Distribution",
                        Tasks = new List<IPatientTask> { new PatientTaskBool{Label = "Calculation volume", Checked = true}, new PatientTaskBool{Label = "Algorithm", Checked = false} } }
                }},
            new Patient{
                FirstName = "Drew",
                LastName = "Carey",
                Id = "01124",
                StartDate = new DateTime(2023, 04, 11),
                Status = 2,
                TaskGroups = new List<PatientTaskGroup> {
                    new PatientTaskGroup {Label = "Standard",
                        Tasks = new List<IPatientTask> { new PatientTaskBool{Label = "Requisition", Checked = true}, new PatientTaskText { Label = "Site", Text = "" } } },
                    new PatientTaskGroup {Label = "Setup",
                        Tasks = new List<IPatientTask> { new PatientTaskBool{Label = "SSD", Checked = true}, new PatientTaskBool{Label = "Tattoos" }, new PatientTaskBool{Label = "Tolerance tables" } } },
                    new PatientTaskGroup {Label = "Dose Distribution",
                        Tasks = new List<IPatientTask> { new PatientTaskBool{Label = "Calculation volume", Checked = true}, new PatientTaskText{Label = "Dose Plan", Text="min_50gy_01"  } } }
            } }
        };

        public Task<Patient[]> GetPatientsAsync() {
            return Task.FromResult(patients.ToArray());
        }

        public Task<Patient> GetPatientByIdAsync(string id) {
            var index = patients.FindIndex(p => p.Id == id);
            var result = patients.First(p => p.Id == id);
            if (index < 0) return null;
            return Task.FromResult(patients[index]);
        }

        public async Task UpdatePatient(Patient patient) {
            var newPatientInfo = patients.FindIndex(p => p.Id == patient.Id);
            if (newPatientInfo >= 0) {
                patients[newPatientInfo] = patient;
            }
        }

        public async Task AddPatient(Patient patient) {
            patients.Add(patient);
        }

        public async Task RemovePatient(string id) {
            var result = patients.Find(p => p.Id == id);
            if (result is not null) {
                patients.Remove(result);
            }
        }


        //templates
        private List<Patient> _patientTemplates = new();

        public async Task AddPatientTemplate(Patient patient) => _patientTemplates.Add(patient);
        public Task<Patient[]> GetPatientTemplates() => Task.FromResult(_patientTemplates.ToArray());
        public Task<Patient> GetPatientTemplate(int index) => Task.FromResult(_patientTemplates[index]);
        public async Task UpdatePatientTemplate(int index, Patient patient) => _patientTemplates[index] = patient;
        public async Task RemovePatientTemplate(Patient patient)=> _patientTemplates.Remove(patient);
        

        private List<PatientTaskGroup> _taskTemplates = new();

        public async Task AddTaskTemplate(PatientTaskGroup group) => _taskTemplates.Add(group);
        public Task<PatientTaskGroup[]> GetTaskTemplates() => Task.FromResult(_taskTemplates.ToArray());
        public Task<PatientTaskGroup> GetTaskTemplate(int index) => Task.FromResult(_taskTemplates[index]);
        public async Task UpdateTaskTemplate(int index, PatientTaskGroup group) => _taskTemplates[index] = group;
        public async Task RemoveTaskTemplate(PatientTaskGroup group) => _taskTemplates.Remove(group);
    }
    }

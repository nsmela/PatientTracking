using PatientTracking.Data.Templates;

namespace PatientTracking.Data.Patient {
    public class PatientService {
        private List<Patient> patients = new List<Patient> {
            new Patient("001", "Alexander", "Trebek", new List<PatientTaskGroup> {
                    new PatientTaskGroup("Standard", new List<IPatientTask> { 
                            new PatientTaskDate{Label = "Due Date:", Date=DateTime.Today.AddDays(1)},
                            new PatientTaskBool{Label = "Requisition", Checked = true},
                            new PatientTaskBool{Label = "Approved", Checked = false},
                            new PatientTaskBool{Label = "New", Checked = true},
                            new PatientTaskText { Label = "Site", Text = "Lungs" },
                            new PatientTaskNumber {Label = "Site Volume:", Value=1.023f},
                            new PatientTaskList {Label = "Progress Status", SelectedOption=0, Options = new List<string>{"Admitted", "Onc Review Complete", "Phys Review Complete", "Ready for treatment"}}
                        }) ,
                    new PatientTaskGroup( "Setup", new List<IPatientTask> { 
                        new PatientTaskBool{Label = "SSD", Checked = true}, new PatientTaskBool{Label = "Tattoos" }, new PatientTaskBool{Label = "Tolerance tables" } } ),
                    new PatientTaskGroup ("Dose Distribution", new List<IPatientTask> { 
                        new PatientTaskBool{Label = "Calculation volume", Checked = true}, new PatientTaskBool{Label = "Grid Size", Checked = true}, new PatientTaskBool{Label = "Algorithm", Checked = false} } )
                }) ,
            new Patient("002", "Vanna", "White", new List<PatientTaskGroup> {
                    new PatientTaskGroup ("Standard", new List<IPatientTask> { 
                        new PatientTaskBool{Label = "Requisition", Checked = true}, new PatientTaskText { Label = "Site", Text = "" } } ),
                    new PatientTaskGroup ("Setup", new List<IPatientTask> { 
                        new PatientTaskBool{Label = "SSD", Checked = true}, new PatientTaskBool{Label = "Tattoos" } } ),
                    new PatientTaskGroup ("Dose Distribution", new List<IPatientTask> { 
                        new PatientTaskBool{Label = "Calculation volume", Checked = true}, new PatientTaskBool{Label = "Algorithm", Checked = false} } )
                }),
            new Patient("003", "Drew", "Carey", new List<PatientTaskGroup> {
                    new PatientTaskGroup ("Standard", new List<IPatientTask> {
                        new PatientTaskBool{Label = "Requisition", Checked = true}, new PatientTaskText { Label = "Site", Text = "" } } ),
                    new PatientTaskGroup ("Setup", new List<IPatientTask> {
                        new PatientTaskBool{Label = "SSD", Checked = true}, new PatientTaskBool{Label = "Tattoos" } } ),
                    new PatientTaskGroup ("Dose Distribution", new List<IPatientTask> {
                        new PatientTaskBool{Label = "Calculation volume", Checked = false}, new PatientTaskBool{Label = "Algorithm", Checked = true} } )
                })
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
        private List<TaskGroupsTemplate> _patientTemplates;
        private void InitializeGroups() {
            _patientTemplates = new List<TaskGroupsTemplate> {
                new TaskGroupsTemplate(0, "Standard Patient", new List<PatientTaskGroup>{ _taskTemplates[0].Tasks, _taskTemplates[1].Tasks }),
                new TaskGroupsTemplate(0, "Brachy Patient", new List<PatientTaskGroup>{ _taskTemplates[0].Tasks, _taskTemplates[2].Tasks }),
            };
        }

        public async Task AddPatientTemplate(string label, List<PatientTaskGroup> taskGroups) {
            if (_patientTemplates == null) InitializeGroups();
            int count = _patientTemplates.Count();
            TaskGroupsTemplate template = new TaskGroupsTemplate(count, label, taskGroups);
            _patientTemplates.Add(template); 
        }
        public Task<TaskGroupsTemplate[]> GetPatientTemplates() {
            if (_patientTemplates == null) InitializeGroups();
            return Task.FromResult(_patientTemplates.ToArray());
        }

        public Task<TaskGroupsTemplate> GetPatientTemplate(int id) {
            if (_patientTemplates == null) InitializeGroups();
            return Task.FromResult(_patientTemplates.Find(pt => pt.Id == id));
        }

        public async Task UpdatePatientTemplate(TaskGroupsTemplate template) {
            if (_patientTemplates == null) InitializeGroups();
            int index = _patientTemplates.IndexOf(template);
            _patientTemplates[index] = template;
        }
        public async Task RemovePatientTemplate(TaskGroupsTemplate template) {
            if (_patientTemplates == null) InitializeGroups();
            _patientTemplates.Remove(template);
            for(int i = 0; i < _patientTemplates.Count(); i++) _patientTemplates[i].Id = i;
        }


        private List<TasksTemplate> _taskTemplates = new List<TasksTemplate> {
            new TasksTemplate(0, "Patient Machine Checks", new PatientTaskGroup("Patient Machine Checks", new List<IPatientTask> {
                new PatientTaskText {Label = "Plan Name"},
                new PatientTaskBool {Label = "Dry Run Test"},
                new PatientTaskBool {Label = "VMAT QA Completed"}
            })),
            new TasksTemplate(1, "Plan Calculations", new PatientTaskGroup( "Plan Calculations", new List<IPatientTask> {
                new PatientTaskText {Label = "Region of Interest (ROI)"},
                new PatientTaskText {Label = "Plan Strategy"},
                new PatientTaskBool {Label = "Oncologist Approved"},
                new PatientTaskBool {Label = "Physics Approved"}
            })),
            new TasksTemplate(2, "Brachytherapy Checklist", new PatientTaskGroup("Brachytherapy Checklist", new List<IPatientTask> {
                new PatientTaskDate {Label = "Plan Start"},
                new PatientTaskText {Label = "Oncologist Review Parameters"},
                new PatientTaskBool {Label = "Radiation Theraptists Consulted"},
                new PatientTaskBool {Label = "Seed Size Comparison Reviewed"}
            }))
            
        };

        public async Task AddTaskTemplate(PatientTaskGroup group) {
            int count = _taskTemplates.Count();
            TasksTemplate tasks = new TasksTemplate(count, group.Label, group);
            _taskTemplates.Add(tasks);
        }
        public Task<TasksTemplate[]> GetTaskTemplates() => Task.FromResult(_taskTemplates.ToArray());
        public Task<TasksTemplate> GetTaskTemplate(int id) => Task.FromResult(_taskTemplates.Find(t => t.Id == id));
        public async Task UpdateTaskTemplate(TasksTemplate template) {
            int index = _taskTemplates.IndexOf(template);
            _taskTemplates[index] = template;
        }
        public async Task RemoveTaskTemplate(TasksTemplate template) {
            _taskTemplates.Remove(template);
            for (int i = 0; i < _taskTemplates.Count(); i++) _taskTemplates[i].Id = i;
        }
    }
    }

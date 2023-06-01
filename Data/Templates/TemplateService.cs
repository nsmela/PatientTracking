using PatientTracking.Data.Patient;

namespace PatientTracking.Data.Templates {
    public class TaskItem {
        public string Label { get; set; }
        public Object Type { get; set; } = typeof(bool);
        public Object Value {get; set; } = null;
        public TaskGroup Parent { get; set; }
        public TaskItem(string label, Object type, TaskGroup parent = null, Object value = null) {
            Label = label;
            Type = type;
            Parent = parent;
            Value = value;
        }
        public TaskItem(TaskItem copy) {
            Label = copy.Label;
            Type = copy.Type;
            Parent = copy.Parent;
            Value = copy.Value;
        }
    }

    public class TaskGroup {
        public int? Id { get; set; }
        public string Label { get; set; }
        public List<TaskItem> Tasks { get; set; }
        public int EditableTaskIndex { get; set; } = -1;
        public TemplateStatus Status { get; set; } = TemplateStatus.Active;
        public TemplateItem Parent { get; set; }
        public TaskGroup(string label, List<TaskItem> tasks, int? id = null, TemplateItem parent = null) {
            Id = id;
            Label = label;
            Parent = parent;

            Tasks = tasks;
            foreach(var task in Tasks) task.Parent = this;
            
        }

        public TaskGroup(string label) {
            Label = label;
            Tasks = new();
            Id = null;
            Parent = null;
        }

        public TaskGroup Copy() {
            var group = new TaskGroup(this.Label, new(), this.Id);
            group.Parent = null;
            Tasks.ForEach(t => group.Tasks.Add(new TaskItem(t.Label, t.Type, group)));

            return group;
        }
    }

    public class TemplateItem {
        public int Id { get; set; }
        public string Label { get; set; }
        public List<TaskGroup> Groups { get; set; }
        public int EditableGroupIndex { get; set; } = -1;
        public TemplateStatus Status { get; set; } = TemplateStatus.Active; //used to allow children to delete themselves
        public TemplateItem(int id, string label, List<TaskGroup> groups) {
            Id = id;
            Label = label;
            Groups = groups;
            foreach (var group in groups) group.Parent = this;
        }

        public TemplateItem(TemplateItem copy) {
            Id = copy.Id;
            Label = copy.Label;
            EditableGroupIndex = copy.EditableGroupIndex;
            Status = copy.Status;

            Groups = new List<TaskGroup>();
            foreach (var group in copy.Groups) {
                var g = group.Copy();
                g.Parent = this;
                Groups.Add(g);
            }
            
        }
    }

    public enum TemplateStatus { New, Active, Retired, Delete }

    public class TemplateService {
        private List<TemplateItem> _templates { get; set; }
        private List<TaskGroup> _groupTemplates { get; set; }

        public TemplateService() {
            _groupTemplates = new List<TaskGroup>();

            _groupTemplates.Add(new TaskGroup("General Tasks", new List<TaskItem> {
                new TaskItem("Due By:", typeof(DateTime)),
                new TaskItem("Assigned to: ", typeof(string)),
                new TaskItem("Approved by Oncologist", typeof(bool)),
                new TaskItem("Total Volume (mL):", typeof(double)),
                new TaskItem("Expected MU:", typeof(PatientTaskCalculation)),
                new TaskItem("Status", typeof(List<string>), null, new List<string> { "Arrived", "Onc Eval", "Physics Eval", "Treat Ready", "Treated"})
            }));

            _groupTemplates.Add(new TaskGroup("Physics Planning", new List<TaskItem> {
                new TaskItem("Due By:", typeof(DateTime)),
                new TaskItem("Assigned to: ", typeof(string)),
                new TaskItem("Dry run complete", typeof(bool)),
                new TaskItem("VMAT QA", typeof(bool)),
                new TaskItem("Approved", typeof(bool))
            }));

            _groupTemplates.Add(new TaskGroup("Brachytherapy", new List<TaskItem> {
                new TaskItem("Assigned to: ", typeof(string)),
                new TaskItem("Seed Supply Checked", typeof(bool)),
                new TaskItem("Seed Exchange", typeof(DateTime))
            }));

            _templates = new List<TemplateItem>();
            _templates.Add(new TemplateItem(0, "Standard Patient", new List<TaskGroup> {
                _groupTemplates[0].Copy(), _groupTemplates[1].Copy() }));
            _templates.Add(new TemplateItem(1, "Brachy Patient", new List<TaskGroup> {
                _groupTemplates[0].Copy(), _groupTemplates[2].Copy() }));

        }

        public async Task<List<TemplateItem>> GetAllTemplates() => _templates;
        public async Task<List<TaskGroup>> GetAllGroupTemplates() => _groupTemplates.ToList();
        public async Task<TemplateItem> GetTemplate(int index) => _templates[index];
        public async Task<TaskGroup> GetGroup(int index) => _groupTemplates.FirstOrDefault(x => x.Id == index);
        public async Task AddTemplate(TemplateItem template) => _templates.Add(template);
        public async Task AddGroup(TaskGroup group) {
            group.Id = _groupTemplates.Count;
            _groupTemplates.Add(group);
        }
        public async Task UpdateTemplate(TemplateItem template) {
            _templates[template.Id] = template;
        }
        public async Task UpdateGroup(TaskGroup group) {
            _groupTemplates[(int)group.Id] = group;
        }
        public async Task RemoveTemplate(int index) {
            _templates.RemoveAt((int)index);
        }
        public async Task RemoveGroup(int? index) {
            if (index is null || index < 0) return;
            foreach (var template in _templates) {
                foreach (var group in template.Groups) {
                    if (group.Id == index) {
                        template.Groups.Remove(group);
                        break;
                    }
                }
            }
            _groupTemplates.RemoveAt((int)index);
        }
    }
}

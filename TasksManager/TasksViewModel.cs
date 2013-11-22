using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.ComponentModel;
using System.ServiceModel.DomainServices.Client;
using TasksManager.Web;
using TasksManager;

namespace TasksManager
{
    public class TasksViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        DateTime _LowerSearchDate;
        public DateTime LowerSearchDate
        {
            get
            {
                return _LowerSearchDate;
            }
            set
            {
                if (value != _LowerSearchDate)
                {
                    _LowerSearchDate = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("LowerSearchDate"));
                }
            }
        }

        DateTime _UpperSearchDate;
        public DateTime UpperSearchDate
        {
            get
            {
                return _UpperSearchDate;
            }
            set
            {
                if (value != _UpperSearchDate)
                {
                    _UpperSearchDate = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("UpperSearchDate"));
                }
            }
        }

        public ICommand SearchByDateCommand { get; private set; }
        public ICommand AddTaskCommand { get; private set; }
        public ICommand SaveChangesCommand { get; private set; }
        public IEnumerable<Task> Tasks { get; private set; }


        TasksDomainContext _Context = new TasksDomainContext();

        public TasksViewModel()
        {
            SearchByDateCommand = new RelayCommand<object>(OnSearchByDate);
            AddTaskCommand = new RelayCommand<object>(OnAddTask);
            SaveChangesCommand = new RelayCommand<object>(OnSaveChanges);
            Tasks = _Context.Tasks;
            if (!DesignerProperties.IsInDesignTool)
            {
                _Context.Load(_Context.GetTasksQuery());
            }
        }

        private void OnSearchByDate(object param)
        {
            _Context.Tasks.Clear();
            EntityQuery<Task> query = _Context.GetTasksQuery();
            LoadOperation<Task> loadOp = _Context.Load(query.Where(t => t.StartDate >= LowerSearchDate && t.StartDate <= UpperSearchDate));
        }

        private void OnAddTask(object param)
        {
            // Generally don't want to do this for testability reasons
            // Simplification because MVVM structuring is not the focus here
            // See Prism 4 MVVM RI for a cleaner way to do this
            AddTaskView popup = new AddTaskView();
            popup.DataContext = new Task();
            popup.Closed += delegate
            {
                if (popup.DialogResult == true)
                {
                    Task newTask = popup.DataContext as Task;
                    if (newTask != null) _Context.Tasks.Add(newTask);
                }
            };
            popup.Show();
        }

        private void OnSaveChanges(object param)
        {
            _Context.SubmitChanges();
        }

    }
}

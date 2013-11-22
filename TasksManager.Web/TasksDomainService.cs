
namespace TasksManager.Web
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    using System.Linq;
    using System.ServiceModel.DomainServices.EntityFramework;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;


    // Реализует логику приложения с использованием контекста TaskManagerEntities.
    // TODO: добавьте свою прикладную логику в эти или другие методы.
    // TODO: включите проверку подлинности (Windows/ASP.NET Forms) и раскомментируйте следующие строки, чтобы запретить анонимный доступ
    // Кроме того, рассмотрите возможность добавления ролей для соответствующего ограничения доступа.
    // [RequiresAuthentication]
    [EnableClientAccess()]
    public class TasksDomainService : LinqToEntitiesDomainService<TaskManagerEntities>
    {

        // TODO:
        // рассмотрите возможность сокращения результатов метода запроса.  Если необходим дополнительный ввод,
        // то в этот метод можно добавить параметры или создать дополнительные методы выполнения запроса с другими именами.
        // Для поддержки разбиения на страницы добавьте упорядочение в запрос "Tasks".
        public IQueryable<Task> GetTasks()
        {
            return this.ObjectContext.Tasks;
        }

        public Task GetTask(int taskId)
        {
            return this.ObjectContext.Tasks.FirstOrDefault(t => t.TaskId == taskId);
        }

        public IQueryable<Task> GetTasksByStartDate(
        DateTime lowerDateTimeInclusive,
        DateTime upperDateTimeInclusive)
        {
            return this.ObjectContext.Tasks.Where(
                t => t.StartDate >= lowerDateTimeInclusive && t.StartDate <= upperDateTimeInclusive);
        }

        public void InsertTask(Task task)
        {
            if ((task.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(task, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Tasks.AddObject(task);
            }
        }

        public void UpdateTask(Task currentTask)
        {
            this.ObjectContext.Tasks.AttachAsModified(currentTask, this.ChangeSet.GetOriginal(currentTask));
        }

        public void DeleteTask(Task task)
        {
            if ((task.EntityState == EntityState.Detached))
            {
                this.ObjectContext.Tasks.Attach(task);
            }
            this.ObjectContext.Tasks.DeleteObject(task);
        }
    }
}



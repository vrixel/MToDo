using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Mosaic_Todo.Models;

namespace Mosaic_Todo.Models
{
    public interface IToDoCompletable
    {
        void Complete();
    }

    public interface IToDoDeletable
    {
        void Delete();
    }

    public interface IToDoInterface : IToDoCompletable, IToDoDeletable
    {
        int Id { get; set; }
        string Description { get; set; }
        bool Completed { get; set; }
    }

    public class ToDo : IToDoInterface
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool Completed { get; set; }

        public void Complete()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }
    }

    public class ToDoDbContext : DbContext
    {
        public DbSet<ToDo> ToDoes { get; set; }
    }

    public interface IToDoRepositoryInterface
    {
        List<IToDoInterface> GetAll();
        void Add(IToDoInterface toDo);
        void Delete(int toDoId);
        void Complete(int toDoId);
    }

    public class ToDoRepository : IToDoRepositoryInterface
    {
        private List<IToDoInterface> _toDoes;

        public ToDoRepository()
        {
            this._toDoes = new List<IToDoInterface>();

            ToDo defaultToDo = new ToDo();
            defaultToDo.Id = 1;
            defaultToDo.Description = "Default Task";
            defaultToDo.Completed = false;
            this._toDoes.Add(defaultToDo);
        }

        public List<IToDoInterface> GetAll()
        {
            return this._toDoes;
        }

        public void Add(IToDoInterface toDo)
        {
            toDo.Id = this._toDoes.Max(x => x.Id) + 1;
            this._toDoes.Add(toDo);
        }

        public void Delete(int toDoId)
        {
            this._toDoes.Remove(this._toDoes.Where(x => x.Id == toDoId).First());
            //Re-index
            this._toDoes.ForEach((x) => { if (x.Id > toDoId) x.Id = x.Id - 1; });
        }

        public void Complete(int toDoId)
        {
            throw new NotImplementedException();
        }

        
    }


}
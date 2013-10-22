using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

// Directive for the data model.
using banco_de_dados_local.Model;


namespace banco_de_dados_local.ViewModel
{
    public class ToDoViewModel : INotifyPropertyChanged
    {

        
        // Write changes in the data context to the database.
        public void SaveChangesToDB()
        {
            UFABC_Power_CR.App.db.SubmitChanges();
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify the app that a property has changed.
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion



        //Matérias
        private ObservableCollection<Historico> _materiasNoQuad;
        public ObservableCollection<Historico> MateriasNoQuad
        {
            get { return _materiasNoQuad; }
            set
            {
                _materiasNoQuad = value;
                NotifyPropertyChanged("MateriasNoQuad");
            }
        }

        
        //Items do dia
        private ObservableCollection<ToDoItem> _hojeItems;
        public ObservableCollection<ToDoItem> HojeItems
        {
            get { return _hojeItems; }
            set
            {
                _hojeItems = value;
                NotifyPropertyChanged("HojeItems");
            }
        }

        // All to-do items.
        private ObservableCollection<ToDoItem> _todosItems;
        public ObservableCollection<ToDoItem> TodosItems
        {
            get { return _todosItems; }
            set
            {
                _todosItems = value;
                NotifyPropertyChanged("TodosItems");
            }
        }

        // To-do items associated with the home category.
        private ObservableCollection<ToDoItem> _exItems;
        public ObservableCollection<ToDoItem> ExItems
        {
            get { return _exItems; }
            set
            {
                _exItems = value;
                NotifyPropertyChanged("ExItems");
            }
        }

        // To-do items associated with the work category.
        private ObservableCollection<ToDoItem> _trabItems;
        public ObservableCollection<ToDoItem> TrabItems
        {
            get { return _trabItems; }
            set
            {
                _trabItems = value;
                NotifyPropertyChanged("TrabItems");
            }
        }

        // To-do items associated with the hobbies category.
        private ObservableCollection<ToDoItem> _provasItems;
        public ObservableCollection<ToDoItem> ProvasItems
        {
            get { return _provasItems; }
            set
            {
                _provasItems = value;
                NotifyPropertyChanged("ProvasItems");
            }
        }


        // To-do items associated with the hobbies category.
        private ObservableCollection<ToDoItem> _outrosItems;
        public ObservableCollection<ToDoItem> OutrosItems
        {
            get { return _outrosItems; }
            set
            {
                _outrosItems = value;
                NotifyPropertyChanged("OutrosItems");
            }
        }



        // horários hoje.
        private ObservableCollection<Grade> _horariosHoje;
        public ObservableCollection<Grade> HorariosHoje
        {
            get { return _horariosHoje; }
            set
            {
                _horariosHoje = value;
                NotifyPropertyChanged("HorariosHoje");
            }
        }

        // horários segunda.
        private ObservableCollection<Grade> _horariosSeg;
        public ObservableCollection<Grade> HorariosSeg
        {
            get { return _horariosSeg; }
            set
            {
                _horariosSeg = value;
                NotifyPropertyChanged("HorariosSeg");
            }
        }


        // horários Terça.
        private ObservableCollection<Grade> _horariosTer;
        public ObservableCollection<Grade> HorariosTer
        {
            get { return _horariosTer; }
            set
            {
                _horariosTer = value;
                NotifyPropertyChanged("HorariosTer");
            }
        }


        // horários Quarta.
        private ObservableCollection<Grade> _horariosQua;
        public ObservableCollection<Grade> HorariosQua
        {
            get { return _horariosQua; }
            set
            {
                _horariosQua = value;
                NotifyPropertyChanged("HorariosQua");
            }
        }

        // horários Quinta.
        private ObservableCollection<Grade> _horariosQui;
        public ObservableCollection<Grade> HorariosQui
        {
            get { return _horariosQui; }
            set
            {
                _horariosQui = value;
                NotifyPropertyChanged("HorariosQui");
            }
        }

        // horários Sexta.
        private ObservableCollection<Grade> _horariosSex;
        public ObservableCollection<Grade> HorariosSex
        {
            get { return _horariosSex; }
            set
            {
                _horariosSex = value;
                NotifyPropertyChanged("HorariosSex");
            }
        }


        // horários Sabado.
        private ObservableCollection<Grade> _horariosSab;
        public ObservableCollection<Grade> HorariosSab
        {
            get { return _horariosSab; }
            set
            {
                _horariosSab = value;
                NotifyPropertyChanged("HorariosSab");
            }
        }


        // horários Domingo.
        private ObservableCollection<Grade> _horariosDom;
        public ObservableCollection<Grade> HorariosDom
        {
            get { return _horariosDom; }
            set
            {
                _horariosDom = value;
                NotifyPropertyChanged("HorariosDom");
            }
        }

        // A list of all categories, used by the add task page.
        private List<ToDoCategory> _categoriesList;
        public List<ToDoCategory> CategoriesList
        {
            get { return _categoriesList; }
            set
            {
                _categoriesList = value;
                NotifyPropertyChanged("CategoriesList");
            }
        }






        // Query database and load the collections and list used by the pivot pages.
        public void LoadCollectionsFromDatabase()
        {

            //query para items hoje
            var itemsHojeInDB = from ToDoItem todo in UFABC_Power_CR.App.db.Items where todo.ItemData.Equals(System.DateTime.Now.ToShortDateString()) ||
                                todo.ItemData.Equals(System.DateTime.Now.AddDays(1).ToShortDateString()) ||
                                todo.ItemData.Equals(System.DateTime.Now.AddDays(2).ToShortDateString()) ||
                                todo.ItemData.Equals(System.DateTime.Now.AddDays(3).ToShortDateString()) ||
                                todo.ItemData.Equals(System.DateTime.Now.AddDays(4).ToShortDateString()) ||
                                todo.ItemData.Equals(System.DateTime.Now.AddDays(5).ToShortDateString()) ||
                                todo.ItemData.Equals(System.DateTime.Now.AddDays(6).ToShortDateString()) ||
                                todo.ItemData.Equals(System.DateTime.Now.AddDays(7).ToShortDateString())
                                select todo;

            // Specify the query for all to-do items in the database.
            var toDoItemsInDB = from ToDoItem todo in UFABC_Power_CR.App.db.Items
                                select todo;


            //matérias quadri
            var AlunoInDb = from Aluno aluno in UFABC_Power_CR.App.db.Aluno select aluno;
            foreach(Aluno aluno in AlunoInDb)
            {
                var materiaQuadInDB = from Historico hist in UFABC_Power_CR.App.db.Historicos where hist.Quadrimestre == aluno.QuadriAtual select hist;
                MateriasNoQuad = new ObservableCollection<Historico>(materiaQuadInDB);

            }

            //horários de hoje
            var horaHojeInDb = from Grade horarios in UFABC_Power_CR.App.db.Grades where horarios.Dias.Contains(System.DateTime.Now.DayOfWeek.ToString()) select horarios;
            //seleciona horarios que contem segunda
            var horaSegInDb = from Grade horarios in UFABC_Power_CR.App.db.Grades where horarios.Dias.Contains("Monday") select horarios;
            //terça
            var horaTerInDb = from Grade horarios in UFABC_Power_CR.App.db.Grades where horarios.Dias.Contains("Tuesday") select horarios;
            //quarta
            var horaQuaInDb = from Grade horarios in UFABC_Power_CR.App.db.Grades where horarios.Dias.Contains("Wednesday") select horarios;
            //quinta
            var horaQuiInDb = from Grade horarios in UFABC_Power_CR.App.db.Grades where horarios.Dias.Contains("Thursday") select horarios;
            //sexta
            var horaSexInDb = from Grade horarios in UFABC_Power_CR.App.db.Grades where horarios.Dias.Contains("Friday") select horarios;
            //sábado
            var horaSabInDb = from Grade horarios in UFABC_Power_CR.App.db.Grades where horarios.Dias.Contains("Saturday") select horarios;
            //domingo
            var horaDomInDb = from Grade horarios in UFABC_Power_CR.App.db.Grades where horarios.Dias.Contains("Sunday") select horarios;





            List<ToDoItem> aux = new List<ToDoItem>();
            
            //carrega items hoje
            foreach (ToDoItem item in itemsHojeInDB)
            {
                aux.Add(item);
            }
            HojeItems = new ObservableCollection<ToDoItem>(aux.OrderBy(z=> System.DateTime.Parse(z.ItemData)));
            aux.Clear();



            // Query the database and load all to-do items.
            
            TodosItems = new ObservableCollection<ToDoItem>(toDoItemsInDB);
            aux.Clear();

            // Specify the query for all categories in the database.
            var toDoCategoriesInDB = from ToDoCategory category in UFABC_Power_CR.App.db.Categories
                                     select category;


            // Query the database and load all associated items to their respective collections.
            foreach (ToDoCategory category in toDoCategoriesInDB)
            {
                switch (category.Name)
                {
                    case "Exercicio":
                        ExItems = new ObservableCollection<ToDoItem>(category.ToDos);
                        break;
                    case "Trabalho":
                        TrabItems = new ObservableCollection<ToDoItem>(category.ToDos);
                        break;
                    case "Prova":
                        ProvasItems = new ObservableCollection<ToDoItem>(category.ToDos);
                        break;
                    case "Outros":
                        OutrosItems = new ObservableCollection<ToDoItem>(category.ToDos);
                        break;
                    default:
                        break;
                }
            }

            List<Grade> a = new List<Grade>();
            //hoje
            foreach (Grade hora in horaHojeInDb)
            {
                a.Add(hora);
            }
            HorariosHoje = new ObservableCollection<Grade>(a.OrderBy(x => System.DateTime.Parse(x.HoraInicio)));
            a.Clear();
            //segunda
            foreach (Grade hora in horaSegInDb)
            {
                a.Add(hora);
            }
            
            HorariosSeg = new ObservableCollection<Grade>(a.OrderBy(x => System.DateTime.Parse(x.HoraInicio)));
            a.Clear();
            //terça
            foreach (Grade hora in horaTerInDb)
            {
                a.Add(hora);
            }
            HorariosTer = new ObservableCollection<Grade>(a.OrderBy(x => System.DateTime.Parse(x.HoraInicio)));
            a.Clear();
            //quarta
            foreach (Grade hora in horaQuaInDb)
            {
                a.Add(hora);
            }
            HorariosQua = new ObservableCollection<Grade>(a.OrderBy(x => System.DateTime.Parse(x.HoraInicio)));
            a.Clear();
            //quinta
            foreach (Grade hora in horaQuiInDb)
            {
                a.Add(hora);
            }
            HorariosQui = new ObservableCollection<Grade>(a.OrderBy(x => System.DateTime.Parse(x.HoraInicio)));
            a.Clear();
            //sexta
            foreach (Grade hora in horaSexInDb)
            {
                a.Add(hora);
            }
            HorariosSex = new ObservableCollection<Grade>(a.OrderBy(x => System.DateTime.Parse(x.HoraInicio)));
            a.Clear();
            //sábado
            foreach (Grade hora in horaSabInDb)
            {
                a.Add(hora);
            }

            HorariosSab = new ObservableCollection<Grade>(a.OrderBy(x => System.DateTime.Parse(x.HoraInicio)));
            a.Clear();
            //domingo
            foreach (Grade hora in horaDomInDb)
            {
                a.Add(hora);
            }
            HorariosDom = new ObservableCollection<Grade>(a.OrderBy(x => System.DateTime.Parse(x.HoraInicio)));


            // Load a list of all categories.
            CategoriesList = UFABC_Power_CR.App.db.Categories.ToList();

        }

        



        public  List<Historico> CarregaMateriasMaiorConceito()
        {
            var materias = from Historico hist in UFABC_Power_CR.App.db.Historicos select hist;
            List<Historico> ListaMaterias = new List<Historico>();

            foreach (Historico hist in materias)
            {
               
                if (ListaMaterias.Any(item => item.Materia.ToUpper().Trim() == hist.Materia.ToUpper().Trim() && 
                    (hist.Conceito == "A" ||
                    hist.Conceito == "B" && (item.Conceito == "C" || item.Conceito == "D" || item.Conceito == "F" || item.Conceito == "O") ||
                    hist.Conceito == "C" && (item.Conceito == "D" || item.Conceito == "F" || item.Conceito == "O") ||
                    hist.Conceito == "D" && (item.Conceito == "F" || item.Conceito == "O"))))
                {
                    ListaMaterias.Remove(ListaMaterias.First(item => item.Materia.ToUpper().Trim() == hist.Materia.ToUpper().Trim() ));
                    ListaMaterias.Add(hist);
                }
                else if (ListaMaterias.Any(item => item.Materia.ToUpper().Trim() == hist.Materia.ToUpper().Trim() )==false)
                {
                    ListaMaterias.Add(hist);
                }
            }
            return ListaMaterias;
        }





        public List<Grade> CarregaAtividades(string diaSemana)
        {
            var query = from Grade ativ in UFABC_Power_CR.App.db.Grades where ativ.Dias.Contains(diaSemana) select ativ;
            List<Grade> atividades = new List<Grade>();
            foreach (Grade ativ in query)
            {
                atividades.Add(ativ);
            }
            return atividades;
        }




        public  List<Historico> CarregaMaterias(bool materiasRefeitas)
        {
            var materias = from Historico hist in UFABC_Power_CR.App.db.Historicos select hist;
            List<Historico> ListaMaterias = new List<Historico>();
            foreach (Historico hist in materias)
            {
                if (materiasRefeitas == true)
                {
                    ListaMaterias.Add(hist);
                }
                else
                {
                    if (ListaMaterias.Any(item => item.Materia.ToUpper() == hist.Materia.ToUpper().Trim()) || hist.Conceito=="F" || hist.Conceito=="O")
                    {
                    }
                    else
                    {
                        ListaMaterias.Add(hist);
                    }
                }
            }
            return ListaMaterias;
        }



        public  List<Historico> CarregaMaterias(int quadrimestre)
        {
            var materiasNoQuadrimestre = from Historico hist in UFABC_Power_CR.App.db.Historicos where hist.Quadrimestre == quadrimestre select hist;
            List<Historico> ListaMaterias = new List<Historico>();
            foreach (Historico hist in materiasNoQuadrimestre)
            {
                ListaMaterias.Add(hist);
            }
            return ListaMaterias;
        }



        public void EditaItem(int id, ToDoItem itemNovo)
        {
            var query = from ToDoItem itemEdita in UFABC_Power_CR.App.db.Items where itemEdita.ToDoItemId == id select itemEdita;
            foreach (ToDoItem itemEdita in query)
            {
                itemEdita.ItemName = itemNovo.ItemName;
                itemEdita.ItemData = itemNovo.ItemData;
                itemEdita.ItemHora = itemNovo.ItemHora;
                itemEdita.ItemDetalhes = itemNovo.ItemDetalhes;
            }
            UFABC_Power_CR.App.db.SubmitChanges();
        }


        // Add a to-do item to the database and collections.
        public void AddToDoItem(ToDoItem newToDoItem)
        {
            // Add a to-do item to the data context.
            UFABC_Power_CR.App.db.Items.InsertOnSubmit(newToDoItem);   

            // Save changes to the database.
            UFABC_Power_CR.App.db.SubmitChanges();

            // Add a to-do item to the "all" observable collection.
            TodosItems.Add(newToDoItem);
            if (newToDoItem.ItemData.Equals(System.DateTime.Now.ToShortDateString()) || 
                System.DateTime.Now.AddDays(1).ToShortDateString().Equals(newToDoItem.ItemData) || 
                System.DateTime.Now.AddDays(2).ToShortDateString().Equals(newToDoItem.ItemData) || 
                System.DateTime.Now.AddDays(3).ToShortDateString().Equals(newToDoItem.ItemData) || 
                System.DateTime.Now.AddDays(4).ToShortDateString().Equals(newToDoItem.ItemData) || 
                System.DateTime.Now.AddDays(5).ToShortDateString().Equals(newToDoItem.ItemData) || 
                System.DateTime.Now.AddDays(6).ToShortDateString().Equals(newToDoItem.ItemData) || 
                System.DateTime.Now.AddDays(7).ToShortDateString().Equals(newToDoItem.ItemData))
            {
                List<ToDoItem> xd = new List<ToDoItem>();
                xd.AddRange(HojeItems);
                xd.Add(newToDoItem);
                HojeItems.Clear();
                HojeItems = new ObservableCollection<ToDoItem>(xd.OrderBy(v => System.DateTime.Parse(v.ItemData)));
            }

            // Add a to-do item to the appropriate filtered collection.
            switch (newToDoItem.Category.Name)
            {
                case "Exercicio":
                    ExItems.Add(newToDoItem);
                    break;
                case "Trabalho":
                    TrabItems.Add(newToDoItem);
                    break;
                case "Prova":
                    ProvasItems.Add(newToDoItem);
                    break;
                case "Outros":
                    OutrosItems.Add(newToDoItem);
                    break;
                default:
                    break;
            }
        }



        // Remove a to-do task item from the database and collections.
        public void DeleteToDoItem(ToDoItem toDoForDelete)
        {

            // Remove the to-do item from the "all" observable collection.
            TodosItems.Remove(toDoForDelete);
            if (toDoForDelete.ItemData.Equals(System.DateTime.Now.ToShortDateString()) ||
                System.DateTime.Now.AddDays(1).ToShortDateString().Equals(toDoForDelete.ItemData) ||
                System.DateTime.Now.AddDays(2).ToShortDateString().Equals(toDoForDelete.ItemData) ||
                System.DateTime.Now.AddDays(3).ToShortDateString().Equals(toDoForDelete.ItemData) ||
                System.DateTime.Now.AddDays(4).ToShortDateString().Equals(toDoForDelete.ItemData) ||
                System.DateTime.Now.AddDays(5).ToShortDateString().Equals(toDoForDelete.ItemData) ||
                System.DateTime.Now.AddDays(6).ToShortDateString().Equals(toDoForDelete.ItemData) ||
                System.DateTime.Now.AddDays(7).ToShortDateString().Equals(toDoForDelete.ItemData))
            {
                HojeItems.Remove(toDoForDelete);
            }

            // Remove the to-do item from the data context.
            UFABC_Power_CR.App.db.Items.DeleteOnSubmit(toDoForDelete);

            // Remove the to-do item from the appropriate category.
            try
            {
                switch (toDoForDelete.Category.Name)
                {
                    case "Exercicio":
                        ExItems.Remove(toDoForDelete);
                        break;
                    case "Trabalho":
                        TrabItems.Remove(toDoForDelete);
                        break;
                    case "Prova":
                        ProvasItems.Remove(toDoForDelete);
                        break;
                    case "Outros":
                        OutrosItems.Remove(toDoForDelete);
                        break;
                    default:
                        break;
                }
            }
            catch
            {
            }

            // Save changes to the database.
            UFABC_Power_CR.App.db.SubmitChanges();
        }



    }
}

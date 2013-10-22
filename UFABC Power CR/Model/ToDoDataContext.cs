using System;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace banco_de_dados_local.Model
{


    public class ToDoDataContext : DataContext
    {
        // Pass the connection string to the base class.
        public ToDoDataContext(string connectionString)
            : base(connectionString)
        { }

        // Specify a table for the to-do items.
        public Table<ToDoItem> Items;

        // Specify a table for the categories.
        public Table<ToDoCategory> Categories;

        public Table<Aluno> Aluno;

        public Table<Historico> Historicos;

        public Table<Quadrimestre> Quadrimestres;

        public Table<Grade> Grades;

        public Table<Materia> Materias;

        public Table<Professor> Professores;
    }


    [Table]
    public class ToDoCategory : INotifyPropertyChanged, INotifyPropertyChanging
    {
        // Define the entity set for the collection side of the relationship.
        private EntitySet<ToDoItem> _todos;

        [Association(Storage = "_todos", OtherKey = "_categoryId", ThisKey = "Id")]
        public EntitySet<ToDoItem> ToDos
        {
            get { return this._todos; }
            set { this._todos.Assign(value); }
        }


        // Assign handlers for the add and remove operations, respectively.
        public ToDoCategory()
        {
            _todos = new EntitySet<ToDoItem>(
                new Action<ToDoItem>(this.attach_ToDo),
                new Action<ToDoItem>(this.detach_ToDo)
                );
        }

        // Called during an add operation
        private void attach_ToDo(ToDoItem toDo)
        {
            NotifyPropertyChanging("ToDoItem");
            toDo.Category = this;
        }

        // Called during a remove operation
        private void detach_ToDo(ToDoItem toDo)
        {
            NotifyPropertyChanging("ToDoItem");
            toDo.Category = null;
        }

        // Define ID: private field, public property, and database column.
        private int _id;

        [Column(DbType = "INT NOT NULL IDENTITY", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id
        {
            get { return _id; }
            set
            {
                NotifyPropertyChanging("Id");
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }

        // Define category name: private field, public property, and database column.
        private string _name;

        [Column]
        public string Name
        {
            get { return _name; }
            set
            {
                NotifyPropertyChanging("Name");
                _name = value;
                NotifyPropertyChanged("Name");
            }
        }


        // Version column aids update performance.
        [Column(IsVersion = true)]
        private Binary _version;

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify that a property changed
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region INotifyPropertyChanging Members

        public event PropertyChangingEventHandler PropertyChanging;

        // Used to notify that a property is about to change
        private void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        #endregion
    }

    [Table]
    public class ToDoItem : INotifyPropertyChanged, INotifyPropertyChanging
    {



        // Internal column for the associated ToDoCategory ID value
        [Column]
        internal int _categoryId;

        // Entity reference, to identify the ToDoCategory "storage" table
        private EntityRef<ToDoCategory> _category;

        // Association, to describe the relationship between this key and that "storage" table
        [Association(Storage = "_category", ThisKey = "_categoryId", OtherKey = "Id", IsForeignKey = true)]
        public ToDoCategory Category
        {
            get { return _category.Entity; }
            set
            {
                NotifyPropertyChanging("Category");
                _category.Entity = value;

                if (value != null)
                {
                    _categoryId = value.Id;
                }

                NotifyPropertyChanging("Category");
            }
        }




        // Define ID: private field, public property, and database column.
        private int _toDoItemId;

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int ToDoItemId
        {
            get { return _toDoItemId; }
            set
            {
                if (_toDoItemId != value)
                {
                    NotifyPropertyChanging("ToDoItemId");
                    _toDoItemId = value;
                    NotifyPropertyChanged("ToDoItemId");
                }
            }
        }

        // Define item name: private field, public property, and database column.
        private string _itemName;

        [Column]
        public string ItemName
        {
            get { return _itemName; }
            set
            {
                if (_itemName != value)
                {
                    NotifyPropertyChanging("ItemName");
                    _itemName = value;
                    NotifyPropertyChanged("ItemName");
                }
            }
        }

        

        // Data
        private string _itemData;

        [Column]
        public string ItemData
        {
            get { return _itemData; }
            set
            {
                if (_itemData != value)
                {
                    NotifyPropertyChanging("ItemData");
                    _itemData = value;
                    NotifyPropertyChanged("ItemData");
                }
            }
        }

        
        // Horario
        private string _itemHora;

        [Column]
        public string ItemHora
        {
            get { return _itemHora; }
            set
            {
                if (_itemHora != value)
                {
                    NotifyPropertyChanging("ItemHora");
                    _itemHora = value;
                    NotifyPropertyChanged("ItemHora");
                }
            }
        }


        // Detalhes
        private string _itemDetalhes;

        [Column]
        public string ItemDetalhes
        {
            get { return _itemDetalhes; }
            set
            {
                if (_itemDetalhes != value)
                {
                    NotifyPropertyChanging("ItemDetalhes");
                    _itemDetalhes = value;
                    NotifyPropertyChanged("ItemDetalhes");
                }
            }
        }
        

        // Version column aids update performance.
        [Column(IsVersion = true)]
        private Binary _version;

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify that a property changed
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region INotifyPropertyChanging Members

        public event PropertyChangingEventHandler PropertyChanging;

        // Used to notify that a property is about to change
        private void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        #endregion
    }

    [Table]
    public class Aluno : INotifyPropertyChanged, INotifyPropertyChanging
    {
        private int _alunoId;

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int AlunoId
        {
            get { return _alunoId; }
            set
            {
                if (_alunoId != value)
                {
                    NotifyPropertyChanging("AlunoId");
                    _alunoId = value;
                    NotifyPropertyChanged("AlunoId");
                }
            }
        }
   
        private string _nome;

        [Column]
        public string Nome
        {
            get { return _nome; }
            set
            {
                if (_nome != value)
                {
                    NotifyPropertyChanging("Nome");

                    _nome = value;
                    NotifyPropertyChanged("Nome");
                }
            }
        }

        private int _helpId;
        [Column]
        public int HelpId
        {
            get { return _helpId; }
            set
            {
                if (_helpId != value)
                {
                    NotifyPropertyChanging("HelpId");

                    _helpId = value;
                    NotifyPropertyChanged("HelpId");
                }
            }
        }

        private int _fbId;
        [Column]
        public int FbID
        {
            get { return _fbId; }
            set
            {
                if (_fbId != value)
                {
                    NotifyPropertyChanging("FbId");

                    _fbId = value;
                    NotifyPropertyChanged("FbId");
                }
            }
        }

        private int _fbToken;
        [Column]
        public int FbToken
        {
            get { return _fbToken; }
            set
            {
                if (_fbToken != value)
                {
                    NotifyPropertyChanging("FbToken");

                    _fbToken = value;
                    NotifyPropertyChanged("FbToken");
                }
            }
        }


        private int _curso;
        [Column]
        public int Curso
        {
            get { return _curso; }
            set
            {
                if (_curso != value)
                {
                    NotifyPropertyChanging("Curso");

                    _curso = value;
                    NotifyPropertyChanged("Curso");
                }
            }
        }


        private int _campus;
        [Column]
        public int Campus
        {
            get { return _campus; }
            set
            {
                if (_campus != value)
                {
                    NotifyPropertyChanging("Campus");

                    _campus = value;
                    NotifyPropertyChanged("Campus");
                }
            }
        }


        private int _anoIngresso;

        [Column]
        public int AnoIngresso
        {
            get { return _anoIngresso; }
            set
            {
                if (_anoIngresso != value)
                {
                    NotifyPropertyChanging("AnoIngresso");

                    _anoIngresso = value;
                    NotifyPropertyChanged("AnoIngresso");
                }
            }
        }

        private int _quadriAtual;
        [Column]
        public int QuadriAtual
        {
            get { return _quadriAtual; }
            set
            {
                if (_quadriAtual != value)
                {
                    NotifyPropertyChanging("QuadriAtual");

                    _quadriAtual = value;
                    NotifyPropertyChanged("QuadriAtual");
                }
            }
        }

        private double _CR;

        [Column]
        public double CR
        {
            get { return _CR; }
            set
            {
                if (_CR != value)
                {
                    NotifyPropertyChanging("CR");

                    _CR = value;
                    NotifyPropertyChanged("CR");
                }
            }
        }

        private double _CA;

        [Column]
        public double CA
        {
            get { return _CA; }
            set
            {
                if (_CA != value)
                {
                    NotifyPropertyChanging("CA");

                    _CA = value;
                    NotifyPropertyChanged("CA");
                }
            }
        }


        private double _CP;

        [Column]
        public double CP
        {
            get { return _CP; }
            set
            {
                if (_CP != value)
                {
                    NotifyPropertyChanging("CP");

                    _CP = value;
                    NotifyPropertyChanged("CP");
                }
            }
        }


        // Version column aids update performance.
        [Column(IsVersion = true)]
        private Binary _version;

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify that a property changed
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region INotifyPropertyChanging Members

        public event PropertyChangingEventHandler PropertyChanging;

        // Used to notify that a property is about to change
        private void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        #endregion
    }

    [Table]
    public class Historico : INotifyPropertyChanged, INotifyPropertyChanging
    {

        private int _histId;

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int HistId
        {
            get { return _histId; }
            set
            {
                if (_histId != value)
                {
                    NotifyPropertyChanging("HistId");
                    _histId = value;
                    NotifyPropertyChanged("HistId");
                }
            }
        }


        private string _materia ;

        [Column]
        public string Materia
        {
            get { return _materia; }
            set
            {
                if (_materia != value)
                {
                    NotifyPropertyChanging("Materia");

                    _materia = value;
                    NotifyPropertyChanged("Materia");
                }
            }
        }


        private int _tipo;

        [Column]
        public int Tipo
        {
            get { return _tipo; }
            set
            {
                if (_tipo != value)
                {
                    NotifyPropertyChanging("Tipo");

                    _tipo = value;
                    NotifyPropertyChanged("Tipo");
                }
            }
        }


        private int _credito;

        [Column]
        public int Credito
        {
            get { return _credito; }
            set
            {
                if (_credito != value)
                {
                    NotifyPropertyChanging("Credito");

                    _credito = value;
                    NotifyPropertyChanged("Credito");
                }
            }
        }

        private string _conceito;

        [Column]
        public string Conceito
        {
            get { return _conceito; }
            set
            {
                if (_conceito != value)
                {
                    NotifyPropertyChanging("Conceito");

                    _conceito = value;
                    NotifyPropertyChanged("Conceito");
                }
            }
        }

        private int _quadrimestre;

        [Column]
        public int Quadrimestre
        {
            get { return _quadrimestre; }
            set
            {
                if (_quadrimestre != value)
                {
                    NotifyPropertyChanging("Quadrimestre");

                    _quadrimestre = value;
                    NotifyPropertyChanged("Quadrimestre");
                }
            }
        }


        private string _professor;

        [Column]
        public string Professor
        {
            get { return _professor; }
            set
            {
                if (_professor != value)
                {
                    NotifyPropertyChanging("Professor");

                    _professor = value;
                    NotifyPropertyChanged("Professor");
                }
            }
        }



        private string _site;

        [Column]
        public string Site
        {
            get { return _site; }
            set
            {
                if (_site != value)
                {
                    NotifyPropertyChanging("Site");

                    _site = value;
                    NotifyPropertyChanged("Site");
                }
            }
        }


        // Version column aids update performance.
        [Column(IsVersion = true)]
        private Binary _version;

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify that a property changed
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region INotifyPropertyChanging Members

        public event PropertyChangingEventHandler PropertyChanging;

        // Used to notify that a property is about to change
        private void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        #endregion
    }


    [Table]
    public class Quadrimestre : INotifyPropertyChanged, INotifyPropertyChanging
    {

        private int _quadriId;

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int QuadriId
        {
            get { return _quadriId; }
            set
            {
                if (_quadriId != value)
                {
                    NotifyPropertyChanging("QuadriId");
                    _quadriId = value;
                    NotifyPropertyChanged("QuadriId");
                }
            }
        }


        private string _nome;

        [Column]
        public string Nome
        {
            get { return _nome; }
            set
            {
                if (_nome != value)
                {
                    NotifyPropertyChanging("Nome");

                    _nome = value;
                    NotifyPropertyChanged("Nome");
                }
            }
        }


        private int _numero;

        [Column]
        public int Numero
        {
            get { return _numero; }
            set
            {
                if (_numero != value)
                {
                    NotifyPropertyChanging("Numero");

                    _numero = value;
                    NotifyPropertyChanged("Numero");
                }
            }
        }


        private double _cr;

        [Column]
        public double CR
        {
            get { return _cr; }
            set
            {
                if (_cr != value)
                {
                    NotifyPropertyChanging("CR");

                    _cr = value;
                    NotifyPropertyChanged("CR");
                }
            }
        }


        
        [Column(IsVersion = true)]
        private Binary _version;

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify that a property changed
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region INotifyPropertyChanging Members

        public event PropertyChangingEventHandler PropertyChanging;

        // Used to notify that a property is about to change
        private void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        #endregion
    }


    [Table]
    public class Grade : INotifyPropertyChanged, INotifyPropertyChanging
    {

        private int _Id;

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int Id
        {
            get { return _Id; }
            set
            {
                if (_Id != value)
                {
                    NotifyPropertyChanging("Id");
                    _Id = value;
                    NotifyPropertyChanged("Id");
                }
            }
        }


        private string _nome;
        [Column]
        public string Nome
        {
            get { return _nome; }
            set
            {
                if (_nome != value)
                {
                    NotifyPropertyChanging("Nome");

                    _nome = value;
                    NotifyPropertyChanged("Nome");
                }
            }
        }

        private string _local;
        [Column]
        public string Local
        {
            get { return _local; }
            set
            {
                if (_local != value)
                {
                    NotifyPropertyChanging("Local");

                    _local = value;
                    NotifyPropertyChanged("Local");
                }
            }
        }

        private string _sala;
        [Column]
        public string Sala
        {
            get { return _sala; }
            set
            {
                if (_sala != value)
                {
                    NotifyPropertyChanging("Sala");

                    _sala = value;
                    NotifyPropertyChanged("Sala");
                }
            }
        }

        private string _horaInicio;

        [Column]
        public string HoraInicio
        {
            get { return _horaInicio; }
            set
            {
                if (_horaInicio != value)
                {
                    NotifyPropertyChanging("HoraInicio");

                    _horaInicio = value;
                    NotifyPropertyChanged("HoraInicio");
                }
            }
        }

        private string _horaFim;

        [Column]
        public string HoraFim
        {
            get { return _horaFim; }
            set
            {
                if (_horaFim != value)
                {
                    NotifyPropertyChanging("HoraFim");

                    _horaFim = value;
                    NotifyPropertyChanged("HoraFim");
                }
            }
        }

        private string _dias;

        [Column]
        public string Dias
        {
            get { return _dias; }
            set
            {
                if (_dias != value)
                {
                    NotifyPropertyChanging("Dias");

                    _dias = value;
                    NotifyPropertyChanged("Dias");
                }
            }
        }


        private bool _aula;

        [Column]
        public bool Aula
        {
            get { return _aula; }
            set
            {
                if (_aula != value)
                {
                    NotifyPropertyChanging("Aula");

                    _aula = value;
                    NotifyPropertyChanged("Aula");
                }
            }
        }

       
        
        [Column(IsVersion = true)]
        private Binary _version;

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify that a property changed
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region INotifyPropertyChanging Members

        public event PropertyChangingEventHandler PropertyChanging;

        // Used to notify that a property is about to change
        private void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        #endregion
    }


    [Table]
    public class Materia : INotifyPropertyChanged, INotifyPropertyChanging
    {

        private int _Id;

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int Id
        {
            get { return _Id; }
            set
            {
                if (_Id != value)
                {
                    NotifyPropertyChanging("Id");
                    _Id = value;
                    NotifyPropertyChanged("Id");
                }
            }
        }


        private int _helpId;
        [Column]
        public int HelpId
        {
            get { return _helpId; }
            set
            {
                if (_helpId != value)
                {
                    NotifyPropertyChanging("HelpId");

                    _helpId = value;
                    NotifyPropertyChanged("HelpId");
                }
            }
        }

        private string _nome;
        [Column]
        public string Nome
        {
            get { return _nome; }
            set
            {
                if (_nome != value)
                {
                    NotifyPropertyChanging("Nome");

                    _nome = value;
                    NotifyPropertyChanged("Nome");
                }
            }
        }


        private int _teoria;
        [Column]
        public int Teoria
        {
            get { return _teoria; }
            set
            {
                if (_teoria != value)
                {
                    NotifyPropertyChanging("Teoria");

                    _teoria = value;
                    NotifyPropertyChanged("Teoria");
                }
            }
        }


        private int _pratica;
        [Column]
        public int Pratica
        {
            get { return _pratica; }
            set
            {
                if (_pratica != value)
                {
                    NotifyPropertyChanging("Pratica");

                    _pratica = value;
                    NotifyPropertyChanged("Pratica");
                }
            }
        }


        private int _individual;
        [Column]
        public int Individual
        {
            get { return _individual; }
            set
            {
                if (_individual != value)
                {
                    NotifyPropertyChanging("Individual");

                    _individual = value;
                    NotifyPropertyChanged("Individual");
                }
            }
        }

        private int _creditos;
        [Column]
        public int Creditos
        {
            get { return _creditos; }
            set
            {
                if (_creditos != value)
                {
                    NotifyPropertyChanging("Creditos");

                    _creditos = value;
                    NotifyPropertyChanged("Creditos");
                }
            }
        }

        private string _recomendacao;
        [Column]
        public string Recomendacao
        {
            get { return _recomendacao; }
            set
            {
                if (_recomendacao != value)
                {
                    NotifyPropertyChanging("Recomendacao");

                    _recomendacao = value;
                    NotifyPropertyChanged("Recomendacao");
                }
            }
        }


        private string _ementa;
        [Column]
        public string Ementa
        {
            get { return _ementa; }
            set
            {
                if (_ementa != value)
                {
                    NotifyPropertyChanging("Ementa");

                    _ementa = value;
                    NotifyPropertyChanged("Ementa");
                }
            }
        }

        private int _tipo;
        [Column]
        public int Tipo
        {
            get { return _tipo; }
            set
            {
                if (_tipo != value)
                {
                    NotifyPropertyChanging("Tipo");

                    _tipo = value;
                    NotifyPropertyChanged("Tipo");
                }
            }
        }


        // Version column aids update performance.
        [Column(IsVersion = true)]
        private Binary _version;

        
        
        
        
        
        
        
        
        
        
        
        
        
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify that a property changed
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region INotifyPropertyChanging Members

        public event PropertyChangingEventHandler PropertyChanging;

        // Used to notify that a property is about to change
        private void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        #endregion
    }


    [Table]
    public class Professor : INotifyPropertyChanged, INotifyPropertyChanging
    {

        private int _Id;

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int Id
        {
            get { return _Id; }
            set
            {
                if (_Id != value)
                {
                    NotifyPropertyChanging("Id");
                    _Id = value;
                    NotifyPropertyChanged("Id");
                }
            }
        }



        private int _helpId;

        [Column]
        public int helpId
        {
            get { return _helpId; }
            set
            {
                if (_helpId != value)
                {
                    NotifyPropertyChanging("HelpId");

                    _helpId = value;
                    NotifyPropertyChanged("HelpId");
                }
            }
        }


        private string _nome;

        [Column]
        public string Nome
        {
            get { return _nome; }
            set
            {
                if (_nome != value)
                {
                    NotifyPropertyChanging("Nome");

                    _nome = value;
                    NotifyPropertyChanged("Nome");
                }
            }
        }

        private string _imagem;
        [Column]
        public string Imagem
        {
            get { return _imagem; }
            set
            {
                if (_imagem != value)
                {
                    NotifyPropertyChanging("Imagem");

                    _imagem = value;
                    NotifyPropertyChanged("Imagem");
                }
            }
        }

        private string _centro;
        [Column]
        public string Centro
        {
            get { return _centro; }
            set
            {
                if (_centro != value)
                {
                    NotifyPropertyChanging("Centro");

                    _centro = value;
                    NotifyPropertyChanged("Centro");
                }
            }
        }

        private string _email;

        [Column]
        public string Email
        {
            get { return _email; }
            set
            {
                if (_email != value)
                {
                    NotifyPropertyChanging("Email");

                    _email = value;
                    NotifyPropertyChanged("Email");
                }
            }
        }


        private string _local;
        [Column]
        public string Local
        {
            get { return _local; }
            set
            {
                if (_local != value)
                {
                    NotifyPropertyChanging("Local");

                    _local = value;
                    NotifyPropertyChanged("Local");
                }
            }
        }


        private string _sala;

        [Column]
        public string Sala
        {
            get { return _sala; }
            set
            {
                if (_sala != value)
                {
                    NotifyPropertyChanging("Sala");

                    _sala = value;
                    NotifyPropertyChanged("Sala");
                }
            }
        }

        private string _telefone;

        [Column]
        public string Telefone
        {
            get { return _telefone; }
            set
            {
                if (_telefone != value)
                {
                    NotifyPropertyChanging("Telefone");

                    _telefone = value;
                    NotifyPropertyChanged("Telefone");
                }
            }
        }

        // Version column aids update performance.
        [Column(IsVersion = true)]
        private Binary _version;

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify that a property changed
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region INotifyPropertyChanging Members

        public event PropertyChangingEventHandler PropertyChanging;

        // Used to notify that a property is about to change
        private void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        #endregion
    }

   

   

}
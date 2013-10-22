using System;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace banco_de_dados_local.Model
{
    class UsuariosDataContext : DataContext
    {
        public UsuariosDataContext(string connectionString)
            : base(connectionString)
        { }

        public Table<Usuario> Usuarios;

        public Table<Login> Login;


    }


    [Table]
    public class Usuario : INotifyPropertyChanged, INotifyPropertyChanging
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


        private byte[] _senha;

        [Column]
        public byte[] Senha
        {
            get { return _senha; }
            set
            {
                if (_senha != value)
                {
                    NotifyPropertyChanging("Senha");

                    _senha = value;
                    NotifyPropertyChanged("Senha");
                }
            }
        }
        

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
    public class Login : INotifyPropertyChanged, INotifyPropertyChanging
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


        private bool _auto;

        [Column]
        public bool Auto
        {
            get { return _auto; }
            set
            {
                if (_auto != value)
                {
                    NotifyPropertyChanging("Auto");

                    _auto = value;
                    NotifyPropertyChanged("Auto");
                }
            }
        }


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

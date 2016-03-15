﻿/**
Copyright(c) 2016 Menno van der Woude

Permission is hereby granted, free of charge, to any person obtaining a 
copy of this software and associated documentation files (the "Software"), 
to deal in the Software without restriction, including without limitation 
the rights to use, copy, modify, merge, publish, distribute, sublicense, 
and/or sell copies of the Software, and to permit persons to whom the 
Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included 
in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS 
OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING 
FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
DEALINGS IN THE SOFTWARE.
**/

using Microsoft.Win32;
using MiniTD.DataAccess;
using MiniTD.DataTypes;
using MiniTD.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MiniTD.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Fields

        private MiniOrganizerViewModel _OrganizerVM;
        private MiniDataProvider _DataProvider;

        #endregion // Fields

        #region Properties

        public MiniOrganizerViewModel OrganizerVM
        {
            get { return _OrganizerVM; }
            set
            {
                _OrganizerVM = value;
                OnPropertyChanged("OrganizerVM");
                OnPropertyChanged("HasOrganizer");
            }
        }

        public bool HasOrganizer
        {
            get { return OrganizerVM != null; }
        }

        public MiniDataProvider DataProvider
        {
            get { return _DataProvider; }
        }

        #endregion // Properties

        #region Commands

        RelayCommand _NewFileCommand;
        public ICommand NewFileCommand
        {
            get
            {
                if (_NewFileCommand == null)
                {
                    _NewFileCommand = new RelayCommand(NewFileCommand_Executed, NewFileCommand_CanExecute);
                }
                return _NewFileCommand;
            }
        }

        RelayCommand _OpenFileCommand;
        public ICommand OpenFileCommand
        {
            get
            {
                if (_OpenFileCommand == null)
                {
                    _OpenFileCommand = new RelayCommand(OpenFileCommand_Executed, OpenFileCommand_CanExecute);
                }
                return _OpenFileCommand;
            }
        }

        RelayCommand _SaveFileCommand;
        public ICommand SaveFileCommand
        {
            get
            {
                if (_SaveFileCommand == null)
                {
                    _SaveFileCommand = new RelayCommand(SaveFileCommand_Executed, SaveFileCommand_CanExecute);
                }
                return _SaveFileCommand;
            }
        }

        RelayCommand _SaveAsFileCommand;
        public ICommand SaveAsFileCommand
        {
            get
            {
                if (_SaveAsFileCommand == null)
                {
                    _SaveAsFileCommand = new RelayCommand(SaveAsFileCommand_Executed, SaveAsFileCommand_CanExecute);
                }
                return _SaveAsFileCommand;
            }
        }


        RelayCommand _CloseFileCommand;
        public ICommand CloseFileCommand
        {
            get
            {
                if (_CloseFileCommand == null)
                {
                    _CloseFileCommand = new RelayCommand(CloseFileCommand_Executed, CloseFileCommand_CanExecute);
                }
                return _CloseFileCommand;
            }
        }

        RelayCommand _ExitApplicationCommand;
        public ICommand ExitApplicationCommand
        {
            get
            {
                if (_ExitApplicationCommand == null)
                {
                    _ExitApplicationCommand = new RelayCommand(ExitApplicationCommand_Executed, ExitApplicationCommand_CanExecute);
                }
                return _ExitApplicationCommand;
            }
        }

        #endregion // Commands

        #region Command functionality


        void NewFileCommand_Executed(object prm)
        {
            DataProvider.NewOrganizer();
            OrganizerVM = new MiniOrganizerViewModel(DataProvider);
        }
        
        bool NewFileCommand_CanExecute(object prm)
        {
            return true;
        }

        void OpenFileCommand_Executed(object prm)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.CheckFileExists = true;
            openFileDialog.Filter = "MiniTD files|*.mtd";
            if (openFileDialog.ShowDialog() == true)
            {
                DataProvider.FileName = openFileDialog.FileName;
                DataProvider.LoadOrganizer();
                OrganizerVM = new MiniOrganizerViewModel(DataProvider);
            }
        }

        bool OpenFileCommand_CanExecute(object prm)
        {
            return true;
        }

        void SaveFileCommand_Executed(object prm)
        {
            if (string.IsNullOrWhiteSpace(DataProvider.FileName))
                SaveAsFileCommand.Execute(null);
            else
                DataProvider.SaveOrganizer();
        }

        bool SaveFileCommand_CanExecute(object prm)
        {
            return true;
        }
        
        void SaveAsFileCommand_Executed(object prm)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.Filter = "MiniTD files|*.mtd";
            if (!string.IsNullOrWhiteSpace(DataProvider.FileName))
                saveFileDialog.FileName = DataProvider.FileName;
            if (saveFileDialog.ShowDialog() == true)
            {
                DataProvider.FileName = saveFileDialog.FileName;
                DataProvider.SaveOrganizer();
            }
        }

        bool SaveAsFileCommand_CanExecute(object prm)
        {
            return true;
        }
        
        void CloseFileCommand_Executed(object prm)
        {
            DataProvider.CloseOrganizer();
            OrganizerVM = null;
        }
        
        bool CloseFileCommand_CanExecute(object prm)
        {
            return true;
        }
        
        void ExitApplicationCommand_Executed(object prm)
        {
            System.Windows.Application.Current.Shutdown();
        }
        
        bool ExitApplicationCommand_CanExecute(object prm)
        {
            return true;
        }

        #endregion // Command functionality

        #region Private methods

        #endregion // Private methods

        #region Public methods

        #endregion // Public methods

        #region Constructor

        public MainWindowViewModel()
        {
            _DataProvider = new MiniDataProvider();
        }

        #endregion // Constructor
    }
}
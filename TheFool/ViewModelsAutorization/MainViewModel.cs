using ModelsApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using TheFool.Tools;
using TheFool.ViewsAutorization;

namespace TheFool.ViewModelsAutorization
{
    public class MainViewModel : BaseViewModel
    {

        private UserApi entry;

        public List<UserApi> users { get; set; }
        public UserApi Entry
        {
            get => entry;
            set
            {
                entry = value;
                SignalChanged();
            }
        }

        public CustomCommand SingIn { get; set; }
        public CustomCommand Register { get; set; }

        public MainViewModel(UserApi user)
        {

            Task.Run(GetListUsers).ContinueWith(s =>
            {
                if (user == null)
                {
                    Entry = new UserApi();
                }
                else
                {
                    Entry = new UserApi
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        Password = user.Password,
                        Email = user.Email,
                    };
                }
            });

            SingIn = new CustomCommand(() =>
            {
                try
                {
                    Task.Run(GetListUsers);
                    Thread.Sleep(200);
                    foreach (var Users in users)
                    {
                        if (Users.UserName == Entry.UserName && Users.Password == Entry.Password)
                        {
                            InsideWindow qq = new InsideWindow();
                            qq.Show();
                            foreach (Window window in Application.Current.Windows)
                            {
                                if (window.DataContext == this)
                                    CloseWin(window);
                            }
                        }
                        else if (Users.Password != Entry.Password && Users.UserName != Entry.UserName)
                        {
                            continue;
                        }
                    }
                    if (Application.Current.MainWindow?.IsActive == true)
                        MessageBox.Show("Пароль или логин неверны.");
                }
                catch
                {
                    MessageBox.Show("Закиньте дров в печь");
                }
            });

            Register = new CustomCommand(() =>
            {
                RegisterWindow rw = new RegisterWindow();
                rw.ShowDialog();
            });
        }

        public async Task GetListUsers()
        {
            var result = await Api.GetListAsync<UserApi[]>("User");
            users = new List<UserApi>(result);
            SignalChanged("users");
        }

        public void CloseWin(object obj)
        {
            Window win = obj as Window;
            win.Close();
        }
    }
}

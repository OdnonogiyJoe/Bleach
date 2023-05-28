using ModelsApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using TheFool.Tools;

namespace TheFool.ViewModelsAutorization
{
    class RegisterViewModel : BaseViewModel
    {
        public List<UserApi> users { get; set; }
        public UserApi AddUser { get; set; }
        public CustomCommand SaveUser { get; set; }

        public RegisterViewModel(UserApi user)
        {
            Task.Run(GetListUsers);
            if (user == null)
            {
                AddUser = new UserApi();
            }
            else
            {
                AddUser = new UserApi
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Password = user.Password,
                    Email = user.Email,
                };

            }
            SaveUser = new CustomCommand(() =>
            {
                if (AddUser.Id == 0)
                {
                    Task.Run(CreateNewUser);
                    Thread.Sleep(200);
                    Task.Run(GetListUsers);
                }
                else
                    Task.Run(EditUsers);

                foreach (Window window in Application.Current.Windows)
                {
                    if (window.DataContext == this)
                        CloseWin(window);
                }
            });
        }

        public void CloseWin(object obj)
        {
            Window win = obj as Window;
            win.Close();
        }

        public async Task CreateNewUser()
        {
            await Api.PostAsync<UserApi>(AddUser, "User");
        }

        public async Task EditUsers()
        {
            await Api.PutAsync<UserApi>(AddUser, "User");
        }

        public async Task GetListUsers()
        {
            var result = await Api.GetListAsync<UserApi[]>("User");
            users = new List<UserApi>(result);
            SignalChanged("users");
        }
    }
}

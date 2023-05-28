using ModelsApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TheFool.Tools;
using TheFool.Views;

namespace TheFool.ViewModels
{
    public class ProfilePageViewModel : BaseViewModel
    {
        //#region Search
        //private string searchText = "";
        //public string SearchText
        //{
        //    get => searchText;
        //    set
        //    {
        //        searchText = value;
        //        Search();
        //    }
        //}
        //private void Search()
        //{
        //    var search = SearchText.ToLower();
        //    searchResult = mysearch.Where(s => s.Name.Contains(search)).ToList();
        //    WorkPlaces = searchResult;
        //}

        //public List<WorkPlaceApi> searchResult;

        //public List<WorkPlaceApi> mysearch { get; set; }
        //#endregion

        #region Commands
        public CustomCommand AddAnnouncement { get; set; }
        public CustomCommand EditAnnouncement { get; set; }
        #endregion

        #region Lego
        public ProfilePageViewModel()
        {
            Task.Run(TakeListAnnouncements).ContinueWith(s =>
            {

            });
            #region добавляй удаляй пиратствуй
            AddAnnouncement = new CustomCommand(() =>
            {
                EditProfileAnnouncementWindow editAnnouncement = new EditProfileAnnouncementWindow();
                editAnnouncement.ShowDialog();
                Thread.Sleep(200);
                Task.Run(TakeListAnnouncements);
            });

            EditAnnouncement = new CustomCommand(() =>
            {
                if (SelectedAnnouncement == null) return;
                EditProfileAnnouncementWindow editAnnouncement = new EditProfileAnnouncementWindow(SelectedAnnouncement);
                editAnnouncement.ShowDialog();
                Thread.Sleep(200);
                Task.Run(TakeListAnnouncements);
            });
            #endregion

        }
        #endregion

        #region properties
        public AnnouncementApi selectedAnnouncement;
        public AnnouncementApi SelectedAnnouncement
        {
            get => selectedAnnouncement;
            set
            {
                selectedAnnouncement = value;
                SignalChanged();
            }
        }
        #endregion

        #region свойства
        public List<AnnouncementApi> announcements { get; set; }
        public List<AnnouncementApi> Announcements
        {
            get => announcements;
            set
            {
                announcements = value;
                SignalChanged();
            }
        }
        #endregion

        #region task

        public async Task TakeListAnnouncements()
        {
            var result = await Api.GetListAsync<AnnouncementApi[]>("Announcement");
            Announcements = new List<AnnouncementApi>(result);
            SignalChanged("Announcements");
            //searchResult = new List<AnnouncementApi>(result);
            //mysearch = new List<WorkPlaceApi>(result);
        }
        #endregion

        public class MainViewModel : BaseViewModel
        {
            private object _currentPage;
            public object CurrentPage
            {
                get { return _currentPage; }
                set
                {
                    _currentPage = value;
                    SignalChanged("CurrentPage");
                }
            }

        }
    }
}

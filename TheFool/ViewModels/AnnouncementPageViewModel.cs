using ModelsApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheFool.Tools;

namespace TheFool.ViewModels
{
    public class AnnouncementPageViewModel : BaseViewModel
    {
        #region Lego
        public AnnouncementPageViewModel()
        {
            Task.Run(TakeListAnnouncements).ContinueWith(s =>
            {
                //InitPagination();
                //Pagination();
            });
        }
        #endregion

        #region свойства
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
            //mysearch = new List<AnnouncementApi>(result);
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

            #region commands
            //public CustomCommand AnnouncementPage { get; set; }
            //public CustomCommand ProfilePage { get; set; }
            //public CustomCommand HomePage { get; set; }

            #endregion
        }
    }
}

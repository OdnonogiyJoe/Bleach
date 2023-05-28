using ModelsApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TheFool.Tools;

namespace TheFool.ViewModels
{
    public class EditProfileAnnouncementWindowViewModel : BaseViewModel
    {
        #region properties Берем и назначаем
        private AnnouncementApi addAnnouncement;
        public AnnouncementApi AddAnnouncement
        {
            get => addAnnouncement;
            set
            {
                addAnnouncement = value;
                SignalChanged();
            }
        }

        public List<AnnouncementApi> announcements { get; set; }
        public List<StatusApi> statuses { get; set; }
        public List<DistrictApi> districts { get; set; }
        public List<HouseTypeApi> houseTypes { get; set; }
        public List<ParkingApi> parkings { get; set; }
        public List<BalconyOrLoggiumApi> balconyOrLoggia { get; set; }
        public List<RepairApi> repairs { get; set; }
        public List<WindowApi> windows { get; set; }
        public List<CityApi> cities { get; set; }
        public List<ApartmentTypeApi> apartmentTypes { get; set; }
        public List<BathroomApi> bathrooms { get; set; }
        public List<ElevatorApi> elevators { get; set; }
        #endregion

        #region commands
        public CustomCommand Save { get; set; }

        public StatusApi selectedStatus;
        public DistrictApi selectedDistrict;
        public HouseTypeApi selectedHouseType;
        public ParkingApi selectedParking;
        public BalconyOrLoggiumApi selectedBalconyOrLoggia;
        public RepairApi selectedRepair;
        public WindowApi selectedWindow;
        public CityApi selectedCity;
        public ApartmentTypeApi selectedApartmentType;
        public BathroomApi selectedBathroom;
        public ElevatorApi selectedElevator;

        public StatusApi SelectedStatus
        {
            get => selectedStatus;
            set
            {
                selectedStatus = value;
                SignalChanged();
            }
        }
        public DistrictApi SelectedDistrict
        {
            get => selectedDistrict;
            set
            {
                selectedDistrict = value;
                SignalChanged();
            }
        }
        public HouseTypeApi SelectedHouseType
        {
            get => selectedHouseType;
            set
            {
                selectedHouseType = value;
                SignalChanged();
            }
        }
        public ParkingApi SelectedParking
        {
            get => selectedParking;
            set
            {
                selectedParking = value;
                SignalChanged();
            }
        }
        public BalconyOrLoggiumApi SelectedBalconyOrLoggia
        {
            get => selectedBalconyOrLoggia;
            set
            {
                selectedBalconyOrLoggia = value;
                SignalChanged();
            }
        }
        public RepairApi SelectedRepair
        {
            get => selectedRepair;
            set
            {
                selectedRepair = value;
                SignalChanged();
            }
        }
        public WindowApi SelectedWindow
        {
            get => selectedWindow;
            set
            {
                selectedWindow = value;
                SignalChanged();
            }
        }
        public CityApi SelectedCity
        {
            get => selectedCity;
            set
            {
                selectedCity = value;
                SignalChanged();
            }
        }
        public ApartmentTypeApi SelectedApartmentType
        {
            get => selectedApartmentType;
            set
            {
                selectedApartmentType = value;
                SignalChanged();
            }
        }
        public BathroomApi SelectedBathroom
        {
            get => selectedBathroom;
            set
            {
                selectedBathroom = value;
                SignalChanged();
            }
        }
        public ElevatorApi SelectedElevator
        {
            get => selectedElevator;
            set
            {
                selectedElevator = value;
                SignalChanged();
            }
        }
        #endregion

        #region Lego
        public EditProfileAnnouncementWindowViewModel(AnnouncementApi announcement)
        {
            Task.Run(TakeListAnnouncements).ContinueWith(s =>
            {
                if (announcement == null)
                {
                    AddAnnouncement = new AnnouncementApi();
                }
                else
                {
                    AddAnnouncement = new AnnouncementApi
                    {
                        Id = announcement.Id,
                        UserId = announcement.UserId,
                        Description = announcement.Description,
                        CityId = announcement.CityId,
                        DistrictId = announcement.DistrictId,
                        Street = announcement.Street,
                        Price = announcement.Price,
                        ApartmentTypeId = announcement.ApartmentTypeId,
                        HouseTypeId = announcement.HouseTypeId,
                        BathroomId = announcement.BathroomId,
                        ParkingId = announcement.ParkingId,
                        FloorsInTheApartment = announcement.FloorsInTheApartment,
                        StatusId = announcement.StatusId,
                        RepairId = announcement.RepairId,
                        WindowId = announcement.WindowId,
                        BalconyOrLoggiaId = announcement.BalconyOrLoggiaId,
                        ElevatorId = announcement.ElevatorId,
                        TotalArea = announcement.TotalArea,
                    };
                    if (announcement.StatusId != null)
                    {
                        SelectedStatus = statuses.First(s => s.Id == announcement.StatusId);
                    }
                    if (announcement.CityId != null)
                    {
                        SelectedCity = cities.First(s => s.Id == announcement.CityId);
                    }
                    if (announcement.DistrictId != null)
                    {
                        SelectedDistrict = districts.First(s => s.Id == announcement.DistrictId);
                    }
                    if (announcement.ApartmentTypeId != null)
                    {
                        SelectedApartmentType = apartmentTypes.First(s => s.Id == announcement.ApartmentTypeId);
                    }
                    if (announcement.HouseTypeId != null)
                    {
                        SelectedHouseType = houseTypes.First(s => s.Id == announcement.HouseTypeId);
                    }
                    if (announcement.BathroomId != null)
                    {
                        SelectedBathroom = bathrooms.First(s => s.Id == announcement.BathroomId);
                    }
                    if (announcement.ParkingId != null)
                    {
                        SelectedParking = parkings.First(s => s.Id == announcement.ParkingId);
                    }
                    if (announcement.RepairId != null)
                    {
                        SelectedRepair = repairs.First(s => s.Id == announcement.RepairId);
                    }
                    if (announcement.WindowId != null)
                    {
                        SelectedWindow = windows.First(s => s.Id == announcement.WindowId);
                    }
                    if (announcement.BalconyOrLoggiaId != null)
                    {
                        SelectedBalconyOrLoggia = balconyOrLoggia.First(s => s.Id == announcement.BalconyOrLoggiaId);
                    }
                    if (announcement.ElevatorId != null)
                    {
                        SelectedElevator = elevators.First(s => s.Id == announcement.ElevatorId);
                    }
                }
            });

            Save = new CustomCommand(() =>
            {
                if (AddAnnouncement.Id == 0)
                    Task.Run(CreateNewAnnouncement);
                else
                    Task.Run(EditAnnouncement);

                foreach (Window window in Application.Current.Windows)
                {
                    if (window.DataContext == this) CloseWin(window);
                }

            });
        }

        #endregion

        #region task
        public async Task CreateNewAnnouncement()
        {
            if (AddAnnouncement.StatusId != null && AddAnnouncement.CityId != null && AddAnnouncement.DistrictId != null &&
                AddAnnouncement.ApartmentTypeId != null && AddAnnouncement.HouseTypeId != null && AddAnnouncement.BathroomId != null &&
                AddAnnouncement.ParkingId != null && AddAnnouncement.RepairId != null && AddAnnouncement.WindowId != null && AddAnnouncement.BalconyOrLoggiaId != null &&
                AddAnnouncement.ElevatorId != null)
            {
                AddAnnouncement.StatusId = selectedStatus.Id;
                AddAnnouncement.CityId = selectedCity.Id;
                AddAnnouncement.DistrictId = selectedDistrict.Id;
                AddAnnouncement.ApartmentTypeId = selectedApartmentType.Id;
                AddAnnouncement.HouseTypeId = selectedHouseType.Id;
                AddAnnouncement.BathroomId = selectedBathroom.Id;
                AddAnnouncement.ParkingId = selectedParking.Id;
                AddAnnouncement.RepairId = selectedRepair.Id;
                AddAnnouncement.WindowId = selectedWindow.Id;
                AddAnnouncement.BalconyOrLoggiaId = selectedBalconyOrLoggia.Id;
                AddAnnouncement.ElevatorId = selectedElevator.Id;
                AddAnnouncement.RepairId = selectedRepair.Id;
            }
                await Api.PostAsync<AnnouncementApi>(AddAnnouncement, "Announcement");
        }

        public async Task EditAnnouncement()
        {
            if (AddAnnouncement.StatusId != null && AddAnnouncement.CityId != null && AddAnnouncement.DistrictId != null &&
                AddAnnouncement.ApartmentTypeId != null && AddAnnouncement.HouseTypeId != null && AddAnnouncement.BathroomId != null &&
                AddAnnouncement.ParkingId != null && AddAnnouncement.RepairId != null && AddAnnouncement.WindowId != null && AddAnnouncement.BalconyOrLoggiaId != null &&
                AddAnnouncement.ElevatorId != null)
            {
                AddAnnouncement.StatusId = selectedStatus.Id;
                AddAnnouncement.CityId = selectedCity.Id;
                AddAnnouncement.DistrictId = selectedDistrict.Id;
                AddAnnouncement.ApartmentTypeId = selectedApartmentType.Id;
                AddAnnouncement.HouseTypeId = selectedHouseType.Id;
                AddAnnouncement.BathroomId = selectedBathroom.Id;
                AddAnnouncement.ParkingId = selectedParking.Id;
                AddAnnouncement.RepairId = selectedRepair.Id;
                AddAnnouncement.WindowId = selectedWindow.Id;
                AddAnnouncement.BalconyOrLoggiaId = selectedBalconyOrLoggia.Id;
                AddAnnouncement.ElevatorId = selectedElevator.Id;
                AddAnnouncement.RepairId = selectedRepair.Id;
            }
            await Api.PutAsync<AnnouncementApi>(AddAnnouncement, "Announcement");
        }

        public async Task TakeListAnnouncements()
        {
            var result = await Api.GetListAsync<AnnouncementApi[]>("Announcement");
            announcements = new List<AnnouncementApi>(result);
            SignalChanged("announcements");

            var result2 = await Api.GetListAsync<StatusApi[]>("Status");
            statuses = new List<StatusApi>(result2);
            SignalChanged("statuses");

            var result3 = await Api.GetListAsync<HouseTypeApi[]>("HouseType");
            houseTypes = new List<HouseTypeApi>(result3);
            SignalChanged("houseTypes");

            var result4 = await Api.GetListAsync<CityApi[]>("City");
            cities = new List<CityApi>(result4);
            SignalChanged("cities");
        }
        #endregion

        public void CloseWin(object obj)
        {
            Window win = obj as Window;
            win.Close();
        }
    }
}

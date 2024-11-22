using Microsoft.Maui.Storage;
namespace prac8RMP
{
    public partial class MainPage : ContentPage
    {
        private const string FIOKey = "FIO";
        private const string GenderKey = "Gender";
        private const string AgeKey = "Age";
        private const string MathGradeKey = "MathGrade";
        private const string RusLanGradeKey = "RusLanGrade";
        private const string PhotoKey = "PhotoPath";
        public MainPage()
        {
            InitializeComponent();
            LoadData(); // Загружаем данные при инициализации
        }

        private async void OnUploadPhotoTapped(object sender, EventArgs e)
        {
            try
            {
                var result = await FilePicker.PickAsync(new PickOptions
                {
                    PickerTitle = "Выберите фото",
                    FileTypes = FilePickerFileType.Images
                });

                if (result != null)
                {
                    var stream = await result.OpenReadAsync();
                    UploadedImage.Source = ImageSource.FromStream(() => stream);

                    // Сохраняем путь к изображению
                    Preferences.Set(PhotoKey, result.FullPath);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", ex.Message, "OK");
            }
        }

        private void LoadData()
        {
            // Загружаем сохраненные данные
            FIOEntry.Text = Preferences.Get(FIOKey, string.Empty);
            Gender.SelectedItem = Preferences.Get(GenderKey, string.Empty);
            AgeEntry.Text = Preferences.Get(AgeKey, string.Empty);
            MathGradeEntry.Text = Preferences.Get(MathGradeKey, string.Empty);
            RusLanGradeEntry.Text = Preferences.Get(RusLanGradeKey, string.Empty);

            // Загружаем изображение
            var photoPath = Preferences.Get(PhotoKey, string.Empty);
            if (!string.IsNullOrEmpty(photoPath))
            {
                UploadedImage.Source = ImageSource.FromFile(photoPath);
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

            // Сохраняем данные при закрытии страницы
            Preferences.Set(FIOKey, FIOEntry.Text);
            Preferences.Set(GenderKey, Convert.ToString(Gender.SelectedItem));
            Preferences.Set(AgeKey, AgeEntry.Text);
            Preferences.Set(MathGradeKey, MathGradeEntry.Text);
            Preferences.Set(RusLanGradeKey, RusLanGradeEntry.Text);
        }
    }
}



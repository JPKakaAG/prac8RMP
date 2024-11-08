namespace prac8RMP
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnUploadPhotoTapped(object sender, EventArgs e)
        {
            try
            {
                // Открытие выбора файла
                var result = await FilePicker.PickAsync(new PickOptions
                {
                    PickerTitle = "Выберите фото",
                    FileTypes = FilePickerFileType.Images // Ограничиваем выбор только изображениями
                });

                if (result != null)
                {
                    // Получаем поток файла
                    var stream = await result.OpenReadAsync();

                    // Устанавливаем изображение на UI
                    UploadedImage.Source = ImageSource.FromStream(() => stream);
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибок, если нужно
                await DisplayAlert("Ошибка", ex.Message, "OK");
            }
        }
    }

}

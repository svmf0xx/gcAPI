namespace gcapi.Models
{
    public class ThemeData
    {
        public string Color { get; set; } = "white";
        public string PicturePath { get; set; } = "/etc/themepics/vlad.png"; // хмм
        //а зачем? Это типа настройки внешнего вида, которые вообще могут храниться в куки, а не в бд
        //плюс тут не должна храниться аватарка пользователя
    }
}
